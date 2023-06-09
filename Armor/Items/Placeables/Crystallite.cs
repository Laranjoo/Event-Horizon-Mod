﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using EventHorizons.Tiles;


namespace EventHorizons.Items.Placeables
{
    public class Crystallite : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
            ItemID.Sets.SortingPriorityMaterials[Type] = 4;
        }
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.value = Item.buyPrice(silver: 1);
            Item.autoReuse = true;
            Item.rare = 4;
            Item.useAnimation = 10;
            Item.useStyle = 1;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.createTile = ModContent.TileType<Tiles.some_crystal_block_spritesheet>();
        }


    }

}