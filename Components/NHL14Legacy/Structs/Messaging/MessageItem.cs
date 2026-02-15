using Tdf;

namespace Zamboni14Legacy.Components.NHL14Legacy.Structs.Messaging;

[TdfStruct]
public struct MessageItem
{
    [TdfMember("DATA")] 
    public string mLinkData;

    [TdfMember("FMAT")]
    public DynamicMessageFormat mFormat;

    [TdfMember("HINT")] 
    public string mLinkHint;

    [TdfMember("IMGS")] 
    public List<MessagePart> mImages;

    [TdfMember("MUID")]
    public int mMessageId;

    [TdfMember("TEXT")] 
    public List<MessagePart> mText;

    [TdfMember("TITL")]
    public string mTitle;

    [TdfMember("TYPE")] 
    public DynamicMessageType mLinkType;
}