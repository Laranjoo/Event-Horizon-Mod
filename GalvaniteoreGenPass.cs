using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using EventHorizons.Tiles;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace EventHorizons.Commons.Systems.Genpasses
{
    internal class GalvaniteoreGenPass : GenPass
    {
        public GalvaniteoreGenPass(string name, float weight) : base(name, weight) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Charging up space";

            int maxToSpawn = (int)(Main.maxTilesX * Main.maxTilesY * 6E-05);
            for (int i = 0; i < maxToSpawn; i++)
            {
                int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                int y = WorldGen.genRand.Next((int)Main.worldSurface * 0.35, (int)Main.worldSurface * 0);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 5), ModContent.TileType<Galvaniteore>());
            } 
        }
    }

}
