using Verse;

namespace ATReforged
{
    // The Paladin Active Armor hediff giver will give a incoming damage factor that scales with the amount of damage the unit has taken.
    public class HediffGiver_PaladinActiveArmor : HediffGiver
    {
        public override void OnIntervalPassed(Pawn pawn, Hediff cause)
        {
            float pawnHealthpct = pawn.health.summaryHealth.SummaryHealthPercent;

            // A pawn is dead if they have less than 5% health by the SummaryHealthPercent method.
            if (pawnHealthpct >= 0.05f)
            {
                Hediff activeArmorHediff = pawn.health.hediffSet.GetFirstHediffOfDef(hediff);

                if (activeArmorHediff == null)
                {
                    activeArmorHediff = HediffMaker.MakeHediff(hediff, pawn);
                    pawn.health.AddHediff(activeArmorHediff);
                }

                activeArmorHediff.Severity = pawnHealthpct;
            }
        }
    }
}