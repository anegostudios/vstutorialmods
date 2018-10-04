using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace ExampleMods
{
    public class CommandMod : ModSystem
    {

        public override bool ShouldLoad(EnumAppSide side)
        {
            return side == EnumAppSide.Server;
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
            AssetLocation sound = new AssetLocation("here", "sounds/partyhorn");
            api.RegisterCommand("here", "spawns particles around the player", "",
                (IServerPlayer player, int groupId, CmdArgs args) =>
                    {
                        IEntityPlayer byEntity = player.Entity;
                        byEntity.World.PlaySoundAt(sound, byEntity);
                        Vec3d pos = byEntity.Pos.XYZ.Add(0, byEntity.EyeHeight, 0);
                        Random rand = new Random();
                        for (int i = 0; i < 100; i++)
                        {
                            Vec3d realPos = pos.AddCopy(-0.1 + rand.NextDouble() * 0.2, 0, -0.1 + rand.NextDouble() * 0.2);
                            Vec3f velocity = new Vec3f(-0.2F + (float) rand.NextDouble() * 0.4F, 0.4F + (float) rand.NextDouble() * 2F, -0.2F + (float) rand.NextDouble() * 0.4F);
                            byEntity.World.SpawnParticles(1, ColorUtil.ToRgba(255, rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)),
                                realPos, realPos,
                                velocity, velocity, (float) rand.NextDouble()*1 + 1, 0.01F,
                                1, EnumParticleModel.Cube);
                        }
                    }, Privilege.chat);
        }

    }
}
