using HarmonyLib;
using RimWorld;
using Verse;

namespace ATReforged
{
    // With M-Series enabled, instead of the transcendant state for fractal pills being permanent, it instead turns the pawn into a newborn Jellyman
    internal class Hediff_Fractal_Patch
    {
        [HarmonyPatch(typeof(Hediff_Fractal), "DoTranscendance")]
        public class DoTranscendance_Patch
        {
            [HarmonyPostfix]
            public static void Listener(Pawn pawn)
            {
                string label = "ATM_FractalTranscendance".Translate();
                label = label.AdjustedFor(pawn);
                string text = "ATM_FractalTranscendanceDesc".Translate(pawn.Name.ToStringShort);
                Find.LetterStack.ReceiveLetter(label, text, LetterDefOf.PositiveEvent, pawn);

                PawnGenerationRequest request = new PawnGenerationRequest(ATM_PawnKindDefOf.ATM_M4JellymanColony, Faction.OfPlayer, PawnGenerationContext.NonPlayer, forceGenerateNewPawn: true, canGeneratePawnRelations: false, fixedBiologicalAge: 0, fixedChronologicalAge: 0, forceNoIdeo: true);
                Pawn jellyman = PawnGenerator.GeneratePawn(request);

                Utils.Duplicate(pawn, jellyman, false, false);
                GenSpawn.Spawn(jellyman, pawn.Position, pawn.Map);

                // If Royalty is active, Jellyman will have a Psylink at level 3 on spawn.
                if (ModsConfig.RoyaltyActive)
                {
                    Hediff_Level psylinkHediff = HediffMaker.MakeHediff(HediffDefOf.PsychicAmplifier, jellyman, jellyman.health.hediffSet.GetBrain()) as Hediff_Level;
                    jellyman.health.AddHediff(psylinkHediff, jellyman.health.hediffSet.GetBrain());
                    psylinkHediff.SetLevelTo(3);
                }

                pawn.Destroy();
            }
        }
    }
}