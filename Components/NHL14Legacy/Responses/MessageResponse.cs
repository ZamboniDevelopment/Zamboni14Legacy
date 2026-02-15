using Tdf;
using Zamboni14Legacy.Components.NHL14Legacy.Structs.Messaging;

namespace Zamboni14Legacy.Components.NHL14Legacy.Responses;

[TdfStruct]
public struct MessageResponse
{
    [TdfMember("ENUM")] 
    public DynamicMessageEnum mDynamicMessageEnum;

    [TdfMember("MSGS")] 
    public List<MessageItem> mMessagesList;
}