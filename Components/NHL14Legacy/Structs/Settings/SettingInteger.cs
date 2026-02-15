using Tdf;

namespace Zamboni14Legacy.Components.NHL14Legacy.Structs.Settings;

[TdfStruct]
public struct SettingInteger
{
    [TdfMember("DEF")] 
    public uint mDefault;

    [TdfMember("HLAB")] 
    public string mHelpLabel;

    [TdfMember("ID")] 
    public string mId;

    [TdfMember("LABL")] 
    public string mLabel;

    [TdfMember("LOCF")] 
    public uint mLocalizedFields;

    [TdfMember("MPVL")] 
    public SortedDictionary<uint,string> mPossibleValueMap;

    [TdfMember("TOGG")] 
    public uint mToggles;
    
}