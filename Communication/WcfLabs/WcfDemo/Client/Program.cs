using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Service.Interface;

namespace Client
{
    class Program
    {
        private static void Main(string[] args)
        {

            ICalculator proxy = GetProxy<ICalculator>();
            Console.WriteLine(proxy.Add(3, 6));
            Console.WriteLine(proxy.Subtract(3, 6));
            Console.WriteLine(proxy.Multiply(3, 6));
            Console.WriteLine(proxy.Divide(3, 6));

            IHelloWord proxy2 = GetProxy<IHelloWord>();
            Console.WriteLine(proxy2.HelloWord());
            Console.WriteLine(proxy2.HelloWord("sadfasfsafasdfassafsadf"));

            Console.Read();
        }

        private static T GetProxy<T>()
        {
            ChannelFactory<T> channelFactory = new ChannelFactory<T>(new WSHttpBinding(),
                $"http://192.168.1.80:3721/{typeof (T).FullName}");

            
            return channelFactory.CreateChannel();
        }
    }
}
