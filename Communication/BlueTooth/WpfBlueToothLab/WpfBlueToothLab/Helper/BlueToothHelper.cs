using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBlueToothLab.Helper
{
    public static class BlueToothHelper
    {
        private static Guid _serviceClassId;

        public static Guid ServiceClassId
        {
            get
            {
                if (_serviceClassId == Guid.Empty)
                {
                    return new Guid("00001101-0000-1000-8000-00805F9B34FB");
                }

                return _serviceClassId;
            }
            set { _serviceClassId = value; }
        }
    }
}
