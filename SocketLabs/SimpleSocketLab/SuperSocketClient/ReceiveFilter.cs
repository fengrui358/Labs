using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;

namespace SuperSocketClient
{
    class ReceiveFilter : TerminatorReceiveFilter<BufferedPackageInfo>
    {
        public ReceiveFilter() : base(Encoding.ASCII.GetBytes("||"))
        {
            
        }

        public override BufferedPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            throw new NotImplementedException();
        }
    }
}
