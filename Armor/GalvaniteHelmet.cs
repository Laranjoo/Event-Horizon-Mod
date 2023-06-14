using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using EventHorizons.Buffs;
using EventHorizons.Players;
using EventHorizons.Items.Placeables;

namespace EventHorizons.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class GalvaniteHelmet : ModItem
	{
		int chance = Main.rand.Next(3);
		public override void SetStaticDefaults() {

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() 
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Blue; // The rarity of the item
			Item.defense = 6; // The amount of defense the item will give when equipped
		}
		public override bool IsArmorSet(Item head, Item body, Item legs) 
		{
			return body.type == ModContent.ItemType<Items.Armor.GalvaniteBreastplate>() && legs.type == ModContent.ItemType<Items.Armor.GalvaniteLeggings>();
		}
		public override void UpdateArmorSet(Player player) 
		{
			player.setBonus = "Ranged and Magic attacks have a chnace to apply the Shocked debuff.";
			player.GetModPlayer<ShockedPlayer>().HasGalvaniteArmor = true;
		}
		public override void AddRecipes()
        {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Galvanitebar>(), 15);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}