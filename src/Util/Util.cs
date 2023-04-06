using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;

namespace NoWaterproofInventories;

public static class Util
{
    public static void PlayExtinguishSound(this Entity byEntity, IWorldAccessor world)
    {
        world.PlaySoundAt(new AssetLocation("sounds/effect/extinguish"), byEntity.Pos.X + 0.5, byEntity.Pos.Y + 0.75, byEntity.Pos.Z + 0.5, null, false, 16);
    }

    public static void PlayExtinguishSound(this BlockPos pos, IWorldAccessor world)
    {
        world.PlaySoundAt(new AssetLocation("sounds/effect/extinguish"), pos.X + 0.5, pos.Y + 0.75, pos.Z + 0.5, null, false, 16);
    }
}