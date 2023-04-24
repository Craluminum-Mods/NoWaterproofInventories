using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Util;
using Vintagestory.GameContent;

[assembly: ModInfo("No Waterproof Inventories",
    Authors = new[] { "Craluminum2413" })]

namespace NoWaterproofInventories;

public class Core : ModSystem
{
    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        api.RegisterEntityBehaviorClass("NWI_EntityBehaviorNoWaterproofInventory", typeof(EntityBehaviorNoWaterproofInventory));
        api.RegisterBlockEntityBehaviorClass("NWI_BEBehaviorNoWaterproofInventory", typeof(BEBehaviorNoWaterproofInventory));
        api.Event.OnEntitySpawn += AddEntityBehaviors;
        api.World.Logger.Event("started 'No Waterproof Inventories' mod");
    }

    private void AddEntityBehaviors(Entity entity)
    {
        if (entity is EntityPlayer) entity.AddBehavior(new EntityBehaviorNoWaterproofInventory(entity));
    }

    public override void AssetsFinalize(ICoreAPI api)
    {
        foreach (var block in api.World.Blocks)
        {
            TryAppendNoWaterproofInventoryBehavior(block);
        }
    }

    private static void TryAppendNoWaterproofInventoryBehavior(Block block)
    {
        if (block.HasBehavior<BlockBehaviorContainer>() || block is BlockContainer)
        {
            var behavior = new BlockEntityBehaviorType()
            {
                Name = "NWI_BEBehaviorNoWaterproofInventory",
                properties = null
            };

            block.BlockEntityBehaviors = block.BlockEntityBehaviors.Append(behavior);
        }
    }
}