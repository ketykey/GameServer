﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using common;
using common.Tools;
using GameServer.Handler;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace GameServer
{
    //管理跟客户端的链接的
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public ClientPeer(InitRequest initRequest) : base(initRequest)
        {
            
        }
        //处理客户端断开连接的后续工作
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {

        }
        //处理客户端的请求
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            //OperationRequest封装了请求的信息
            //SendParameters 参数，传递的数据


            //通过客户端的OperationCode从HandlerDict里面获取到了需要的Hander
            BaseHandler handler = DictTool.GetValue<OperationCode, BaseHandler>(GameServer.Instance.HandlerDict, (OperationCode)operationRequest.OperationCode);

            //如果找到了需要的hander就调用我们hander里面处理请求的方法
            if (handler != null)
            {
                handler.OnOperationRequest(operationRequest, sendParameters, this);

            }
            else//否则我们就使用默认的hander
            {
                BaseHandler defaultHandler = DictTool.GetValue<OperationCode, BaseHandler>(GameServer.Instance.HandlerDict, OperationCode.Default);
                defaultHandler.OnOperationRequest(operationRequest, sendParameters, this);
            }
        }
    }
}
