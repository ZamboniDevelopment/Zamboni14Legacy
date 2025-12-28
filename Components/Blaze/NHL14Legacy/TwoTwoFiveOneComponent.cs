using BlazeCommon;

namespace Zamboni14Legacy.Components.Blaze.NHL14Legacy;

internal class TwoTwoFiveOneComponent : TwoTwoFiveOneComponentBase.Server
{
    public override Task<NullStruct> OneOneTwoAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new NullStruct());
    }
}