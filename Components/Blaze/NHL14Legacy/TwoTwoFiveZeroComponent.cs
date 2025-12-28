using BlazeCommon;

namespace Zamboni14Legacy.Components.Blaze.NHL14Legacy;

internal class TwoTwoFiveZeroComponent : TwoTwoFiveZeroComponentBase.Server
{
    public override Task<NullStruct> TwoAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new NullStruct());
    }
}