using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirKissLib
{
    public class AirKissTask
    {
        private char _randomChar;
        private AirKissEncoder _airKissEncoder;

        public AirKissTask(AirKissEncoder airKissEncoder)
        {
            _randomChar = airKissEncoder.GetRandomChar();
            _airKissEncoder = airKissEncoder;
        }

        public void Execute()
        {
            
        }
    }
}
