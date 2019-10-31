using common;
using common.Tools;
using GameServer;
using GameServer.Handler;
using GameServer.Manager;
using Photon.SocketServer;

namespace GameServer.Handler
{
    public class LoginHandler:BaseHandler
    {
        public LoginHandler()
        {
            opCode = OperationCode.Login;//赋值OperationCode为Login,表示处理的是那个请求
        }
        //登陆请求的处理的代码
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            //根据发送过来的请求获得用户名和密码
            string username = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Username) as string;
            string password = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Password) as string;
            //连接数据库进行校验
            UserManager manager = new UserManager();
            bool isSuccess = manager.VerifyUser(username, password);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            //如果验证成功，把成功的结果利用response.ReturnCode返回成功给客户端
            if (isSuccess)
            {
                response.ReturnCode = (short)ReturnCode.success;
            }
            else//否则返回失败给客户端
            {
                response.ReturnCode = (short)ReturnCode.failed;
            }
            //把上面的回应给客户端
            peer.SendOperationResponse(response, sendParameters);

        }
    }
}