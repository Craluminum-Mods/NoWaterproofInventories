using System.Text;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace NoWaterproofInventories;

public class BEBehaviorNoWaterproofInventory : BlockEntityBehavior
{
    public BEBehaviorNoWaterproofInventory(BlockEntity blockentity) : base(blockentity) { }

    long listenerId;

    public override void Initialize(ICoreAPI api, JsonObject properties)
    {
        listenerId = api.Event.RegisterGameTickListener(UpdateEvery1000ms, 1000);
        base.Initialize(api, properties);
    }

    private void UpdateEvery1000ms(float dt)
    {
        if (Blockentity?.Block?.LiquidCode?.Contains("water") == false) return;
        if (Blockentity is not BlockEntityContainer becontainer) return;
        if (becontainer?.Inventory?.Empty == true) return;

        foreach (var slot in becontainer.Inventory)
        {
            if (slot?.Empty == true) continue;
            if (slot.Itemstack.Collectible is not BlockTorch torch) continue;

            var IsExtinct = slot.Itemstack.Collectible.Variant["state"] == "extinct";
            if (Api.World.Side != EnumAppSide.Server || IsExtinct || torch.ExtinctVariant == null) continue;

            Pos.PlayExtinguishSound(Api.World);
            slot.Itemstack = new ItemStack(torch.ExtinctVariant, slot.Itemstack.StackSize);
            slot.MarkDirty();
        }
    }

    public override void GetBlockInfo(IPlayer forPlayer, StringBuilder dsc)
    {
        base.GetBlockInfo(forPlayer, dsc);
        dsc.Append("No Waterproof Inventory TEST");
    }
}