using System.Collections.Concurrent;
using Npgsql;
using StackExchange.Redis;

namespace Zamboni14Legacy.Api;

#nullable enable
public class ApiHandler
{
    private readonly string baseUrl;
    private readonly string routePrefix;

    public ApiHandler()
    {
        var cfg = Program.ZamboniConfig;

        baseUrl = $"http://0.0.0.0:{cfg.ApiServerPort}";
        routePrefix = "/" + cfg.ApiServerIdentifier.Trim('/');
    }
    public async Task StartAsync()
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseUrls(baseUrl);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSingleton<IConnectionMultiplexer?>(_ =>
        {
            try { return ConnectionMultiplexer.Connect(Program.ZamboniConfig.RedisConnectionString); }
            catch { return null; }
        });

        builder.Services.AddSingleton(_ =>
            new FixedWindowRateLimiter(120, TimeSpan.FromMinutes(1), 10));

        var app = builder.Build();
        string prefix = "/" + Program.ZamboniConfig.ApiServerIdentifier;

        app.Use(async (ctx, next) =>
        {
            var limiter = ctx.RequestServices.GetRequiredService<FixedWindowRateLimiter>();
            if (!await limiter.AllowRequestAsync())
            {
                ctx.Response.StatusCode = 429;
                await ctx.Response.WriteAsJsonAsync(new { message = "Too many requests" });
                return;
            }
            await next();
        });

        // HELPERS

        static Dictionary<string, object?> ReadRow(NpgsqlDataReader r)
        {
            var row = new Dictionary<string, object?>();
            for (int i = 0; i < r.FieldCount; i++)
                row[r.GetName(i)] = r.IsDBNull(i) ? null : r.GetValue(i);
            return row;
        }

        static async Task<List<Dictionary<string, object?>>> ReadRows(
            NpgsqlConnection conn,
            string sql,
            params NpgsqlParameter[] p)
        {
            using var cmd = new NpgsqlCommand(sql, conn);
            foreach (var x in p) cmd.Parameters.Add(x);

            var list = new List<Dictionary<string, object?>>();
            using var r = await cmd.ExecuteReaderAsync();

            while (await r.ReadAsync())
                list.Add(ReadRow(r));

            return list;
        }

        static List<object> GroupByGame(List<Dictionary<string, object?>> rows)
        {
            return rows
                .GroupBy(r => Convert.ToInt64(r["game_id"]))
                .Select(g => new
                {
                    game_id = g.Key,
                    reports = g.ToList()
                })
                .Cast<object>()
                .ToList();
        }

        static DateTime RangeToDate(string range) =>
            range switch
            {
                "day" => DateTime.UtcNow.AddDays(-1),
                "week" => DateTime.UtcNow.AddDays(-7),
                "month" => DateTime.UtcNow.AddMonths(-1),
                _ => DateTime.MinValue
            };

        // STATUS
        app.MapGet($"{prefix}/status", () => Results.Json(new
        {
            serverVersion = Program.Name,
            onlineUsersCount = ServerManager.GetServerPlayers().Count,
            queuedUsers = ServerManager.GetQueuedPlayers().Count,
            activeGames = ServerManager.GetServerGames().Count
        }));

