using Blaze3SDK;
using BlazeCommon;
using NLog;

namespace Zamboni14Legacy.Components.Blaze.NHL14Legacy;

public static class TwoTwoFourNineComponentBase
{
    public enum TwoTwoFourNineComponentCommand : ushort
    {
        one = 1,
        two = 2
    }

    public enum TwoTwoFourNineComponentNotification : ushort
    {
    }

    public const ushort Id = 2249;
    public const string Name = "TwoTwoFourNineComponent";

    public static Type GetCommandRequestType(TwoTwoFourNineComponentCommand command)
    {
        return command switch
        {
            TwoTwoFourNineComponentCommand.one => typeof(NullStruct),
            _ => typeof(NullStruct)
        };
    }

    public static Type GetCommandResponseType(TwoTwoFourNineComponentCommand command)
    {
        return command switch
        {
            TwoTwoFourNineComponentCommand.one => typeof(NullStruct),
            _ => typeof(NullStruct)
        };
    }

    public static Type GetCommandErrorResponseType(TwoTwoFourNineComponentCommand command)
    {
        return command switch
        {
            TwoTwoFourNineComponentCommand.one => typeof(NullStruct),
            _ => typeof(NullStruct)
        };
    }

    public static Type GetNotificationType(TwoTwoFourNineComponentNotification notification)
    {
        return notification switch
        {
            _ => typeof(NullStruct)
        };
    }

    public class Server : BlazeServerComponent<TwoTwoFourNineComponentCommand, TwoTwoFourNineComponentNotification, Blaze3RpcError>
    {
        public Server() : base(TwoTwoFourNineComponentBase.Id, TwoTwoFourNineComponentBase.Name)
        {
        }

        [BlazeCommand((ushort)TwoTwoFourNineComponentCommand.one)]
        public virtual Task<NullStruct> OneAsync(NullStruct request, BlazeRpcContext context)
        {
            throw new BlazeRpcException(Blaze3RpcError.ERR_COMMAND_NOT_FOUND);
        }

        [BlazeCommand((ushort)TwoTwoFourNineComponentCommand.two)]
        public virtual Task<NullStruct> TwoAsync(NullStruct request, BlazeRpcContext context)
        {
            throw new BlazeRpcException(Blaze3RpcError.ERR_COMMAND_NOT_FOUND);
        }

        public override Type GetCommandRequestType(TwoTwoFourNineComponentCommand command)
        {
            return TwoTwoFourNineComponentBase.GetCommandRequestType(command);
        }

        public override Type GetCommandResponseType(TwoTwoFourNineComponentCommand command)
        {
            return TwoTwoFourNineComponentBase.GetCommandResponseType(command);
        }

        public override Type GetCommandErrorResponseType(TwoTwoFourNineComponentCommand command)
        {
            return TwoTwoFourNineComponentBase.GetCommandErrorResponseType(command);
        }

        public override Type GetNotificationType(TwoTwoFourNineComponentNotification notification)
        {
            return TwoTwoFourNineComponentBase.GetNotificationType(notification);
        }
    }

    public class Client : BlazeClientComponent<TwoTwoFourNineComponentCommand, TwoTwoFourNineComponentNotification, Blaze3RpcError>
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public Client(BlazeClientConnection connection) : base(TwoTwoFourNineComponentBase.Id, TwoTwoFourNineComponentBase.Name)
        {
            Connection = connection;
            if (!Connection.Config.AddComponent(this))
                throw new InvalidOperationException($"A component with Id({Id}) has already been created for the connection.");
        }

        private BlazeClientConnection Connection { get; }


        public NullStruct One(NullStruct request)
        {
            return Connection.SendRequest<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoFourNineComponentCommand.one, request);
        }

        public Task<NullStruct> OneAsync(NullStruct request)
        {
            return Connection.SendRequestAsync<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoFourNineComponentCommand.one, request);
        }

        public NullStruct Two(NullStruct request)
        {
            return Connection.SendRequest<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoFourNineComponentCommand.two, request);
        }

        public Task<NullStruct> TwoAsync(NullStruct request)
        {
            return Connection.SendRequestAsync<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoFourNineComponentCommand.two, request);
        }


        public override Type GetCommandRequestType(TwoTwoFourNineComponentCommand command)
        {
            return TwoTwoFourNineComponentBase.GetCommandRequestType(command);
        }

        public override Type GetCommandResponseType(TwoTwoFourNineComponentCommand command)
        {
            return TwoTwoFourNineComponentBase.GetCommandResponseType(command);
        }

        public override Type GetCommandErrorResponseType(TwoTwoFourNineComponentCommand command)
        {
            return TwoTwoFourNineComponentBase.GetCommandErrorResponseType(command);
        }

        public override Type GetNotificationType(TwoTwoFourNineComponentNotification notification)
        {
            return TwoTwoFourNineComponentBase.GetNotificationType(notification);
        }
    }

    public class Proxy : BlazeProxyComponent<TwoTwoFourNineComponentCommand, TwoTwoFourNineComponentNotification, Blaze3RpcError>
    {
        public Proxy() : base(TwoTwoFourNineComponentBase.Id, TwoTwoFourNineComponentBase.Name)
        {
        }

        [BlazeCommand((ushort)TwoTwoFourNineComponentCommand.one)]
        public virtual Task<NullStruct> OneAsync(NullStruct request, BlazeProxyContext context)
        {
            return context.ClientConnection.SendRequestAsync<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoFourNineComponentCommand.one, request);
        }

        [BlazeCommand((ushort)TwoTwoFourNineComponentCommand.two)]
        public virtual Task<NullStruct> TwoAsync(NullStruct request, BlazeProxyContext context)
        {
            return context.ClientConnection.SendRequestAsync<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoFourNineComponentCommand.two, request);
        }

        public override Type GetCommandRequestType(TwoTwoFourNineComponentCommand command)
        {
            return TwoTwoFourNineComponentBase.GetCommandRequestType(command);
        }

        public override Type GetCommandResponseType(TwoTwoFourNineComponentCommand command)
        {
            return TwoTwoFourNineComponentBase.GetCommandResponseType(command);
        }

        public override Type GetCommandErrorResponseType(TwoTwoFourNineComponentCommand command)
        {
            return TwoTwoFourNineComponentBase.GetCommandErrorResponseType(command);
        }

        public override Type GetNotificationType(TwoTwoFourNineComponentNotification notification)
        {
            return TwoTwoFourNineComponentBase.GetNotificationType(notification);
        }
    }
}