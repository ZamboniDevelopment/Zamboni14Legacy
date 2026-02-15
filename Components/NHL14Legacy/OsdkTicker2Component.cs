using BlazeCommon;
using Zamboni14Legacy.Components.NHL14Legacy.Bases;
using Zamboni14Legacy.Components.NHL14Legacy.Requests;
using Zamboni14Legacy.Components.NHL14Legacy.Responses;
using Zamboni14Legacy.Components.NHL14Legacy.Structs.Ticker;

namespace Zamboni14Legacy.Components.NHL14Legacy;

internal class OsdkTicker2Component : OsdkTicker2ComponentBase.Server
{
    public override Task<RegisterResponse> RegisterArgsAsync(RegisterArgs request, BlazeRpcContext context)
    {
        return Task.FromResult(new RegisterResponse()
        {
            mNumMessages = 1
        });
    }

    public override Task<GetMessagesResponse> GetMessagesAsync(GetMessagesRequest request, BlazeRpcContext context)
    {
        return Task.FromResult(new GetMessagesResponse()
        {
            mData = new List<TickerMessage>
            {
                new TickerMessage
                {
                    mData = new List<string>
                    {
                        "Join Zamboni.gg/discord"
                    },
                    mENDT = 10,
                    mFilterIndex = 10,
                    mBlazeId = 10,
                    mPRIO = 10,
                    mPROV = "Kaap0",
                    mSTRT = 10,
                    mType = TickerMessageType.TYPE_NEWS
                }
            }
        });
    }
    
    public override Task<TickerFilter> UpdateFiltersAsync(UpdateFiltersRequest request, BlazeRpcContext context)
    {
        return Task.FromResult(new TickerFilter()
        {
            mBottom = request.mTickerFilter.mBottom,
            mTop = request.mTickerFilter.mTop
        });
    }
}