        // PLAYERS
        app.MapGet($"{prefix}/api/players", async () =>
        {
            await using var conn = new NpgsqlConnection(Program.ZamboniConfig.DatabaseConnectionString);
            await conn.OpenAsync();

            var rows = await ReadRows(conn, @"
                SELECT DISTINCT gtag FROM reports_vs
                UNION
                SELECT DISTINCT gtag FROM reports_so
            ");

            return Results.Json(rows.Select(r => r["gtag"]).Where(x => x != null));
        });
        
        // RAW
        app.MapGet($"{prefix}/api/raw/games", async () =>
        {
            await using var conn = new NpgsqlConnection(Program.ZamboniConfig.DatabaseConnectionString);
            await conn.OpenAsync();
            return Results.Json(await ReadRows(conn, "SELECT * FROM games ORDER BY created_at DESC"));
        });

        app.MapGet($"{prefix}/api/raw/reports", async () =>
        {
            await using var conn = new NpgsqlConnection(Program.ZamboniConfig.DatabaseConnectionString);
            await conn.OpenAsync();

            return Results.Json(await ReadRows(conn, @"
                SELECT * FROM reports_vs
                UNION ALL
                SELECT * FROM reports_so
                ORDER BY created_at DESC
            "));
        });

        // RAW VS / SO
        app.MapGet($"{prefix}/api/raw/vs", async () =>
        {
            await using var conn = new NpgsqlConnection(Program.ZamboniConfig.DatabaseConnectionString);
            await conn.OpenAsync();
            return Results.Json(await ReadRows(conn, "SELECT * FROM reports_vs ORDER BY created_at DESC"));
        });

        app.MapGet($"{prefix}/api/raw/so", async () =>
        {
            await using var conn = new NpgsqlConnection(Program.ZamboniConfig.DatabaseConnectionString);
            await conn.OpenAsync();
            return Results.Json(await ReadRows(conn, "SELECT * FROM reports_so ORDER BY created_at DESC"));
        });

        // GAMES
        app.MapGet($"{prefix}/api/games", async () =>
        {
            await using var conn = new NpgsqlConnection(Program.ZamboniConfig.DatabaseConnectionString);
            await conn.OpenAsync();

            var games = await ReadRows(conn, "SELECT * FROM games ORDER BY created_at DESC");

            var reports = await ReadRows(conn, @"
                SELECT * FROM reports_vs
                UNION ALL
                SELECT * FROM reports_so
            ");

            var grouped = reports.GroupBy(r => Convert.ToInt64(r["game_id"]))
                .ToDictionary(g => g.Key, g => g.ToList());

            var result = new List<object>();

            foreach (var g in games)
            {
                var id = Convert.ToInt64(g["game_id"]);
                grouped.TryGetValue(id, out var reps);
                reps ??= new();

                result.Add(new
                {
                    game_id = id,
                    created_at = g["created_at"],
                    teams = reps.Select(r => r["team_name"]).Distinct(),
                    players = reps.Count,
                    totalGoals = reps.Sum(r => Convert.ToInt32(r["scor"] ?? 0)),
                    avgFps = reps.Any() ? reps.Average(r => Convert.ToInt32(r["fpsavg"] ?? 0)) : 0,
                    avgLatency = reps.Any() ? reps.Average(r => Convert.ToInt32(r["lateavgnet"] ?? 0)) : 0,
                    status = reps.Count > 0 ? "Finished" : "Unknown"
                });
            }

            return Results.Json(result);
        });

        // GAME REPORTS
        app.MapGet($"{prefix}/api/game/{{id:int}}/reports", async (int id) =>
        {
            await using var conn = new NpgsqlConnection(Program.ZamboniConfig.DatabaseConnectionString);
            await conn.OpenAsync();

            return Results.Json(await ReadRows(conn, @"
                SELECT * FROM reports_vs WHERE game_id=@id
                UNION ALL
                SELECT * FROM reports_so WHERE game_id=@id
            ", new NpgsqlParameter("id", id)));
        });

        // GAME SUMMARY
        app.MapGet($"{prefix}/api/games/{{id:int}}/summary", async (int id) =>
        {
            await using var conn = new NpgsqlConnection(Program.ZamboniConfig.DatabaseConnectionString);
            await conn.OpenAsync();

            var reports = await ReadRows(conn, @"
                SELECT * FROM reports_vs WHERE game_id=@id
                UNION ALL
                SELECT * FROM reports_so WHERE game_id=@id
            ", new NpgsqlParameter("id", id));

            if (reports.Count == 0)
                return Results.NotFound();

            var home = reports.Where(r => Convert.ToBoolean(r["home"])).ToList();
            var away = reports.Where(r => !Convert.ToBoolean(r["home"])).ToList();

            int homeScore = home.Sum(r => Convert.ToInt32(r["scor"] ?? 0));
            int awayScore = away.Sum(r => Convert.ToInt32(r["scor"] ?? 0));

            string? winner =
                homeScore > awayScore ? home.FirstOrDefault()?["team_name"]?.ToString() :
                awayScore > homeScore ? away.FirstOrDefault()?["team_name"]?.ToString() :
                null;

            return Results.Json(new
            {
                gameId = id,
                homeTeam = home.FirstOrDefault()?["team_name"],
                awayTeam = away.FirstOrDefault()?["team_name"],
                homeScore,
                awayScore,
                winnerTeam = winner,
                reports
            });
        });

        // LEADERBOARD
        app.MapGet($"{prefix}/api/leaderboard/{{range}}", async (string range) =>
        {
            var from = RangeToDate(range);

            await using var conn = new NpgsqlConnection(Program.ZamboniConfig.DatabaseConnectionString);
            await conn.OpenAsync();

            var sql = @"
                SELECT gtag, SUM(scor) AS total_goals, COUNT(*) AS games_played
                FROM (
                    SELECT gtag, scor, created_at FROM reports_vs
                    UNION ALL
                    SELECT gtag, scor, created_at FROM reports_so
                ) x
                WHERE (@from = '0001-01-01'::timestamp OR created_at >= @from)
                GROUP BY gtag
                ORDER BY total_goals DESC
            ";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("from", from);

            var list = new List<object>();
            using var r = await cmd.ExecuteReaderAsync();

            int rank = 1;
            while (await r.ReadAsync())
            {
                list.Add(new
                {
                    gamertag = r["gtag"],
                    totalGoals = r["total_goals"],
                    gamesPlayed = r["games_played"],
                    rank = rank++
                });
            }

            return Results.Json(list);
        });

        // STATS
        app.MapGet($"{prefix}/api/stats/global", async () =>
        {
            await using var conn = new NpgsqlConnection(Program.ZamboniConfig.DatabaseConnectionString);
            await conn.OpenAsync();

            var games = Convert.ToInt32((await new NpgsqlCommand("SELECT COUNT(*) FROM games", conn).ExecuteScalarAsync())!);
            var reports = Convert.ToInt32((await new NpgsqlCommand("SELECT COUNT(*) FROM reports_vs", conn).ExecuteScalarAsync())!)
                        + Convert.ToInt32((await new NpgsqlCommand("SELECT COUNT(*) FROM reports_so", conn).ExecuteScalarAsync())!);

            var players = Convert.ToInt32((await new NpgsqlCommand(@"
                SELECT COUNT(DISTINCT gtag) FROM (
                    SELECT gtag FROM reports_vs
                    UNION
                    SELECT gtag FROM reports_so
                ) x", conn).ExecuteScalarAsync())!);

            return Results.Json(new
            {
                totalGames = games,
                totalReports = reports,
                totalPlayers = players
            });
        });

        app.MapGet($"{prefix}/api/stats/active", () =>
        {
            return Results.Json(new
            {
                serverVersion = Program.Name,
                onlineUsersCount = ServerManager.GetServerPlayers().Count,
                queuedUsers = ServerManager.GetQueuedPlayers().Count,
                activeGames = ServerManager.GetServerGames().Count
            });
        });

        // LATEST REPORTS
        app.MapGet($"{prefix}/api/reports/latest", async (int? limit) =>
        {
            int max = Math.Clamp(limit ?? 50, 1, 500);

            await using var conn = new NpgsqlConnection(Program.ZamboniConfig.DatabaseConnectionString);
            await conn.OpenAsync();

            return Results.Json(await ReadRows(conn, $@"
                SELECT * FROM (
                    SELECT * FROM reports_vs
                    UNION ALL
                    SELECT * FROM reports_so
                ) x
                ORDER BY created_at DESC
                LIMIT {max}
            "));
        });

        // USER HISTORY
        app.MapGet($"{prefix}/api/user/{{id:long}}/history", async (long id) =>
        {
            await using var conn = new NpgsqlConnection(Program.ZamboniConfig.DatabaseConnectionString);
            await conn.OpenAsync();

            var userRows = await ReadRows(conn, @"
                SELECT * FROM reports_vs WHERE user_id=@id
                UNION ALL
                SELECT * FROM reports_so WHERE user_id=@id
            ", new NpgsqlParameter("id", id));

            if (userRows.Count == 0)
                return Results.Json(Array.Empty<object>());

            var gameIds = userRows.Select(r => Convert.ToInt64(r["game_id"])).Distinct().ToArray();

            var oppRows = await ReadRows(conn, $@"
                SELECT * FROM reports_vs WHERE game_id = ANY(@ids)
                UNION ALL
                SELECT * FROM reports_so WHERE game_id = ANY(@ids)
            ", new NpgsqlParameter("ids", gameIds));

            foreach (var r in userRows)
            {
                var opp = oppRows.FirstOrDefault(o =>
                    Convert.ToInt64(o["game_id"]) == Convert.ToInt64(r["game_id"]) &&
                    Convert.ToInt64(o["user_id"]) != Convert.ToInt64(r["user_id"])
                );

                if (opp != null)
                {
                    r["opponent"] = opp["gtag"];
                    r["opponent_team"] = opp["team_name"];
                    r["opponent_score"] = opp["scor"];
                }
            }

            return Results.Json(userRows);
        });

        await app.RunAsync();
    }
}

public class FixedWindowRateLimiter
{
    private readonly int _permitLimit;
    private readonly TimeSpan _window;
    private readonly int _queueLimit;
    private readonly ConcurrentQueue<DateTime> _requests = new();

    public FixedWindowRateLimiter(int permitLimit, TimeSpan window, int queueLimit)
    {
        _permitLimit = permitLimit;
        _window = window;
        _queueLimit = queueLimit;
    }

    public Task<bool> AllowRequestAsync()
    {
        var now = DateTime.UtcNow;

        while (_requests.TryPeek(out var ts) && now - ts > _window)
            _requests.TryDequeue(out _);

        if (_requests.Count >= _permitLimit + _queueLimit)
            return Task.FromResult(false);

        _requests.Enqueue(now);
        return Task.FromResult(true);
    }
}