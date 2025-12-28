using System.Text;
using Blaze3SDK.Blaze;
using Blaze3SDK.Blaze.GameManager;
using Blaze3SDK.Components;

namespace Zamboni14Legacy;

public class ServerGame
{
    private readonly object _lockReplicatedPlayers = new();

    // public ServerGame(ServerPlayer host, StartMatchmakingRequest request, string gameMode)
    // {
    //     var gameId = Program.Database.GetNextGameId();
    //
    //     ReplicatedGameData = new ReplicatedGameData
    //     {
    //         mAdminPlayerList = new List<long>
    //         {
    //             host.UserIdentification.mBlazeId,
    //         },
    //         mGameAttribs = VsGameAttribs(),
    //         mSlotCapacities = Capacities("1"),
    //         mEntryCriteriaMap = new SortedDictionary<string, string>(),
    //         mGameId = gameId,
    //         mGameName = "game" + gameId,
    //         mGameProtocolVersionHash = GetGameProtocolVersionHash(request.mGameProtocolVersionString),
    //         mGameSettings = request.mGameSettings,
    //         mGameReportingId = gameId,
    //         mGameState = GameState.INITIALIZING,
    //         mGameTypeName = "game" + gameId,
    //         mGameStatusUrl = "",
    //         mHostNetworkAddressList = new List<NetworkAddress>
    //         {
    //             host.ExtendedData.mAddress
    //         },
    //         mTopologyHostSessionId = host.UserIdentification.mExternalId,
    //         mIgnoreEntryCriteriaWithInvite = true,
    //         mMeshAttribs = new SortedDictionary<string, string>(),
    //         mMaxPlayerCapacity = 2,
    //         mNetworkQosData = host.ExtendedData.mQosData,
    //         mServerNotResetable = false,
    //         mNetworkTopology = request.mNetworkTopology,
    //         mPersistedGameId = "game" + gameId,
    //         mPersistedGameIdSecret = new byte[]
    //         {
    //         },
    //         mPlatformHostInfo = new HostInfo
    //         {
    //             mPlayerId = host.UserIdentification.mBlazeId,
    //             mSlotId = 0
    //         },
    //         mPresenceMode = PresenceMode.PRESENCE_MODE_STANDARD,
    //         mPingSiteAlias = "qos",
    //         mQueueCapacity = 2,
    //         mSharedSeed = 2,
    //         mTeamCapacity = 2,
    //         mTopologyHostInfo = new HostInfo
    //         {
    //             mPlayerId = host.UserIdentification.mBlazeId,
    //             mSlotId = 0
    //         },
    //         mTeamIds = new List<ushort>(),
    //         mUUID = "",
    //         mVoipNetwork = VoipTopology.VOIP_DISABLED,
    //         mGameProtocolVersionString = request.mGameProtocolVersionString,
    //         mXnetNonce = new byte[]
    //         {
    //         },
    //         mXnetSession = new byte[]
    //         {
    //         }
    //     };
    //
    //     ServerManager.AddServerGame(this);
    // }

    public ServerGame(ServerPlayer host, CreateGameRequest request)
    {
        var gameId = Program.Database.GetNextGameId();

        ReplicatedGameData = new ReplicatedGameData
        {
            mAdminPlayerList = new List<long>
            {
                host.UserIdentification.mAccountId
            },
            mEntryCriteriaMap = request.mEntryCriteriaMap,
            mGameAttribs = request.mGameAttribs,
            mGameId = gameId,
            mGameName = "game" + gameId,
            mGameProtocolVersionHash = GetGameProtocolVersionHash(request.mGameProtocolVersionString),
            mGameProtocolVersionString = request.mGameProtocolVersionString,
            mGameReportingId = gameId,
            mGameSettings = request.mGameSettings,
            mGameState = GameState.INITIALIZING,
            mGameTypeName = request.mGameTypeName,
            mHostNetworkAddressList = new List<NetworkAddress>
            {
                host.ExtendedData.mAddress
            },
            mIgnoreEntryCriteriaWithInvite = request.mIgnoreEntryCriteriaWithInvite,
            mMaxPlayerCapacity = request.mMaxPlayerCapacity,
            mMeshAttribs = request.mMeshAttribs,
            mNetworkQosData = host.ExtendedData.mQosData,
            mNetworkTopology = GameNetworkTopology.PEER_TO_PEER_FULL_MESH, //TODO
            mPersistedGameId = gameId.ToString(),
            mPersistedGameIdSecret = request.mPersistedGameIdSecret,
            mPingSiteAlias = "qos",
            mPlatformHostInfo = new HostInfo
            {
                mPlayerId = host.UserIdentification.mAccountId,
                mSlotId = 0
            },
            mTopologyHostSessionId = (uint)host.UserIdentification.mAccountId,
            mPresenceMode = request.mPresenceMode,
            mQueueCapacity = request.mQueueCapacity,
            mServerNotResetable = request.mServerNotResetable,
            mSharedSeed = (uint)gameId,
            mSlotCapacities = request.mSlotCapacities,
            mTeamCapacity = request.mTeamCapacity,
            mTeamIds = request.mTeamIds,
            mTopologyHostInfo = new HostInfo
            {
                mPlayerId = host.UserIdentification.mAccountId,
                mSlotId = 0
            },
            mUUID = gameId.ToString(),
            mVoipNetwork = VoipTopology.VOIP_DISABLED,
            mXnetNonce = new byte[]
            {
            },
            mXnetSession = new byte[]
            {
            }
        };
        ServerManager.AddServerGame(this);
    }

