using System;
using System.Collections.Generic;
using AlmanacClasses.Managers;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace AlmanacClasses.Classes.Abilities.Core;

// TODO: Description for Forager has been updated for clarity, Korean + Brazilian translation?
public static class Forager
{
    [HarmonyPatch(typeof(Pickable), nameof(Pickable.RPC_Pick))]
    private static class Pickable_RPC_Pick_Prefix
    {
        private static void Prefix(Pickable __instance)
        {
            if (!PlayerManager.m_playerTalents.TryGetValue("Forager", out Talent talent)) return;
            if (!__instance.m_nview.IsOwner() || __instance.m_picked) return;
            if (!__instance.m_itemPrefab) return;
            if (!__instance.m_itemPrefab.TryGetComponent(out ItemDrop component)) return;
            if (component.m_itemData.m_shared.m_itemType is not ItemDrop.ItemData.ItemType.Consumable)
            {
                if (!IsForageItem(__instance.m_itemPrefab.name, talent.GetCustomForageItems())) return;
            }
            var bonusChance = talent.GetForageModifier(talent.GetLevel()) - 1.0f;
            if (ClassUtilities.RandomBoolWithWeight(bonusChance))
            {
                int scaledDropNum = (__instance.m_dontScale ? __instance.m_amount : Mathf.Max(__instance.m_minAmountScaled, Game.instance.ScaleDrops(__instance.m_itemPrefab, __instance.m_amount)));
                __instance.Drop(__instance.m_itemPrefab, 1, scaledDropNum);
                AlmanacClassesPlugin.AlmanacClassesLogger.LogDebug($"[Forager]: Successful roll, extra dropped: {scaledDropNum}");
                DisplayText.ShowText(Color.white, __instance.transform.position, $"Forager Bonus: +{scaledDropNum}");
            }
        }
    }

    [HarmonyPatch(typeof(Pickable), nameof(Pickable.GetHoverText))]
    private static class Pickable_GetHoverText_Postfix
    {
        private static void Postfix(Pickable __instance, ref string __result)
        {
            if (!PlayerManager.m_playerTalents.TryGetValue("Forager", out Talent talent)) return;
            if (!__instance.m_nview.IsOwner() || __instance.m_picked) return;
            if (!__instance.m_itemPrefab) return;
            if (!__instance.m_itemPrefab.TryGetComponent(out ItemDrop component)) return;
            if (component.m_itemData.m_shared.m_itemType is not ItemDrop.ItemData.ItemType.Consumable)
            {
                if (!IsForageItem(__instance.m_itemPrefab.name, talent.GetCustomForageItems())) return;
            }

            var bonusChance = (talent.GetForageModifier(talent.GetLevel()) - 1.0f) * 100.0f;
            __result += Localization.instance.Localize($"\n[{talent.GetName()} <color=orange>{talent.GetLevel()}</color>]: <color=orange>{bonusChance}%</color> Double Drop");
        }
    }

    private static bool IsForageItem(string prefabName, List<string> list) => list.Contains(prefabName);
}