using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukSharp.BindingGen
{
    public class CType
    {
        public CType(string type)
        {
            Name = type.Replace("*", "").Replace("const", "").Trim();
            PointerLevel = type.Where(c => c == '*').Count();
            Const = type.Contains("const");
            TypeString = type;
        }

        public string Name { get; }
        public int PointerLevel { get; }
        public bool Const { get; }
        public string TypeString { get; }

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
