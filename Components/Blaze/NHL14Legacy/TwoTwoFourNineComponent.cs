using BlazeCommon;

namespace Zamboni14Legacy.Components.Blaze.NHL14Legacy;

internal class TwoTwoFourNineComponent : TwoTwoFourNineComponentBase.Server
{
    public override Task<NullStruct> OneAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new NullStruct());
    }

    public override Task<NullStruct> TwoAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new NullStruct());
    }
}