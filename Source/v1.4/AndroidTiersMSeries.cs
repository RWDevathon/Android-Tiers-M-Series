using HarmonyLib;
using System.Reflection;
using Verse;

namespace ATReforged
{
    public class ATReforged : Mod
    {
        public ATReforged(ModContentPack content) : base(content)
        {
            new Harmony("ATMSeries").PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
