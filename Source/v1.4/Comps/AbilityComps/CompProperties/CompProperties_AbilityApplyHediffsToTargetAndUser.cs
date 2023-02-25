using RimWorld;
using Verse;

namespace ATReforged
{
    public class CompProperties_AbilityApplyHediffsToTargetAndUser : CompProperties_AbilityEffect
    {
        public CompProperties_AbilityApplyHediffsToTargetAndUser()
        {
            compClass = typeof(CompAbilityEffect_ApplyHediffsToTargetAndUser);
        }

        public HediffDef hediffDefToApply;

        public HediffDef hediffDefToSuffer;

        public bool affectedByBodySize;

        public float bodySizeRatioEffect = 0.5f;
    }
}
