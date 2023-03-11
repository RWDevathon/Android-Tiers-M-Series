using Verse;
using RimWorld;

namespace ATReforged
{
    [DefOf]
    public static class ATM_ThingDefOf
    {
        static ATM_ThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ATM_ThingDefOf));
        }

        public static ThingDef ATM_MechFallTargetterBeam;

    }
}
