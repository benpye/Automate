using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukSharp.BindingGen
{
    public class CType
    {
        public CType(string name, int pointerLevel, bool c)
        {
            Name = name;
            PointerLevel = pointerLevel;
            Const = c;
        }

        public string Name { get; }
        public int PointerLevel { get; }
        public bool Const { get; }

        public override bool Equals(Object obj)
        {
            return obj is CType && this == (CType)obj;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ PointerLevel.GetHashCode() ^ Const.GetHashCode();
        }
        public static bool operator ==(CType x, CType y)
        {
            return x.Name == y.Name && x.PointerLevel == y.PointerLevel && x.Const == y.Const;
        }
        public static bool operator !=(CType x, CType y)
        {
            return !(x == y);
        }
    }
}
