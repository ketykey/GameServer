﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using common;
using Photon.SocketServer;

namespace GameServer.Handler
{
    public abstract class BaseHandler
    {

        public OperationCode opCode;
        //处理请求的方法
        public abstract void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer);

    }
}