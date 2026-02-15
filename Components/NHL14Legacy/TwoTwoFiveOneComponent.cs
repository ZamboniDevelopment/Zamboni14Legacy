using BlazeCommon;
using Zamboni14Legacy.Components.NHL14Legacy.Bases;

namespace Zamboni14Legacy.Components.NHL14Legacy;

internal class TwoTwoFiveOneComponent : TwoTwoFiveOneComponentBase.Server
{
    public override Task<NullStruct> OneOneTwoAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new NullStruct());
    }
}