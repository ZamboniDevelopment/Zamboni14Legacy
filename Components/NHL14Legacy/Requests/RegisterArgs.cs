using Tdf;
using Zamboni14Legacy.Components.NHL14Legacy.Structs.Ticker;

namespace Zamboni14Legacy.Components.NHL14Legacy.Requests;

[TdfStruct]
public struct RegisterArgs
{
    
    [TdfMember("FILT")] 
    public TickerFilter mFilter;
    
    [TdfMember("IDEN")] 
    public ulong mBlazeId;
    
    [TdfMember("LANG")] 
    public string mLanguage;
    
    [TdfMember("PLAT")] 
    public string mPlatform;
    
    [TdfMember("REGN")] 
    public string mRegion;
    
    [TdfMember("TITL")] 
    public string mTitle;
    
    [TdfMember("USER")] 
    public string mUser;
    
}