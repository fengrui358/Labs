using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;

namespace SuperSocketClientWithCommand
{
    public class CalcResultPackageInfo : BufferedPackageInfo
    {
        public bool CanParse { get; private set; }

        public double Result { get; private set; }

        public CalcResultPackageInfo(string key, IList<ArraySegment<byte>> data) : base(key, data)
        {
            var dataString = Encoding.UTF8.GetString(data);
            double result;

            CanParse = double.TryParse(dataString, out result);
            Result = result;
        }
    }
}
