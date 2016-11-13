using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirKissDemo.Core
{
    public interface IUdpClient
    {
        int SleepingTime { get; set; }

        /// <summary>
        /// 发送端口号
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// 是否启动
        /// </summary>
        bool Start { get; }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        void StartSend(int[] data);

        /// <summary>
        /// 停止发送
        /// </summary>
        void Stop();

        /// <summary>
        /// Udp的内部信息
        /// </summary>
        event EventHandler<string> StatusEvent;
    }
}
