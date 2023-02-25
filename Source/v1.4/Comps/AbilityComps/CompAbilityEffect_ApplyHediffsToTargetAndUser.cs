using RimWorld;
using Verse;

namespace ATReforged
{
    /// <summary>
    /// When this ability is used, a Hediff provided by Props will be applied to the target and (optionally) a separate one to the activator.
    /// The hediff applied to the target is constant, and the hediff applied to the activator has a severity that is based on their comparative body sizes if enabled in Props.
    /// This ability can not be used on any pawn that would cause the severity of the Hediff on the activator to exceed 1 (if one exists).
    /// </summary>
    public class CompAbilityEffect_ApplyHediffsToTargetAndUser : CompAbilityEffect
    {
        public new CompProperties_AbilityApplyHediffsToTargetAndUser Props => (CompProperties_AbilityApplyHediffsToTargetAndUser)props;

        // Apply the hediffs to the target and (if a Hediff exists for it) the user.
        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            Pawn pawn = target.Pawn;
            if (pawn != null)
            {
                if (Props.hediffDefToApply != null)
                {
                    pawn.health.AddHediff(Props.hediffDefToApply);
                }

                if (Props.hediffDefToSuffer != null)
                {
                    Hediff sufferedHediff = parent.pawn.health.hediffSet.GetFirstHediffOfDef(Props.hediffDefToSuffer);
                    // Hediff already exists, add to its severity.
                    if (sufferedHediff != null)
                    {
                        sufferedHediff.Severity += pawn.BodySize * Props.bodySizeRatioEffect / parent.pawn.BodySize;
                    }
                    // Hediff does not exist yet, create and apply one with proper severity.
                    else
                    {
                        sufferedHediff = HediffMaker.MakeHediff(Props.hediffDefToSuffer, pawn);
                        sufferedHediff.Severity = pawn.BodySize * Props.bodySizeRatioEffect / parent.pawn.BodySize;
                        parent.pawn.health.AddHediff(sufferedHediff);
                    }
                }
            }
        }

        // Precheck some information on the target pawn before checking for validity.
        public override bool CanApplyOn(LocalTargetInfo target, LocalTargetInfo dest)
        {
            Pawn pawn = target.Pawn;
            if (pawn != null)
            {
                if (pawn.Downed)
                {
                    return false;
                }
            }
            return Valid(target);
        }

        // Check if the target is valid based on Props.
        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            Pawn pawn = target.Pawn;
            if (pawn != null)
            {
                // If the user does not suffer a hediff upon use, then it is valid as long as the target is a pawn.
                if (Props.hediffDefToSuffer == null)
                {
                    return true;
                }

                // If BodySize comparison is enabled, compare the two pawns' body sizes and determine whether the ratio would cause the severity of the user to exceed 1.
                if (Props.affectedByBodySize)
                {
                    // Ensure that the difference between body sizes is not too extreme.
                    if (pawn.BodySize * Props.bodySizeRatioEffect >= parent.pawn.BodySize)
                    {
                        return false;
                    }

                    // If it is possible to apply it, make sure that an existing instance would not be modified to exceed the severity limit.
                    Hediff sufferedHediff = parent.pawn.health.hediffSet.GetFirstHediffOfDef(Props.hediffDefToSuffer);
                    if (sufferedHediff != null && sufferedHediff.Severity + (pawn.BodySize * Props.bodySizeRatioEffect / parent.pawn.BodySize) > 1)
                    {
                        return false;
                    }
                }
                // If no comparison, just make sure that there is no pre-existing hediff on the activator.
                else
                {
                    Hediff sufferedHediff = parent.pawn.health.hediffSet.GetFirstHediffOfDef(Props.hediffDefToSuffer);
                    if (sufferedHediff != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
