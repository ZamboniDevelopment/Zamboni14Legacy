using Blaze3SDK;
using BlazeCommon;
using NLog;

namespace Zamboni14Legacy.Components.Blaze.NHL14Legacy;

public static class TwoTwoFiveOneComponentBase
{
    public enum TwoTwoFiveOneComponentCommand : ushort
    {
        oneOneTwo = 112
    }

    public enum TwoTwoFiveOneComponentNotification : ushort
    {
    }

    public const ushort Id = 2251;
    public const string Name = "TwoTwoFiveOneComponent";

    public static Type GetCommandRequestType(TwoTwoFiveOneComponentCommand command)
    {
        return command switch
        {
            TwoTwoFiveOneComponentCommand.oneOneTwo => typeof(NullStruct),
            _ => typeof(NullStruct)
        };
    }

    public static Type GetCommandResponseType(TwoTwoFiveOneComponentCommand command)
    {
        return command switch
        {
            TwoTwoFiveOneComponentCommand.oneOneTwo => typeof(NullStruct),
            _ => typeof(NullStruct)
        };
    }

    public static Type GetCommandErrorResponseType(TwoTwoFiveOneComponentCommand command)
    {
        return command switch
        {
            TwoTwoFiveOneComponentCommand.oneOneTwo => typeof(NullStruct),
            _ => typeof(NullStruct)
        };
    }

    public static Type GetNotificationType(TwoTwoFiveOneComponentNotification notification)
    {
        return notification switch
        {
            _ => typeof(NullStruct)
        };
    }

    public class Server : BlazeServerComponent<TwoTwoFiveOneComponentCommand, TwoTwoFiveOneComponentNotification, Blaze3RpcError>
    {
        public Server() : base(TwoTwoFiveOneComponentBase.Id, TwoTwoFiveOneComponentBase.Name)
        {
        }

        [BlazeCommand((ushort)TwoTwoFiveOneComponentCommand.oneOneTwo)]
        public virtual Task<NullStruct> OneOneTwoAsync(NullStruct request, BlazeRpcContext context)
        {
            throw new BlazeRpcException(Blaze3RpcError.ERR_COMMAND_NOT_FOUND);
        }

        public override Type GetCommandRequestType(TwoTwoFiveOneComponentCommand command)
        {
            return TwoTwoFiveOneComponentBase.GetCommandRequestType(command);
        }

        public override Type GetCommandResponseType(TwoTwoFiveOneComponentCommand command)
        {
            return TwoTwoFiveOneComponentBase.GetCommandResponseType(command);
        }

        public override Type GetCommandErrorResponseType(TwoTwoFiveOneComponentCommand command)
        {
            return TwoTwoFiveOneComponentBase.GetCommandErrorResponseType(command);
        }

        public override Type GetNotificationType(TwoTwoFiveOneComponentNotification notification)
        {
            return TwoTwoFiveOneComponentBase.GetNotificationType(notification);
        }
    }

    public class Client : BlazeClientComponent<TwoTwoFiveOneComponentCommand, TwoTwoFiveOneComponentNotification, Blaze3RpcError>
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public Client(BlazeClientConnection connection) : base(TwoTwoFiveOneComponentBase.Id, TwoTwoFiveOneComponentBase.Name)
        {
            Connection = connection;
            if (!Connection.Config.AddComponent(this))
                throw new InvalidOperationException($"A component with Id({Id}) has already been created for the connection.");
        }

        private BlazeClientConnection Connection { get; }


        public NullStruct OneOneTwo(NullStruct request)
        {
            return Connection.SendRequest<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoFiveOneComponentCommand.oneOneTwo, request);
        }

        public Task<NullStruct> OneOneTwoAsync(NullStruct request)
        {
            return Connection.SendRequestAsync<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoFiveOneComponentCommand.oneOneTwo, request);
        }


        public override Type GetCommandRequestType(TwoTwoFiveOneComponentCommand command)
        {
            return TwoTwoFiveOneComponentBase.GetCommandRequestType(command);
        }

        public override Type GetCommandResponseType(TwoTwoFiveOneComponentCommand command)
        {
            return TwoTwoFiveOneComponentBase.GetCommandResponseType(command);
        }

        public override Type GetCommandErrorResponseType(TwoTwoFiveOneComponentCommand command)
        {
            return TwoTwoFiveOneComponentBase.GetCommandErrorResponseType(command);
        }

        public override Type GetNotificationType(TwoTwoFiveOneComponentNotification notification)
        {
            return TwoTwoFiveOneComponentBase.GetNotificationType(notification);
        }
    }

    public class Proxy : BlazeProxyComponent<TwoTwoFiveOneComponentCommand, TwoTwoFiveOneComponentNotification, Blaze3RpcError>
    {
        public Proxy() : base(TwoTwoFiveOneComponentBase.Id, TwoTwoFiveOneComponentBase.Name)
        {
        }

        [BlazeCommand((ushort)TwoTwoFiveOneComponentCommand.oneOneTwo)]
        public virtual Task<NullStruct> FetchSettingsAsync(NullStruct request, BlazeProxyContext context)
        {
            return context.ClientConnection.SendRequestAsync<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoFiveOneComponentCommand.oneOneTwo, request);
        }

        public override Type GetCommandRequestType(TwoTwoFiveOneComponentCommand command)
        {
            return TwoTwoFiveOneComponentBase.GetCommandRequestType(command);
        }

        public override Type GetCommandResponseType(TwoTwoFiveOneComponentCommand command)
        {
            return TwoTwoFiveOneComponentBase.GetCommandResponseType(command);
        }

        public override Type GetCommandErrorResponseType(TwoTwoFiveOneComponentCommand command)
        {
            return TwoTwoFiveOneComponentBase.GetCommandErrorResponseType(command);
        }

        public override Type GetNotificationType(TwoTwoFiveOneComponentNotification notification)
        {
            return TwoTwoFiveOneComponentBase.GetNotificationType(notification);
        }
    }
}