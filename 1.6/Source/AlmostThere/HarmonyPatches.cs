using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace AlmostThere
{
    [StaticConstructorOnStartup]
    class HarmonyPatches
    {
        static HarmonyPatches()
        {

            var harmony = new Harmony("cat2002.almostthere");
            harmony.PatchAll();
        }
    }
    [HarmonyPatch(typeof(Caravan), "get_NightResting")]
    class Patch_Caravan_get_NightResting
    {
        static void Postfix(Caravan __instance, ref bool __result)
        {
            //var playerCaravans = Current.Game.World.worldObjects.Caravans;

            AlmostThereWorldObjectComp comp = __instance.GetComponent<AlmostThereWorldObjectComp>();
            if (comp != null)
            {
                if (!__instance.IsHashIntervalTick(60) && !comp.cacheDirty)
                {
                    __result = comp.cachedResult;
                    return;
                }
                else if (comp.forceRest)
                {
                    __result = true;
                }
                else if (comp.fullyIgnoreRest)
                {
                    __result = false;
                }
                else if (comp.almostThere)
                {
                    var estimatedTicks = (float)CaravanArrivalTimeEstimator.EstimatedTicksToArrive(__instance, allowCaching: true);
                    var restTicksLeft = CaravanNightRestUtility.LeftRestTicksAt(__instance.Tile, Find.TickManager.TicksAbs);
                    estimatedTicks -= restTicksLeft;
                    if (estimatedTicks / GenDate.TicksPerHour < AlmostThereSettings.AlmostThereHours)
                    {
                        __result = false;
                    }
                }
                comp.cacheDirty = false;
                comp.cachedResult = __result;
            }
        }
    }
}
