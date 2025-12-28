using Blaze3SDK.Blaze;
using Blaze3SDK.Components;
using BlazeCommon;

namespace Zamboni14Legacy.Components.Blaze;

internal class UserSessionsComponent : UserSessionsBase.Server
{
    public override Task<NullStruct> UpdateNetworkInfoAsync(NetworkInfo request, BlazeRpcContext context)
    {
        _ = Task.Run(async () =>
        {
            await Task.Delay(5000);

            var serverPlayer = ServerManager.GetServerPlayer(context.BlazeConnection);
            if (serverPlayer == null) return;

            var serverPlayerExtendedData = serverPlayer.ExtendedData;
            serverPlayerExtendedData.mAddress = request.mAddress;
            serverPlayerExtendedData.mQosData = request.mQosData;
            serverPlayerExtendedData.mBestPingSiteAlias = "qos";
            serverPlayer.ExtendedData = serverPlayerExtendedData;


            await NotifyUserSessionExtendedDataUpdateAsync(context.BlazeConnection, new UserSessionExtendedDataUpdate
            {
                mExtendedData = new List<UserSessionExtendedData>
                {
                    serverPlayerExtendedData
                }
                // mUserId = serverPlayer.UserIdentification.mBlazeId
            });
        });

        return Task.FromResult(new NullStruct());
    }

    // public override Task<NullStruct> UpdateExtendedDataAttributeAsync(NullStruct request, BlazeRpcContext context)
    // {
    //     //This was fired only once?
    //     return Task.FromResult(new NullStruct());
    // }

    public override Task<NullStruct> UpdateHardwareFlagsAsync(UpdateHardwareFlagsRequest request, BlazeRpcContext context)
    {
        return Task.FromResult(new NullStruct());
    }

    public override Task<UserData> LookupUserAsync(UserIdentification request, BlazeRpcContext context)
    {
        var target = ServerManager.GetServerPlayer(request.mName);
        if (target == null) return Task.FromResult(new UserData());
        return Task.FromResult(new UserData
        {
            mExtendedData = target.ExtendedData,
            mStatusFlags = UserDataFlags.Online,
            mUserInfo = target.UserIdentification
        });
    }

    public override Task<UserDataResponse> LookupUsersAsync(LookupUsersRequest request, BlazeRpcContext context)
    {
        var response = new UserDataResponse();
        response.mUserDataList = new List<UserData>();

        if (request.mLookupType.Equals(LookupUsersRequest.LookupType.PERSONA_NAME))
            foreach (var variable in request.mUserIdentificationList)
            {
                var serverPlayer = ServerManager.GetServerPlayer(variable.mName);
                if (serverPlayer != null)
                    response.mUserDataList.Add(new UserData
                    {
                        mExtendedData = serverPlayer.ExtendedData,
                        mStatusFlags = UserDataFlags.Online,
                        mUserInfo = serverPlayer.UserIdentification
                    });
            }

        return Task.FromResult(response);
    }
}