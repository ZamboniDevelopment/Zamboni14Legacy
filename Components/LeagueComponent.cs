using Blaze3SDK.Blaze.League;
using Blaze3SDK.Components;
using BlazeCommon;

namespace Zamboni14Legacy.Components;

internal class LeagueComponent : LeagueComponentBase.Server
{
    public override Task<NullStruct> GetLeaguesByUserAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new NullStruct
        {
        });
    }

    public override Task<NullStruct> GetInvitationsAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new NullStruct());
    }
}