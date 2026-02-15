using Tdf;

namespace Zamboni14Legacy.Components.NHL14Legacy.Requests;

[TdfStruct]
public struct GetMessagesRequest
{
    [TdfMember("IDEN")] 
    public ulong mBlazeId;

}