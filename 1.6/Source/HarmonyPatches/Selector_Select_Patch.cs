using HarmonyLib;
using RimWorld;
using Verse;
using Verse.Sound;
namespace Discoveries
{
    [HarmonyPatch(typeof(Selector), nameof(Selector.Select))]
    public static class Selector_Select_Patch
    {
        public static void Postfix(Selector __instance, object obj)
        {
            if (!DiscoveriesMod.settings.discoveryEnabled) return;
            if (obj is Thing thing && __instance.SingleSelectedThing == thing)
            {
                Thing discoveryThing = DiscoveryTracker.GetDiscoveryThing(thing);
                if (DiscoveryTracker.IsDiscovered(discoveryThing)) return;
                if (DiscoveryTracker.ShouldExcludeThing(discoveryThing)) return;
                if (DiscoveriesMod.settings.displayOnlyUnlocks && !HasUnlock(discoveryThing)) return;
                __instance.ClearSelection();
                DiscoveryTracker.MarkDiscovered(discoveryThing);
                DefsOf.Disc_Discovery.PlayOneShotOnCamera();
                Find.WindowStack.Add(new Window_Discovery(discoveryThing));
            }
        }
        private static bool HasUnlock(Thing thing)
        {
            if (thing.def.HasModExtension<UnlockResearchOnDiscovery>())
            {
                return true;
            }
            return false;
        }
    }
}
