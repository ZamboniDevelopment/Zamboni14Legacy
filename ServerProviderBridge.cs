using ZamboniUltimateTeam;

namespace Zamboni14Legacy;

public class ServerProviderBridge : IServerProvider
{
    public long GetUserIdByConnectionId(long connectionId)
    {
        return 301116;
    }

    public string GetUserNameByUserId(long userId)
    {
        return "Kaap0";
    }
}