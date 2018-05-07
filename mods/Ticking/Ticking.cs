using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;

namespace ExampleMods
{
    public class Ticking : ModSystem
    {

        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            api.RegisterBlockEntityClass("tickingcounter", typeof(TickingBlockEntity));
        }

    }

    public class TickingBlockEntity : BlockEntity
    {

        public int timer;

        public override void Initialize(ICoreAPI api)
        {
            base.Initialize(api);
            RegisterGameTickListener(onTick, 20);
        }

        public void onTick(float par)
        {
            timer++;
            if(timer > 60)
            {
                Block block = api.World.BlockAccessor.GetBlock(pos);
                if (block.Code.Path.EndsWith("-on"))
                    block = api.World.GetBlock(block.CodeWithParts("off"));
                else
                    block = api.World.GetBlock(block.CodeWithParts("on"));
                api.World.BlockAccessor.SetBlock(block.BlockId, pos);
            }
        }

        public override void ToTreeAttributes(ITreeAttribute tree)
        {
            base.ToTreeAttributes(tree);
            tree.SetInt("timer", timer);
        }

        public override void FromTreeAtributes(ITreeAttribute tree, IWorldAccessor worldAccessForResolve)
        {
            base.FromTreeAtributes(tree, worldAccessForResolve);
            timer = tree.GetInt("timer");
        }
    }
}
