using Tdf;
using Zamboni14Legacy.Components.NHL14Legacy.Structs.Settings;

namespace Zamboni14Legacy.Components.NHL14Legacy.Responses;

[TdfStruct]
public struct FetchSettingsResponse
{
    
    [TdfMember("LSIN")] 
    public List<SettingInteger> mIntegerSettingList;

    [TdfMember("LSST")] 
    public List<SettingString> mStringSettingList;
    
}