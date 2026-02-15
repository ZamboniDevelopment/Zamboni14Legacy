using Tdf;

namespace Zamboni14Legacy.Components.NHL14Legacy.Structs.Messaging;

[TdfStruct]
public struct MessagePart
{
    [TdfMember("DATA")] 
    public string mData;

    [TdfMember("DURN")] 
    public int mDuration;
}