using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace ExampleMods
{
    public class MovingBlocks : ModSystem
    {

        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            api.RegisterBlockBehaviorClass("AdvancedMoving", typeof(Moving));
        }

    }

    class Moving : BlockBehavior
    {

        public int distance = 1;
        public bool pull = false;

        public Moving(Block block) : base(block)
        {

        }

        public override bool OnPlayerBlockInteract(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel, ref EnumHandling handling)
        {
            BlockPos pos = blockSel.Position.AddCopy(pull && byPlayer.WorldData.EntityControls.Sneak ? blockSel.Face : blockSel.Face.GetOpposite(), distance);
            if (world.BlockAccessor.GetBlock(pos).IsReplacableBy(block))
            {
                world.BlockAccessor.SetBlock(0, blockSel.Position);
                world.BlockAccessor.SetBlock(block.BlockId, pos);
            }
            handling = EnumHandling.PreventDefault;
            return true;
        }

        public override void Initialize(JsonObject properties)
        {
            base.Initialize(properties);
            distance = properties["distance"].AsInt(1);
            pull = properties["pull"].AsBool(false);
        }
    }
}
