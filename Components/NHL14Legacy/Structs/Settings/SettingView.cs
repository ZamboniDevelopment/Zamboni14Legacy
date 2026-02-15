using Tdf;

namespace Zamboni14Legacy.Components.NHL14Legacy.Structs.Settings;

[TdfStruct]
public struct SettingView
{
    [TdfMember("ID")] 
    public string mID;

    [TdfMember("LVDS")] 
    public List<SettingViewData> mSettingViewDataList;
    
}