using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData;

namespace StardewValleyExpanded;

internal static class EndNexusMusic
{
    private static IMonitor Monitor;

    public static void Hook(Harmony harmony, IMonitor monitor)
    {
        EndNexusMusic.Monitor = monitor;

        harmony.Patch(
            original: AccessTools.Method(typeof(GameLocation), "resetLocalState"),
            prefix: new HarmonyMethod(typeof(EndNexusMusic), nameof(EndNexusMusic.After_ResetLocalState))
        );
        harmony.Patch(
            original: AccessTools.Method(typeof(GameLocation), nameof(GameLocation.cleanupBeforePlayerExit)),
            prefix: new HarmonyMethod(typeof(EndNexusMusic), nameof(EndNexusMusic.After_CleanupBeforePlayerExit))
        );
    }


    private static void After_ResetLocalState(GameLocation __instance)
    {
        if (Game1.currentLocation.NameOrUniqueName == "Custom_EnchantedGrove")
        {
            Game1.changeMusicTrack("FlashShifter.StardewValleyExpandedCP_Nexus", music_context: MusicContext.Default);
        }

        if (Game1.currentLocation.NameOrUniqueName == "Custom_JojaEmporium")
        {
            Game1.changeMusicTrack("movieTheater", music_context: MusicContext.Default);
        }
    }

    private static void After_CleanupBeforePlayerExit(GameLocation __instance)
    {
        if (Game1.currentLocation.NameOrUniqueName == "Custom_EnchantedGrove")
        {
            Game1.changeMusicTrack("none", music_context: MusicContext.Default);
        }

        if (Game1.currentLocation.NameOrUniqueName == "Custom_JojaEmporium")
        {
            Game1.changeMusicTrack("none", music_context: MusicContext.Default);
        }
    }
}
