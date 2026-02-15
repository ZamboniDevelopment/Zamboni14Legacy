using BlazeCommon;
using Zamboni14Legacy.Components.NHL14Legacy.Bases;
using Zamboni14Legacy.Components.NHL14Legacy.Requests;
using Zamboni14Legacy.Components.NHL14Legacy.Responses;
using Zamboni14Legacy.Components.NHL14Legacy.Structs.Messaging;

namespace Zamboni14Legacy.Components.NHL14Legacy;

internal class OsdkDynamicMessagingComponent : OsdkDynamicMessagingComponentBase.Server
{
    public override Task<DynamicConfigResponse> GetConfigAsync(NullStruct request, BlazeRpcContext context)
    {
        return Task.FromResult(new DynamicConfigResponse
        {
            mDataRequestDelaySeconds = 100,
            mErrorRetryDelaySeconds = 100,
            mMessageDelayIntervalSeconds = 10,
            mMaximumMessageCount = 10
        });
    }

    public override Task<MessageResponse> GetMessagesAsync(MessageRequest request, BlazeRpcContext context)
    {
        return Task.FromResult(new MessageResponse
        {
            mDynamicMessageEnum = DynamicMessageEnum.DYNAMICMESSAGE_ENUM_SUCCESS,
            mMessagesList = new List<MessageItem>
            {
                new MessageItem
                {
                    mLinkData = "Sampletext A",
                    mFormat = DynamicMessageFormat.DYNAMICMESSAGE_FORMAT_PLAINTEXT,
                    mLinkHint = "Sampletext B",
                    mMessageId = 1,
                    mText = new List<MessagePart>
                    {
                        new MessagePart
                        {
                            mData = "Join Zamboni.gg/discord",
                            mDuration = 100
                        }
                    },
                    mTitle = "Sampletext D",
                    mLinkType = DynamicMessageType.DYNAMICMESSAGE_TYPE_MARKETPLACE
                }
            }
        });
    }
}