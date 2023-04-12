using Verse;
using RimWorld.Planet;

namespace ATReforged
{
    public class DownedM4Comp : DownedRefugeeComp
    {
        protected override string PawnSaveKey
        {
            get
            {
                return "markFour";
            }
        }

        protected override void RemovePawnOnWorldObjectRemoved()
        {
            pawn.ClearAndDestroyContents(DestroyMode.Vanish);
        }

        public override string CompInspectStringExtra()
        {
            return null;
        }
    }
}