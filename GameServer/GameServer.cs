﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using GameServer.Manager;
using log4net.Config;
using Photon.SocketServer;
using common;
using GameServer.Handler;
using GameServer.Modeling;

namespace GameServer
{
    //所有的Server端 主类都要继承自applicationbase
    public class GameServer : ApplicationBase
    {
        //定义一个Log对象
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();
        //当一个客户端请求连接的时候，服务器端就会调用这个方法
        //我们使用peerbase,表示和一个客户端的链接,然后photon就会把这些链接管理起来
        //当一个客户端请求连接的时候，服务器端就会调用这个方法
        //我们使用peerbase,表示和一个客户端的链接,然后photon就会把这些链接管理起来
        public new static GameServer Instance { get; private set; }

        Room[] room;
        //存储所有的Handler
        public Dictionary<OperationCode, BaseHandler> HandlerDict = new Dictionary<OperationCode, BaseHandler>();
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            log.Info("一个客户端连接进来了！");
            return new ClientPeer(initRequest);
        }
        //初始化(当整个服务器启动起来的时候调用这个初始化)
        protected override void Setup()
        {
            //日志的初始化(定义配置文件log4net位置)

            //Path.Combine  表示连接目录和文件名，可以屏蔽平台的差异
            // Photon: ApplicationLogPath 就是配置文件里面路径定义的属性
            //this.ApplicationPath 表示可以获取photon的根目录,就是Photon-OnPremise-Server-SDK_v4-0-29-11263\deploy这个目录
            //这一步是设置日志输出的文档文件的位置，这里我们把文档放在Photon-OnPremise-Server-SDK_v4-0-29-11263\deploy\bin_Win64\log里面
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(Path.Combine(Path.Combine(this.ApplicationRootPath, "bin_win64")), "log");
            //this.BinaryPath表示可以获取的部署目录就是目录Photon-OnPremise-Server-SDK_v4-0-29-11263\deploy\MyGameServer\bin
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));//告诉log4net日志的配置文件的位置
            //如果这个配置文件存在
            if (configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);//设置photon我们使用哪个日志插件
                XmlConfigurator.ConfigureAndWatch(configFileInfo);//让log4net这个插件读取配置文件
                /*IUserManager userManager = new UserManager();
                log.Info(userManager.VerifyUser("Sure", "157"));
                log.Info(userManager.VerifyUser("da", "25"));
                log.Info(userManager.VerifyUser("kety", "123"));
                */

            }
            for (int i = 0; i < 5; i++)
            {
                room[i] = new Room(i);
            }

            log.Info("Setup Completed!");//最后利用log对象就可以输出了
        }
        //server端关闭的时候
        protected override void TearDown()
        {
            log.Info("关闭了服务器");
        }
        //用来初始化Handler
        public void InitHandler()
        {
            LoginHandler loginHandler = new LoginHandler();
            HandlerDict.Add(loginHandler.opCode, loginHandler);
            DefaultHandler defaultHandler = new DefaultHandler();
            HandlerDict.Add(defaultHandler.opCode, defaultHandler);

            RegisterHandler registerHandler = new RegisterHandler();
            HandlerDict.Add(registerHandler.opCode, registerHandler);
        }
    }
}