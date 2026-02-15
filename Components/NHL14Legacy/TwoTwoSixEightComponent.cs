using BlazeCommon;
using Zamboni14Legacy.Components.NHL14Legacy.Bases;

namespace Zamboni14Legacy.Components.NHL14Legacy;

internal class TwoTwoSixEightComponent : TwoTwoSixEightComponentBase.Server
{
    public override Task<NullStruct> ThreeAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new NullStruct());
    }
}