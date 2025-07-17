using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace AlmostThere
{
    public class AlmostThereWorldObjectComp : WorldObjectComp
    {
        public bool cacheDirty = true;
        public bool cachedResult = false;
        public bool almostThere = true;
        public bool fullyIgnoreRest = false;
        public bool forceRest = false;
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref almostThere, "almostThere", true);
            Scribe_Values.Look(ref fullyIgnoreRest, "fullyIgnoreRest", false);
            Scribe_Values.Look(ref forceRest, "forceRest", false);
        }
        public override IEnumerable<Gizmo> GetGizmos()
        {
            if (parent.Faction == Faction.OfPlayer)
            {
                yield return new Command_Toggle
                {
                    isActive = () => almostThere,
                    defaultLabel = "AT_Command_AlmostThere_Label".Translate(),
                    defaultDesc = "AT_Command_AlmostThere_Description".Translate(AlmostThereSettings.AlmostThereHours),
                    icon = ContentFinder<Texture2D>.Get("UI/" + "AlmostThere", true),
                    toggleAction = delegate
                    {
                        cacheDirty = true;
                        almostThere = !almostThere;
                        if (almostThere)
                        {
                            forceRest = false;
                            fullyIgnoreRest = false;
                        }
                    }
                };
                yield return new Command_Toggle
                {
                    isActive = () => fullyIgnoreRest,
                    defaultLabel = "AT_Command_DontRest_Label".Translate(),
                    defaultDesc = "AT_Command_DontRest_Description".Translate(),
                    icon = ContentFinder<Texture2D>.Get("UI/" + "DontRest", true),
                    toggleAction = delegate
                    {
                        cacheDirty = true;
                        fullyIgnoreRest = !fullyIgnoreRest;
                        if (fullyIgnoreRest)
                        {
                            forceRest = false;
                            almostThere = false;
                        }
                    }
                };
                yield return new Command_Toggle
                {
                    isActive = () => forceRest,
                    defaultLabel = "AT_Command_ForceRest_Title".Translate(),
                    defaultDesc = "AT_Command_ForceRest_Description".Translate(),
                    icon = ContentFinder<Texture2D>.Get("UI/" + "ForceRest", true),
                    toggleAction = delegate
                    {
                        cacheDirty = true;
                        forceRest = !forceRest;
                        if (forceRest)
                        {
                            almostThere = false;
                            fullyIgnoreRest = false;
                        }
                    }
                };
            }
            yield break;
        }
    }

    public class WorldObjectCompProperties_AlmostThere : WorldObjectCompProperties
    {
        public WorldObjectCompProperties_AlmostThere()
        {
            compClass = typeof(AlmostThereWorldObjectComp);
        }
    }
}
