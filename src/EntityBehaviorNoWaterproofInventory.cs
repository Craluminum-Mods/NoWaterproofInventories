using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.GameContent;

namespace NoWaterproofInventories;

public class EntityBehaviorNoWaterproofInventory : EntityBehavior
{
    public EntityBehaviorNoWaterproofInventory(Entity entity) : base(entity) { }

    public override void OnGameTick(float deltaTime)
    {
        base.OnGameTick(deltaTime);

        switch (entity)
        {
            case EntityPlayer player:
                player?.WalkInventory(CheckSlot);
                break;
            case EntityAgent entityAgent:
                entityAgent?.WalkInventory(CheckSlot);
                break;
        }
    }

    private bool CheckSlot(ItemSlot slot)
    {
        if (slot?.Empty == true) return true;

        if (slot.Itemstack.Collectible is BlockTorch torch)
        {
            var IsExtinct = slot.Itemstack.Collectible.Variant["state"] == "extinct";
            if (entity.World.Side == EnumAppSide.Server && entity.Swimming && !IsExtinct && torch.ExtinctVariant != null)
            {
                entity.PlayExtinguishSound(entity.World);
                slot.Itemstack = new ItemStack(torch.ExtinctVariant, slot.Itemstack.StackSize);
                slot.MarkDirty();
            }
        }

        return true;
    }

    public override string PropertyName() => "nowaterproofinventory";
}