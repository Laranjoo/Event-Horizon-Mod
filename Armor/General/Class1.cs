﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using EventHorizons.Items.Materials;

namespace EventHorizons.General
{
    internal class Class1 : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SpaceDust>(), 15));
          
        }
       
    }
}
