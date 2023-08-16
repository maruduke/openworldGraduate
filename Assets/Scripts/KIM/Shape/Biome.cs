using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Biome
{
    public class Biome
    {

    }

    public class DesertBiome
    {
        public DesertBiome() { }

    }

    public class BlockType
    {
        public string Name { get; set; }
        public BlockTexture textrue { get; set; }
    }

    public class BlockTexture
    {
        public string Name { get; set; }
        public Material material { get; set; }
    }
}