    public List<ServerPlayer> ServerPlayers { get; } = new();
    public ReplicatedGameData ReplicatedGameData { get; set; }
    public List<ReplicatedGamePlayer> ReplicatedGamePlayers { get; set; } = new();

    private List<ushort> Capacities(string gameMode)
    {
        if (gameMode.Equals("3"))
            return new List<ushort>
            {
                0, 12
            };

        return new List<ushort>
        {
            0, 2
        };
    }

    public void AddGameParticipant(ServerPlayer serverPlayer, uint matchmakingSessionId = 0)
    {
        //TODO Lobby capacities?
        ServerPlayers.Add(serverPlayer);
        var replicatedGamePlayer = serverPlayer.ToReplicatedGamePlayer((byte)(ServerPlayers.Count - 1), ReplicatedGameData.mGameId);
        ReplicatedGamePlayers.Add(replicatedGamePlayer);

        GameManagerBase.Server.NotifyGameSetupAsync(serverPlayer.BlazeServerConnection, new NotifyGameSetup
        {
            mGameData = ReplicatedGameData,
            mGameRoster = ReplicatedGamePlayers
        });

        NotifyParticipants(new NotifyPlayerJoining
        {
            mGameId = ReplicatedGameData.mGameId,
            mJoiningPlayer = replicatedGamePlayer
        });
    }

    public bool HasSpaceForPlayer()
    {
        return ReplicatedGameData.mSlotCapacities[0] > ReplicatedGamePlayers.Count;
    }

    public void RemoveGameParticipant(ServerPlayer serverPlayer, PlayerRemovedReason reason, bool notifyOthers = true)
    {
        //Concurrent exception
        lock (_lockReplicatedPlayers)
        {
            ServerPlayers.Remove(serverPlayer);

            var replicatedPlayerToRemove = ReplicatedGamePlayers.Find(replicatedPlayer => replicatedPlayer.mPlayerName.Equals(serverPlayer.UserIdentification.mName));

            ReplicatedGamePlayers.Remove(replicatedPlayerToRemove);

            if (notifyOthers)
                NotifyParticipants(new NotifyPlayerRemoved
                {
                    mPlayerRemovedTitleContext = 0,
                    mGameId = ReplicatedGameData.mGameId,
                    mPlayerId = serverPlayer.UserIdentification.mBlazeId,
                    mPlayerRemovedReason = reason
                });

            if (ServerPlayers.Count == 0) ServerManager.RemoveServerGame(this);
        }
    }

    public void NotifyParticipants(NotifyGamePlayerStateChange playerStateChange)
    {
        foreach (var serverPlayer in ServerPlayers) GameManagerBase.Server.NotifyGamePlayerStateChangeAsync(serverPlayer.BlazeServerConnection, playerStateChange);
    }

    public void NotifyParticipants(NotifyPlayerJoinCompleted playerJoinCompleted)
    {
        foreach (var serverPlayer in ServerPlayers) GameManagerBase.Server.NotifyPlayerJoinCompletedAsync(serverPlayer.BlazeServerConnection, playerJoinCompleted);
    }

    public void NotifyParticipants(NotifyPlayerRemoved playerRemoved)
    {
        foreach (var serverPlayer in ServerPlayers) GameManagerBase.Server.NotifyPlayerRemovedAsync(serverPlayer.BlazeServerConnection, playerRemoved);
    }

    private void NotifyParticipants(NotifyPlayerJoining playerJoining)
    {
        foreach (var serverPlayer in ServerPlayers) GameManagerBase.Server.NotifyPlayerJoiningAsync(serverPlayer.BlazeServerConnection, playerJoining);
    }


    private SortedDictionary<string, string> VsGameAttribs()
    {
        return new SortedDictionary<string, string>
        {
            { "CreatedPlays", "1" },
            { "Fighting", "1" },
            { "Injuries", "1" },
            { "MudCompetitionId", "0" },
            { "MudCpuGame", "0" },
            { "MudGameId", "0" },
            { "OSDK_arenaChallengeId", "0" },
            { "OSDK_categoryId", "0" },
            { "OSDK_coop", "1" },
            { "OSDK_DDPToFullVersion", "0" },
            { "OSDK_gameMode", "1" },
            { "OSDK_roomId", "0" },
            { "OSDK_rosterURL", "" },
            { "OSDK_rosterVersion", "1xiPUG3Cn22f0H2Y7K1QkGUt4BtuKH2v7P3A2uNP5t3WWcV21S1Tv3" },
            { "OSDK_sponsoredEventId", "0" },
            { "Penalties", "1" },
            { "PeriodLength", "5" },
            { "Rules", "1" }
        };
    }

    public static ulong GetGameProtocolVersionHash(string protocolVersion)
    {
        protocolVersion ??= string.Empty;
        //FNV1 HASH - the same hashing logic is used in ea blaze for game protocol versions
        var buf = Encoding.UTF8.GetBytes(protocolVersion);
        var hash = 2166136261UL;
        foreach (var c in buf)
            hash = (hash * 16777619) ^ c;
        return hash;
    }

    public override string ToString()
    {
        return "Players: " +
               string.Join(", ", ServerPlayers.Select(serverPlayer => serverPlayer.UserIdentification.mName)) +
               " gameId:" + ReplicatedGameData.mGameId +
               " state: " + ReplicatedGameData.mGameState +
               " type (1 vs game 2 shootout, 3 otp): " + ReplicatedGameData.mGameAttribs["OSDK_gameMode"];
    }
}