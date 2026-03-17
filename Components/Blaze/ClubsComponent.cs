using Blaze3SDK.Blaze.Clubs;
using Blaze3SDK.Components;
using BlazeCommon;

namespace Zamboni14Legacy.Components.Blaze;

internal class ClubsComponent : ClubsComponentBase.Server
{
    public override Task<ClubsComponentSettings> GetClubsComponentSettingsAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new ClubsComponentSettings());
    }
    
    public override Task<NullStruct> GetClubsAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new NullStruct());
    }
    
    public override Task<NullStruct> GetClubMembershipForUsersAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new NullStruct());
    }
    
}