using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirKissDemo.Core
{
    public interface IUdpServer
    {
        /// <summary>
        /// 监听端口号
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// 是否启动
        /// </summary>
        bool Start { get; }

        /// <summary>
        /// 启动监听
        /// </summary>
        void StartListening();

        /// <summary>
        /// 停止监听
        /// </summary>
        void StopListening();

        /// <summary>
        /// 有新消息到达
        /// </summary>
        event EventHandler<byte[]> NewDataReceiveEvent;

        /// <summary>
        /// Udp的内部信息
        /// </summary>
        event EventHandler<string> StatusEvent;
    }
}
