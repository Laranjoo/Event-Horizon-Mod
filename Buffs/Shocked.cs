using Terraria;
using Terraria.ModLoader;

namespace EventHorizons.Buffs
{
	public class Shocked : ModBuff
	{
		private int timer;
		public override void SetStaticDefaults() 
		{
            		Main.debuff[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex) 
		{
			for(timer = 0, timer < 1000000, timer++)
			{
				if (timer % 60 == 0)
				{
					player.statLife -= 4; // Adjust the damage as per your preference
            				player.lifeRegenTime = 0;
            				player.lifeRegen -= 2;
				}
			}
		}
	}
}
