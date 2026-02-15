using Tdf;

namespace Zamboni14Legacy.Components.NHL14Legacy.Structs.Settings;

[TdfStruct]
public struct SettingGroup
{
    [TdfMember("ID")] 
    public string mId;

    [TdfMember("LSET")] 
    public List<string> mSettingList;

    [TdfMember("LVWS")] 
    public List<SettingView> mViewList;
    
}