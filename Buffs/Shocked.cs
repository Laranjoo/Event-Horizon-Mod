using Terraria;
using Terraria.ModLoader;

namespace EventHorizons.Buffs
{
	public class Shocked : ModBuff
	{
		public override void SetStaticDefaults() {
            Main.debuff[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex) {
			player.statLife -= 4; // Adjust the damage as per your preference
            player.lifeRegenTime = 0;
            player.lifeRegen -= 2;
		}
	}
}