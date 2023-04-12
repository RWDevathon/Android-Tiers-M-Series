using RimWorld;
using Verse;

namespace ATReforged
{
    public class GenStep_DownedM4 : GenStep_DownedRefugee
    {
        protected override void ScatterAt(IntVec3 loc, Map map, GenStepParams parms, int count = 1)
        {
            Pawn pawn;
            if (parms.sitePart != null && parms.sitePart.things != null && parms.sitePart.things.Any)
            {
                pawn = (Pawn)parms.sitePart.things.Take(parms.sitePart.things[0]);
            }
            else
            {
                DownedM4Comp component = map.Parent.GetComponent<DownedM4Comp>();
                if (component == null || !component.pawn.Any)
                {
                    pawn = PawnGenerator.GeneratePawn(new PawnGenerationRequest(ATM_PawnKindDefOf.ATM_M4JellymanColony, Faction.OfAncients, PawnGenerationContext.NonPlayer, map.Tile, forceGenerateNewPawn: true, canGeneratePawnRelations: false, mustBeCapableOfViolence: false, allowFood: false, allowAddictions: false));
                }
                else
                {
                    pawn = component.pawn.Take(component.pawn[0]);
                }
            }

            // Give the M4 a level 3 Psylink if Royalty is active.
            if (ModsConfig.RoyaltyActive)
            {
                Hediff_Level psylinkHediff = HediffMaker.MakeHediff(HediffDefOf.PsychicAmplifier, pawn, pawn.health.hediffSet.GetBrain()) as Hediff_Level;
                pawn.health.AddHediff(psylinkHediff, pawn.health.hediffSet.GetBrain());
                psylinkHediff.SetLevelTo(3);
            }

            HealthUtility.DamageUntilDowned(pawn, false);
            GenSpawn.Spawn(pawn, loc, map);
            pawn.mindState.WillJoinColonyIfRescued = true;
            MapGenerator.rootsToUnfog.Add(loc);
            MapGenerator.SetVar("RectOfInterest", CellRect.CenteredOn(loc, 1, 1));
        }
    }
}