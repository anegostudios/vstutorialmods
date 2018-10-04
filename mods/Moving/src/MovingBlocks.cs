using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;

namespace ExampleMods
{
    public class MovingBlocks : ModSystem
    {

        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            api.RegisterBlockBehaviorClass("Moving", typeof(Moving));
        }

    }

    class Moving : BlockBehavior
    {
        public Moving(Block block) : base(block)
        {

        }

        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel, ref EnumHandling handling)
        {
            BlockPos pos = blockSel.Position.AddCopy(blockSel.Face.GetOpposite());
            if (world.BlockAccessor.GetBlock(pos).IsReplacableBy(block))
            {
                world.BlockAccessor.SetBlock(0, blockSel.Position);
                world.BlockAccessor.SetBlock(block.BlockId, pos);
            }
            handling = EnumHandling.PreventDefault;
            return true;
        }
    }
}
