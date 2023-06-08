/*
 >>>> THIS WEAPON IS USED BY ANOTHER MOD, PLAY ARCHAEA/CATASTROPHE MOD WEAPON CALLED "ORION" TO SEE IT

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace PenumbraMod.Content.Items
{
    public class a : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.DamageType = (DamageClass.Melee);
            Item.width = 71;
            Item.height = 71;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.knockBack = 2;
            Item.value = 453450;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<a2>();
            Item.shootSpeed = 2f;
            Item.noUseGraphic = false;
            Item.scale = 2f;
            Item.noMelee = true;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<a2>()] < 1;
        }
        bool notboollol = true;
        public int TwistedStyle = 0;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Item.noUseGraphic = true;
            if (TwistedStyle == 0)
            {
                if (player.velocity.Y > 5)
                {
                    if (player.direction == 0)
                        notboollol = true;
                    else
                        notboollol = false;
                }
                if (player.velocity.Y < -5)
                {
                    if (player.direction != 0)
                        notboollol = true;
                    else
                        notboollol = false;
                }
                if (player.statLife > player.statLifeMax / 2)
                {
                    Item.useTime = 20;
                    Item.useAnimation = 20;
                }
                else
                {
                    Item.useTime = 40;
                    Item.useAnimation = 40;
                }
                int basic = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<a2>(), damage, knockback, player.whoAmI);
                if (notboollol == true)
                {
                    Main.projectile[basic].ai[1] = -1;
                    notboollol = false;
                }
                else
                {
                    Main.projectile[basic].ai[1] = 1;
                    notboollol = true;
                }
                Main.projectile[basic].rotation = Main.projectile[basic].DirectionTo(Main.MouseWorld).ToRotation();

            }

            return false;
        }
    }
    public class a2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wakizashi");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.damage = 200;
            Projectile.width = 176;
            Projectile.height = 176;
            Projectile.friendly = true;
            Projectile.timeLeft = 20; // lowered from 300
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.knockBack = 8f;
        }
        Vector2 dir = Vector2.Zero;
        Vector2 hlende = Vector2.Zero;
        public static float easeInOutQuad(float x)
        {
            return x < 0.5 ? 2 * x * x : 1 - (float)Math.Pow(-2 * x + 2, 2) / 2;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player projOwner = Main.player[Projectile.owner];
            if (projOwner.statLife < projOwner.statLifeMax / 2)
            {
                Main.instance.LoadProjectile(Projectile.type);
                Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

                // Redraw the projectile with the color not influenced by light
                for (int i = 0; i < Projectile.oldPos.Length; i++)
                {
                    if (Projectile.oldPos[i] == Vector2.Zero)
                        return false;
                    for (float j = 0; j < 1; j += 0.0625f)
                    {
                        Vector2 lerpedPos;
                        if (i > 0)
                            lerpedPos = Vector2.Lerp(Projectile.oldPos[i - 1], Projectile.oldPos[i], easeInOutQuad(j));
                        else
                            lerpedPos = Vector2.Lerp(Projectile.position, Projectile.oldPos[i], easeInOutQuad(j));
                        float lerpedAngle;
                        if (i > 0)
                            lerpedAngle = Utils.AngleLerp(Projectile.oldRot[i - 1], Projectile.oldRot[i], easeInOutQuad(j));
                        else
                            lerpedAngle = Utils.AngleLerp(Projectile.rotation, Projectile.oldRot[i], easeInOutQuad(j));
                        lerpedPos += Projectile.Size / 2;
                        lerpedPos -= Main.screenPosition;
                        Main.EntitySpriteDraw(texture, lerpedPos, null, Color.White * 0.1f * (1 - ((float)i / (float)Projectile.oldPos.Length)), lerpedAngle, new Vector2(texture.Width / 2, texture.Height / 2), Projectile.scale, SpriteEffects.None, 0);
                    }
                }
            }
            return true;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float rotationFactor = Projectile.rotation + (float)Math.PI / 4f; // The rotation of the Jousting Lance.
            float scaleFactor = 120f; // How far back the hit-line will be from the tip of the Jousting Lance. You will need to modify this if you have a longer or shorter Jousting Lance. Vanilla uses 95f
            float widthMultiplier = 30f; // How thick the hit-line is. Increase or decrease this value if your Jousting Lance is thicker or thinner. Vanilla uses 23f
            float collisionPoint = 0f; // collisionPoint is needed for CheckAABBvLineCollision(), but it isn't used for our collision here. Keep it at 0f.
            Player projOwner = Main.player[Projectile.owner];
            if (projOwner.statLife < projOwner.statLifeMax / 2)
                scaleFactor = 140f;
            // This Rectangle is the width and height of the Jousting Lance's hitbox which is used for the first step of collision.
            // You will need to modify the last two numbers if you have a bigger or smaller Jousting Lance.
            // Vanilla uses (0, 0, 300, 300) which that is quite large for the size of the Jousting Lance.
            // The size doesn't matter too much because this rectangle is only a basic check for the collision (the hit-line is much more important).
            Rectangle lanceHitboxBounds = new Rectangle(0, 0, 284, 284);
            // Set the position of the large rectangle.
            lanceHitboxBounds.X = (int)Projectile.position.X - lanceHitboxBounds.Width / 2;
            lanceHitboxBounds.Y = (int)Projectile.position.Y - lanceHitboxBounds.Height / 2;

            // This is the back of the hit-line with Projectile.Center being the tip of the Jousting Lance.
            Vector2 hitLineEnd = Projectile.Center + rotationFactor.ToRotationVector2() * -scaleFactor;
            hlende = hitLineEnd;
            // First check that our large rectangle intersects with the target hitbox.
            // Then we check to see if a line from the tip of the Jousting Lance to the "end" of the lance intersects with the target hitbox.
            if (/*lanceHitboxBounds.Intersects(targetHitbox)
                && Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, hitLineEnd, widthMultiplier * Projectile.scale, ref collisionPoint))
            {
                return true;
            }
            return false;
        }
        public override void AI()
        {
            // All Projectiles have timers that help to delay certain events
            // Projectile.ai[0], Projectile.ai[1] � timers that are automatically synchronized on the client and server
            // Projectile.localAI[0], Projectile.localAI[0] � only on the client
            // In this example, a timer is used to control the fade in / out and despawn of the Projectile
            //Projectile.ai[0] += 1f;

            Player projOwner = Main.player[Projectile.owner];
            if (dir == Vector2.Zero)
            {
                dir = Main.MouseWorld;
                Projectile.rotation = (MathHelper.PiOver2 * Projectile.ai[1]) - MathHelper.PiOver4 + Projectile.DirectionTo(Main.MouseWorld).ToRotation();
            }
            //FadeInAndOut();
            Projectile.Center = Main.player[Projectile.owner].Center;
            Projectile.ai[0] += 1f;
            if (projOwner.statLife > projOwner.statLifeMax / 2)
            {
                Projectile.rotation += (Projectile.ai[1] * MathHelper.ToRadians((20 - Projectile.ai[0])));
                Projectile.scale = 1f;
            }
            else
            {
                Projectile.rotation += (Projectile.ai[1] * MathHelper.ToRadians((40 - Projectile.ai[0])));
                Projectile.scale = 1.2f;
            }

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            if (player.statLife < player.statLifeMax / 2)
                player.Heal(10);
        }
    }
}

*/