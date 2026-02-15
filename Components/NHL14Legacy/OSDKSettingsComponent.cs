using BlazeCommon;
using Zamboni14Legacy.Components.NHL14Legacy.Bases;
using Zamboni14Legacy.Components.NHL14Legacy.Responses;
using Zamboni14Legacy.Components.NHL14Legacy.Structs.Settings;

namespace Zamboni14Legacy.Components.NHL14Legacy;

internal class OSDKSettingsComponent : OSDKSettingsComponentBase.Server
{
    public override Task<FetchSettingsResponse> FetchSettingsAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new FetchSettingsResponse
        {
            mStringSettingList = new List<SettingString>
            {
                new SettingString
                {
                    mId = "O_TKfilter",
                }
            }
        });
    }

    public override Task<FetchSettingsGroupsResponse> FetchSettingsGroupsAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new FetchSettingsGroupsResponse
        {
            mSettingGroupList = new List<SettingGroup>
            {
                new SettingGroup
                {
                    mId = "O_SG_TCKR",
                    mSettingList = new List<string>
                    {
                        "O_TKfilter"
                    }
                }
            }
        });
    }
}