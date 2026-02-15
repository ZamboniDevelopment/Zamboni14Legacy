using Blaze3SDK;
using BlazeCommon;
using NLog;

namespace Zamboni14Legacy.Components.NHL14Legacy.Bases;

public static class TwoTwoSixEightComponentBase
{
    public enum TwoTwoSixEightComponentCommand : ushort
    {
        three = 3
    }

    public enum TwoTwoSixEightComponentNotification : ushort
    {
    }

    public const ushort Id = 2268;
    public const string Name = "TwoTwoSixEightComponent";

    public static Type GetCommandRequestType(TwoTwoSixEightComponentCommand command)
    {
        return command switch
        {
            TwoTwoSixEightComponentCommand.three => typeof(NullStruct),
            _ => typeof(NullStruct)
        };
    }

    public static Type GetCommandResponseType(TwoTwoSixEightComponentCommand command)
    {
        return command switch
        {
            TwoTwoSixEightComponentCommand.three => typeof(NullStruct),
            _ => typeof(NullStruct)
        };
    }

    public static Type GetCommandErrorResponseType(TwoTwoSixEightComponentCommand command)
    {
        return command switch
        {
            TwoTwoSixEightComponentCommand.three => typeof(NullStruct),
            _ => typeof(NullStruct)
        };
    }

    public static Type GetNotificationType(TwoTwoSixEightComponentNotification notification)
    {
        return notification switch
        {
            _ => typeof(NullStruct)
        };
    }

    public class Server : BlazeServerComponent<TwoTwoSixEightComponentCommand, TwoTwoSixEightComponentNotification, Blaze3RpcError>
    {
        public Server() : base(TwoTwoSixEightComponentBase.Id, TwoTwoSixEightComponentBase.Name)
        {
        }

        [BlazeCommand((ushort)TwoTwoSixEightComponentCommand.three)]
        public virtual Task<NullStruct> ThreeAsync(NullStruct request, BlazeRpcContext context)
        {
            throw new BlazeRpcException(Blaze3RpcError.ERR_COMMAND_NOT_FOUND);
        }

        public override Type GetCommandRequestType(TwoTwoSixEightComponentCommand command)
        {
            return TwoTwoSixEightComponentBase.GetCommandRequestType(command);
        }

        public override Type GetCommandResponseType(TwoTwoSixEightComponentCommand command)
        {
            return TwoTwoSixEightComponentBase.GetCommandResponseType(command);
        }

        public override Type GetCommandErrorResponseType(TwoTwoSixEightComponentCommand command)
        {
            return TwoTwoSixEightComponentBase.GetCommandErrorResponseType(command);
        }

        public override Type GetNotificationType(TwoTwoSixEightComponentNotification notification)
        {
            return TwoTwoSixEightComponentBase.GetNotificationType(notification);
        }
    }

    public class Client : BlazeClientComponent<TwoTwoSixEightComponentCommand, TwoTwoSixEightComponentNotification, Blaze3RpcError>
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public Client(BlazeClientConnection connection) : base(TwoTwoSixEightComponentBase.Id, TwoTwoSixEightComponentBase.Name)
        {
            Connection = connection;
            if (!Connection.Config.AddComponent(this))
                throw new InvalidOperationException($"A component with Id({Id}) has already been created for the connection.");
        }

        private BlazeClientConnection Connection { get; }


        public NullStruct Three(NullStruct request)
        {
            return Connection.SendRequest<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoSixEightComponentCommand.three, request);
        }

        public Task<NullStruct> ThreeAsync(NullStruct request)
        {
            return Connection.SendRequestAsync<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoSixEightComponentCommand.three, request);
        }


        public override Type GetCommandRequestType(TwoTwoSixEightComponentCommand command)
        {
            return TwoTwoSixEightComponentBase.GetCommandRequestType(command);
        }

        public override Type GetCommandResponseType(TwoTwoSixEightComponentCommand command)
        {
            return TwoTwoSixEightComponentBase.GetCommandResponseType(command);
        }

        public override Type GetCommandErrorResponseType(TwoTwoSixEightComponentCommand command)
        {
            return TwoTwoSixEightComponentBase.GetCommandErrorResponseType(command);
        }

        public override Type GetNotificationType(TwoTwoSixEightComponentNotification notification)
        {
            return TwoTwoSixEightComponentBase.GetNotificationType(notification);
        }
    }

    public class Proxy : BlazeProxyComponent<TwoTwoSixEightComponentCommand, TwoTwoSixEightComponentNotification, Blaze3RpcError>
    {
        public Proxy() : base(TwoTwoSixEightComponentBase.Id, TwoTwoSixEightComponentBase.Name)
        {
        }

        [BlazeCommand((ushort)TwoTwoSixEightComponentCommand.three)]
        public virtual Task<NullStruct> ThreeAsync(NullStruct request, BlazeProxyContext context)
        {
            return context.ClientConnection.SendRequestAsync<NullStruct, NullStruct, NullStruct>(this, (ushort)TwoTwoSixEightComponentCommand.three, request);
        }

        public override Type GetCommandRequestType(TwoTwoSixEightComponentCommand command)
        {
            return TwoTwoSixEightComponentBase.GetCommandRequestType(command);
        }

        public override Type GetCommandResponseType(TwoTwoSixEightComponentCommand command)
        {
            return TwoTwoSixEightComponentBase.GetCommandResponseType(command);
        }

        public override Type GetCommandErrorResponseType(TwoTwoSixEightComponentCommand command)
        {
            return TwoTwoSixEightComponentBase.GetCommandErrorResponseType(command);
        }

        public override Type GetNotificationType(TwoTwoSixEightComponentNotification notification)
        {
            return TwoTwoSixEightComponentBase.GetNotificationType(notification);
        }
    }
}