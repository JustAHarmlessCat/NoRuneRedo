using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppSystem.Collections.Generic;

namespace NoRuneRedo;

[BepInPlugin("com.justaharmlesscat.noruneredo", "No Rune Redo", "1.0.0")]
public class Plugin : BasePlugin
{
    public override void Load()
    {
        Log.LogInfo("NoRuneRedo is loaded");
        
        var harmony = new Harmony("com.justaharmlesscat.noruneredo");
        harmony.PatchAll(typeof(WeaponPatches));
    }

    [HarmonyPatch(typeof(WeaponBehavior))]
    public static class WeaponPatches
    {
        [HarmonyPatch("HandleRuneSystem")]
        [HarmonyPrefix]
        public static bool PrefixHandle(WeaponBehavior __instance)
        {
            if (__instance.runes != null && __instance.runes.Count > 0)
            {
                return false;
            }
            return true;
        }
        
        [HarmonyPatch("ShouldActivateRuneSystem")]
        [HarmonyPrefix]
        public static bool ActivateRuneSystem(WeaponBehavior __instance, ref bool __result)
        {
            if (__instance.runes != null && __instance.runes.Count > 0)
            {
                __result = false;
                return false;
            }
            return true;
        }

        [HarmonyPatch("ClearRunes")]
        [HarmonyPrefix] 
        public static bool PrefixClear(WeaponBehavior __instance)
        {
            if (__instance.runes != null && __instance.runes.Count > 0) return false;
            return true; 
        }
        
        [HarmonyPatch(nameof(WeaponBehavior.CraftSpell))]
        [HarmonyPrefix]
        public static bool PrefixCraft(WeaponBehavior __instance)
        {
            if (__instance.runes != null && __instance.runes.Count > 0) return false;
            return true;
        }
    }
}