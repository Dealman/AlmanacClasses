﻿using UnityEngine;

namespace AlmanacClasses.Classes.Abilities.Shaman;

public static class ShamanSpawn
{
    public static void TriggerShamanSpawn(GameObject fallBack, Talent talent)
    {
        GameObject prefab = talent.GetCreaturesByLevel(talent.GetLevel()) ?? fallBack;
        if (!prefab.TryGetComponent(out Humanoid _)) return;
        AlmanacClassesPlugin._Plugin.StartCoroutine(SpawnSystem.DelayedMultipleSpawn(
            prefab, "Friendly " + prefab.name.Replace("_", string.Empty), 
            talent.GetCreatureByLevelLevel(talent.GetLevel()), 
            talent.GetCreaturesByLevelLength(talent.GetLevel()), 
            talent.GetCreatureByLevelLevel(talent.GetLevel()))
        );
    }
}