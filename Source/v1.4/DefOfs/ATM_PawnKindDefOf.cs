using Verse;
using RimWorld;

namespace ATReforged
{
    [DefOf]
    public static class ATM_PawnKindDefOf
    {
        static ATM_PawnKindDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ATM_PawnKindDefOf));
        }

        public static PawnKindDef ATM_M4JellymanColony;
    }
}