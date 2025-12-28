using Blaze3SDK.Blaze.GameReporting;
using Blaze3SDK.Components;
using BlazeCommon;

namespace Zamboni14Legacy.Components.Blaze;

internal class GameReportingComponent : GameReportingComponentBase.Server
{
    public override Task<NullStruct> SubmitGameReportAsync(SubmitGameReportRequest request, BlazeRpcContext context)
    {
        if (Program.Database.isEnabled) Program.Database.InsertReport(request);

        Task.Run(async () =>
        {
            await Task.Delay(10);
            NotifyResultNotificationAsync(context.BlazeConnection, new ResultNotification
            {
                mBlazeError = 0,
                mFinalResult = true,
                mGameHistoryId = request.mGameReport.mGameReportingId,
                mGameReportingId = request.mGameReport.mGameReportingId
            });
        });

        return Task.FromResult(new NullStruct());
    }
}