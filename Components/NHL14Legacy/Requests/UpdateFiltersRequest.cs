using Tdf;
using Zamboni14Legacy.Components.NHL14Legacy.Structs.Ticker;

namespace Zamboni14Legacy.Components.NHL14Legacy.Requests;

[TdfStruct]
public struct UpdateFiltersRequest
{
    [TdfMember("FILT")] 
    public TickerFilter mTickerFilter;

    [TdfMember("IDEN")] 
    public ulong mBlazeId;
}