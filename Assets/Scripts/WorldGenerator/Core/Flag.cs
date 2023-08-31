using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Core.Flag
{
    public enum Face : byte
    {
        None = 0,
        Forward = 1 << 0,
        Backward = 1 << 1,
        Top = 1 << 2,
        Bottom = 1 << 3,
        Left = 1 << 4,
        Right = 1 << 5,
        All = Forward | Backward | Top | Bottom | Left | Right,
        //RLBTBF
    }

    public enum BiomeType : byte
    {
        Mountain = 0,
        Grassland = 1,
        Desert = 2,
        //SnowMountain = 3,
    }
}
