using Tdf;
using Zamboni14Legacy.Components.NHL14Legacy.Structs.Ticker;

namespace Zamboni14Legacy.Components.NHL14Legacy.Responses;

[TdfStruct]
public struct GetMessagesResponse
{
    
    [TdfMember("DATA")] 
    public List<TickerMessage> mData;
    
}