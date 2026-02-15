using Tdf;
using Zamboni14Legacy.Components.NHL14Legacy.Structs.Settings;

namespace Zamboni14Legacy.Components.NHL14Legacy.Responses;

[TdfStruct]
public struct FetchSettingsGroupsResponse
{
    
    [TdfMember("LGRP")] 
    public List<SettingGroup> mSettingGroupList;

}