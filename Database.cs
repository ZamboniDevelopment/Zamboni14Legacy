using System.Collections;
using System.Reflection;
using Blaze3SDK.Blaze.GameReporting;
using NLog;
using Npgsql;
using Tdf;
using Zamboni14Legacy.Components.Blaze.NHL14Legacy.Report;

namespace Zamboni14Legacy;

public class Database
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly string connectionString = Program.ZamboniConfig.DatabaseConnectionString;
    public readonly bool isEnabled;

    private ulong fallbackGameIdCounter = 1;

    public Database()
    {
        try
        {
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            isEnabled = true;
            Logger.Warn("Database is accessible.");
        }
        catch (Exception)
        {
            isEnabled = false;
            Logger.Warn("Database is not accessible. Gamedata wont be saved");
            return;
        }

        CreateGameIdSequence();
        CreateGamesTable();
        CreateReportTable();
        CreateSoReportTable();
    }

    private void CreateGameIdSequence()
    {
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();

        const string createSequenceQuery = @"
            CREATE SEQUENCE IF NOT EXISTS zamboni_game_id_seq
            START 1
            INCREMENT 1;
        ";

        using var cmd = new NpgsqlCommand(createSequenceQuery, conn);
        cmd.ExecuteNonQuery();
    }

    private void CreateGamesTable()
    {
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();

        const string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS games (
                    game_id NUMERIC(20,0) PRIMARY KEY,
                    gtyp VARCHAR,
                    arid NUMERIC(20,0),
                    cbid BIGINT,
                    ct_id BIGINT,
                    grid NUMERIC(20,0),
                    gtim BIGINT,
                    isim BOOLEAN,
                    lgid BIGINT,
                    nper INTEGER,
                    ovrt BIGINT,
                    plen INTEGER,
                    rank BOOLEAN,
                    roid BIGINT,
                    seid BIGINT,
                    shoo BIGINT,
                    skil INTEGER,
                    sku INTEGER,
                -- stus shouldn't be used as a reliable way to tell the status of the game, rely on reports table cdnf value or other means
                    stus BIGINT,
                    type VARCHAR,
                    venu INTEGER,
                    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );";

        using var cmd = new NpgsqlCommand(createTableQuery, conn);
        cmd.ExecuteNonQuery();
    }

    private void CreateReportTable()
    {
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();

        const string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS reports_vs (
                    game_id NUMERIC(20,0) NOT NULL,
                    user_id NUMERIC(20,0) NOT NULL,
                    
                    cdnf BIGINT,
                    cht INTEGER,
                    
                    bag BIGINT,
                    bao BIGINT,
                    bs BIGINT,
                    fo BIGINT,
                    fol BIGINT,
                    hits BIGINT,
                    loga BIGINT,
                    logf BIGINT,
                    otg BIGINT,
                    oto BIGINT,
                    pims BIGINT,
                    ppg BIGINT,
                    ppga BIGINT,
                    ppo BIGINT,
                    psa BIGINT,
                    psg BIGINT,
                    pssa BIGINT,
                    pssc BIGINT,
                    shg BIGINT,
                    shga BIGINT,
                    shta BIGINT,
                    shts BIGINT,
                    sklv BIGINT,
                    so BIGINT,
                    toa BIGINT,
                    tsh BIGINT,
                    wiga BIGINT,
                    wigf BIGINT,
                    
                    csco BIGINT,
                    ctry INTEGER,
                    disc INTEGER,
                    fhrn BIGINT,
                    grlt BIGINT,
                    gtag VARCHAR,
                    home BOOLEAN,
                    loss BIGINT,
                    name VARCHAR,
                    opct BIGINT,
                    
                    bandavggm BIGINT,
                    bandavgnet BIGINT,
                    bandhigm BIGINT,
                    bandhinet BIGINT,
                    bandlowgm BIGINT,
                    bandlownet BIGINT,
                    bytesrcvdgm BIGINT,
                    bytesrcvdnet BIGINT,
                    bytessentgm BIGINT,
                    bytessentnet BIGINT,
                    droppkts BIGINT,
                    fpsavg BIGINT,
                    fpsdev BIGINT,
                    fpshi BIGINT,
                    fpslow BIGINT,
                    gdesyncend BIGINT,
                    gdesyncrsn BIGINT,
                    gendphase BIGINT,
                    gresult BIGINT,
                    grpttype BIGINT,
                    grptver BIGINT,
                    guests0 BIGINT,
                    guests1 BIGINT,
                    lateavggm BIGINT,
                    lateavgnet BIGINT,
                    latehigm BIGINT,
                    latehinet BIGINT,
                    latelowgm BIGINT,
                    latelownet BIGINT,
                    latesdevgm BIGINT,
                    latesdevnet BIGINT,
                    pktloss BIGINT,
                    usersend0 BIGINT,
                    usersend1 BIGINT,
                    usersstrt0 BIGINT,
                    usersstrt1 BIGINT,
                    voipend0 BIGINT,
                    voipend1 BIGINT,
                    voipstrt0 BIGINT,
                    voipstrt1 BIGINT,
                    
                    otl BIGINT,
                    peid NUMERIC(20,0),
                    pnid NUMERIC(20,0),
                    ppna VARCHAR,
                    ptag BIGINT,
                    quit INTEGER,
                    relt BIGINT,
                    scor BIGINT,
                    serg INTEGER,
                    skil BIGINT,
                    skpt BIGINT,
                    team BIGINT,
                    ties BIGINT,
                    tnam VARCHAR,
                    wdnf BIGINT,
                    wght BIGINT,
                    wins BIGINT,
                    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    PRIMARY KEY (game_id, user_id)
                );";

        using var cmd = new NpgsqlCommand(createTableQuery, conn);
        cmd.ExecuteNonQuery();
    }

    private void CreateSoReportTable()
    {
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();

        const string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS reports_so (
                    game_id NUMERIC(20,0) NOT NULL,
                    user_id NUMERIC(20,0) NOT NULL,
                    
                    cdnf BIGINT,
                    cht INTEGER,

                    ga BIGINT,
                    gf BIGINT,
                    shta BIGINT,
                    shts BIGINT,
                    sklv BIGINT,
                    
                    csco BIGINT,
                    ctry INTEGER,
                    disc INTEGER,
                    fhrn BIGINT,
                    grlt BIGINT,
                    gtag VARCHAR,
                    home BOOLEAN,
                    loss BIGINT,
                    name VARCHAR,
                    opct BIGINT,
                    
                    bandavggm BIGINT,
                    bandavgnet BIGINT,
                    bandhigm BIGINT,
                    bandhinet BIGINT,
                    bandlowgm BIGINT,
                    bandlownet BIGINT,
                    bytesrcvdgm BIGINT,
                    bytesrcvdnet BIGINT,
                    bytessentgm BIGINT,
                    bytessentnet BIGINT,
                    droppkts BIGINT,
                    fpsavg BIGINT,
                    fpsdev BIGINT,
                    fpshi BIGINT,
                    fpslow BIGINT,
                    gdesyncend BIGINT,
                    gdesyncrsn BIGINT,
                    gendphase BIGINT,
                    gresult BIGINT,
                    grpttype BIGINT,
                    grptver BIGINT,
                    guests0 BIGINT,
                    guests1 BIGINT,
                    lateavggm BIGINT,
                    lateavgnet BIGINT,
                    latehigm BIGINT,
                    latehinet BIGINT,
                    latelowgm BIGINT,
                    latelownet BIGINT,
                    latesdevgm BIGINT,
                    latesdevnet BIGINT,
                    pktloss BIGINT,
                    usersend0 BIGINT,
                    usersend1 BIGINT,
                    usersstrt0 BIGINT,
                    usersstrt1 BIGINT,
                    voipend0 BIGINT,
                    voipend1 BIGINT,
                    voipstrt0 BIGINT,
                    voipstrt1 BIGINT,
                    
                    otl BIGINT,
                    peid NUMERIC(20,0),
                    pnid NUMERIC(20,0),
                    ppna VARCHAR,
                    ptag BIGINT,
                    quit INTEGER,
                    relt BIGINT,
                    scor BIGINT,
                    serg INTEGER,
                    skil BIGINT,
                    skpt BIGINT,
                    team BIGINT,
                    ties BIGINT,
                    tnam VARCHAR,
                    wdnf BIGINT,
                    wght BIGINT,
                    wins BIGINT,
                    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    PRIMARY KEY (game_id, user_id)
                );";

        using var cmd = new NpgsqlCommand(createTableQuery, conn);
        cmd.ExecuteNonQuery();
    }


    public void InsertReport(SubmitGameReportRequest request)
    {
        InsertGameData(request);
        InsertReportData(request);
    }

    public void InsertGameData(SubmitGameReportRequest request)
    {
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();

        var gameId = (decimal)request.mGameReport.mGameReportingId;
        var gameType = request.mGameReport.mGameTypeName;
        var reportData = ((Report)request.mGameReport.mReport).mGameInfoReport;

        const string insertMainQuery = @"
            INSERT INTO games (game_id, gtyp) VALUES (@game_id, @gtyp)
            ON CONFLICT (game_id) DO NOTHING;";

        using (var cmd = new NpgsqlCommand(insertMainQuery, conn))
        {
            cmd.Parameters.AddWithValue("game_id", gameId);
            cmd.Parameters.AddWithValue("gtyp", gameType);
            cmd.ExecuteNonQuery();
        }

        ProcessObject(conn, "games", reportData, gameId);
    }

    private void ProcessObject(NpgsqlConnection conn, string table, object? obj, decimal gameId, long? userId = null)
    {
        if (obj == null) return;
        foreach (var field in obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            var value = field.GetValue(obj);
            if (value == null) continue;

            if (value is IDictionary dict)
            {
                foreach (DictionaryEntry entry in dict)
                    ExecuteDynamicUpsert(conn, table, entry.Key.ToString()!, entry.Value, gameId, userId);
                continue;
            }

            if (!field.FieldType.IsPrimitive && field.FieldType != typeof(string) && field.FieldType != typeof(decimal))
            {
                ProcessObject(conn, table, value, gameId, userId);
                continue;
            }

            var tag = field.GetCustomAttribute<TdfMember>()?.Tag;
            if (tag != null) ExecuteDynamicUpsert(conn, table, tag, value, gameId, userId);
        }
    }

    private void ExecuteDynamicUpsert(NpgsqlConnection conn, string table, string tag, object? value, decimal game_id, long? user_id)
    {
        var query = "";
        var column = tag.ToLower() == "ctid" ? "ct_id" : tag.ToLower(); //PSQL reserved column name "ctid" >:(

        if (table.Equals("games"))
            query = $@"
                INSERT INTO games (game_id, {column}) VALUES (@game_id, @value)
                ON CONFLICT (game_id) DO UPDATE SET {column} = EXCLUDED.{column};";
        else if (table.Equals("reports_vs"))
            query = $@"
                INSERT INTO reports_vs (game_id, user_id, {column}) VALUES (@game_id, @user_id, @value)
                ON CONFLICT (game_id, user_id) DO UPDATE SET {column} = EXCLUDED.{column};";
        else if (table.Equals("reports_so"))
            query = $@"
                INSERT INTO reports_so (game_id, user_id, {column}) VALUES (@game_id, @user_id, @value)
                ON CONFLICT (game_id, user_id) DO UPDATE SET {column} = EXCLUDED.{column};";

        using var cmd = new NpgsqlCommand(query, conn);
        cmd.Parameters.AddWithValue("game_id", game_id);
        if (user_id.HasValue) cmd.Parameters.AddWithValue("user_id", (decimal)user_id.Value);
        cmd.Parameters.AddWithValue("value", MapType(value));

        cmd.ExecuteNonQuery();
    }

    private void InsertReportData(SubmitGameReportRequest request)
    {
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();

        var table = request.mGameReport.mGameTypeName switch
        {
            "gameType1" => "reports_vs",
            "gameType2" => "reports_so",
            _ => throw new NotImplementedException($"Game type {request.mGameReport.mGameTypeName} is not mapped.")
        };

        var gameId = (decimal)request.mGameReport.mGameReportingId;
        var reportData = ((Report)request.mGameReport.mReport).mPlayerReports;

        foreach (var user_id in reportData.Keys)
        {
            var insertMainQuery = $@"
                INSERT INTO {table} (game_id, user_id) VALUES (@game_id, @user_id)
                ON CONFLICT (game_id, user_id) DO NOTHING;";

            using (var cmd = new NpgsqlCommand(insertMainQuery, conn))
            {
                cmd.Parameters.AddWithValue("game_id", gameId);
                cmd.Parameters.AddWithValue("user_id", user_id);
                cmd.ExecuteNonQuery();
            }

            ProcessObject(conn, table, reportData[user_id], gameId, user_id);
        }
    }

    private object MapType(object? val)
    {
        return val switch
        {
            ulong uLongValue => (decimal)uLongValue,
            uint uIntValue => (long)uIntValue,
            ushort uShortValue => (int)uShortValue,
            _ => val ?? DBNull.Value
        };
    }

    public ulong GetNextGameId()
    {
        if (!isEnabled) return fallbackGameIdCounter++;

        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();
        //TODO: PSQL overflows at 9 quintillion. Though game client cant receive a game id max of 18 quintillion.
        using var cmd = new NpgsqlCommand("SELECT nextval('zamboni_game_id_seq');", conn);
        var result = cmd.ExecuteScalar();

        if (result == null || result == DBNull.Value)
            throw new InvalidOperationException("Sequence returned no value.");

        return (ulong)(long)result;
    }
}