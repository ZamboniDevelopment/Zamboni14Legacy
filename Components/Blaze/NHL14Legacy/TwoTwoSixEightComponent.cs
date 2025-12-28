using BlazeCommon;

namespace Zamboni14Legacy.Components.Blaze.NHL14Legacy;

internal class TwoTwoSixEightComponent : TwoTwoSixEightComponentBase.Server
{
    public override Task<NullStruct> ThreeAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new NullStruct());
    }
}