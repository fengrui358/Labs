using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EqualLab
{
    class LabClass
    {
        private readonly int _id;

        public LabClass(int id)
        {
            _id = id;
        }

        public override bool Equals(object obj)
        {
            var labClass = obj as LabClass;
            return labClass?._id == _id;
        }

        public override int GetHashCode()
        {
            return _id;
        }

        public static bool operator ==(LabClass a, LabClass b)
        {
            return a?.Equals(b) ?? false;
        }

        public static bool operator !=(LabClass a, LabClass b)
        {
            return !(a?.Equals(b) ?? false);
        }
    }
}
