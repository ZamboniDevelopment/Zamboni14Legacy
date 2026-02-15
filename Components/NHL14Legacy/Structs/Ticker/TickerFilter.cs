using Tdf;

namespace Zamboni14Legacy.Components.NHL14Legacy.Structs.Ticker;

[TdfStruct]
public struct TickerFilter
{
    [TdfMember("BOT_")] 
    public long mBottom;

    [TdfMember("TOP_")] 
    public long mTop;
    
}