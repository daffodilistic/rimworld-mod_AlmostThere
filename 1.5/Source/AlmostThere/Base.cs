using RimWorld;
using UnityEngine;
using Verse;

namespace AlmostThere
{
    public class AlmostThereMod : Mod
    {
        public AlmostThereMod(ModContentPack content) : base(content)
        {
            GetSettings<AlmostThereSettings>();
        }
        public override string SettingsCategory()
        {
            return "Almost There!";
        }
        public override void DoSettingsWindowContents(Rect rect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(rect);
            AlmostThereSettings.AlmostThereHours = (int)listingStandard.SliderLabeled("AT_AlmostThereHours_Title".Translate(Settings.AlmostThereHours), Settings.AlmostThereHours, 0, 1000, 0.25f);
            listingStandard.End();
            base.DoSettingsWindowContents(rect);
        }
    }

    class AlmostThereSettings : ModSettings
    {
        public static int AlmostThereHours = 4;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref AlmostThereHours, "AlmostThereHours", 4, false);
        }
    }
}
