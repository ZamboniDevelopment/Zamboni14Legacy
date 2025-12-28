using Blaze3SDK;
using BlazeCommon;
using NLog;

namespace Zamboni14Legacy.Components.Blaze.NHL14Legacy;

public static class TwoTwoFiveZeroComponentBase
{
    public enum TwoTwoFiveZeroComponentCommand : ushort
    {
        two = 2
    }

    public enum TwoTwoFiveZeroComponentNotification : ushort
    {
    }

    public const ushort Id = 2250;
    public const string Name = "TwoTwoFiveZeroComponent";

    public static Type GetCommandRequestType(TwoTwoFiveZeroComponentCommand command)
    {
        return command switch
        {
            TwoTwoFiveZeroComponentCommand.two => typeof(NullStruct),
            _ => typeof(NullStruct)
        };
    }

    public static Type GetCommandResponseType(TwoTwoFiveZeroComponentCommand command)
    {
        return command switch
        {
            TwoTwoFiveZeroComponentCommand.two => typeof(NullStruct),
            _ => typeof(NullStruct)
        };
    }

    public static Type GetCommandErrorResponseType(TwoTwoFiveZeroComponentCommand command)
    {
        return command switch
        {
            TwoTwoFiveZeroComponentCommand.two => typeof(NullStruct),
            _ => typeof(NullStruct)
        };
    }

    public static Type GetNotificationType(TwoTwoFiveZeroComponentNotification notification)
    {
        return notification switch
        {
            _ => typeof(NullStruct)
        };
    }

    public class Server : BlazeServerComponent<TwoTwoFiveZeroComponentCommand, TwoTwoFiveZeroComponentNotification, Blaze3RpcError>
    {
        public Server() : base(TwoTwoFiveZeroComponentBase.Id, TwoTwoFiveZeroComponentBase.Name)
        {
        }

        [BlazeCommand((ushort)TwoTwoFiveZeroComponentCommand.two)]
        public virtual Task<NullStruct> TwoAsync(NullStruct request, BlazeRpcContext context)
        {
            throw new BlazeRpcException(Blaze3RpcError.ERR_COMMAND_NOT_FOUND);
        }

        public override Type GetCommandRequestType(TwoTwoFiveZeroComponentCommand command)
        {
            return TwoTwoFiveZeroComponentBase.GetCommandRequestType(command);
        }

        public override Type GetCommandResponseType(TwoTwoFiveZeroComponentCommand command)
        {
            return TwoTwoFiveZeroComponentBase.GetCommandResponseType(command);
        }

        public override Type GetCommandErrorResponseType(TwoTwoFiveZeroComponentCommand command)
        {
            return TwoTwoFiveZeroComponentBase.GetCommandErrorResponseType(command);
        }

        public override Type GetNotificationType(TwoTwoFiveZeroComponentNotification notification)
        {
            return TwoTwoFiveZeroComponentBase.GetNotificationType(notification);
        }
    }

    public class Client : BlazeClientComponent<TwoTwoFiveZeroComponentCommand, TwoTwoFiveZeroComponentNotification, Blaze3RpcError>
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public Client(BlazeClientConnection connection) : base(TwoTwoFiveZeroComponentBase.Id, TwoTwoFiveZeroComponentBase.Name)
        {
            Connection = connection;
            if (!Connection.Config.AddComponent(this))
                throw new InvalidOperationException($"A component with Id({Id}) has already been created for the connection.");
        }

        private BlazeClientConnection Connection { get; }


        public NullStruct Two(NullStruct request)
        {
            return Connection.SendRequest<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoFiveZeroComponentCommand.two, request);
        }

        public Task<NullStruct> TwoAsync(NullStruct request)
        {
            return Connection.SendRequestAsync<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoFiveZeroComponentCommand.two, request);
        }


        public override Type GetCommandRequestType(TwoTwoFiveZeroComponentCommand command)
        {
            return TwoTwoFiveZeroComponentBase.GetCommandRequestType(command);
        }

        public override Type GetCommandResponseType(TwoTwoFiveZeroComponentCommand command)
        {
            return TwoTwoFiveZeroComponentBase.GetCommandResponseType(command);
        }

        public override Type GetCommandErrorResponseType(TwoTwoFiveZeroComponentCommand command)
        {
            return TwoTwoFiveZeroComponentBase.GetCommandErrorResponseType(command);
        }

        public override Type GetNotificationType(TwoTwoFiveZeroComponentNotification notification)
        {
            return TwoTwoFiveZeroComponentBase.GetNotificationType(notification);
        }
    }

    public class Proxy : BlazeProxyComponent<TwoTwoFiveZeroComponentCommand, TwoTwoFiveZeroComponentNotification, Blaze3RpcError>
    {
        public Proxy() : base(TwoTwoFiveZeroComponentBase.Id, TwoTwoFiveZeroComponentBase.Name)
        {
        }

        [BlazeCommand((ushort)TwoTwoFiveZeroComponentCommand.two)]
        public virtual Task<NullStruct> TwoAsync(NullStruct request, BlazeProxyContext context)
        {
            return context.ClientConnection.SendRequestAsync<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoFiveZeroComponentCommand.two, request);
        }

        public override Type GetCommandRequestType(TwoTwoFiveZeroComponentCommand command)
        {
            return TwoTwoFiveZeroComponentBase.GetCommandRequestType(command);
        }

        public override Type GetCommandResponseType(TwoTwoFiveZeroComponentCommand command)
        {
            return TwoTwoFiveZeroComponentBase.GetCommandResponseType(command);
        }

        public override Type GetCommandErrorResponseType(TwoTwoFiveZeroComponentCommand command)
        {
            return TwoTwoFiveZeroComponentBase.GetCommandErrorResponseType(command);
        }

        public override Type GetNotificationType(TwoTwoFiveZeroComponentNotification notification)
        {
            return TwoTwoFiveZeroComponentBase.GetNotificationType(notification);
        }
    }
}