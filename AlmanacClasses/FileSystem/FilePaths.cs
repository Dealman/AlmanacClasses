﻿using System.IO;
using BepInEx;

namespace AlmanacClasses.FileSystem;

public static class FilePaths
{
    private static readonly string FolderPath = Paths.ConfigPath + Path.DirectorySeparatorChar + "AlmanacClasses";
    public static readonly string ExperienceFolderPath = FolderPath + Path.DirectorySeparatorChar + "Experience";
    // public static readonly string ExperienceFilePath = ExperienceFolderPath + Path.DirectorySeparatorChar + "AlmanacExperienceMap.yml";
    public static readonly string TierExperienceFilePath = ExperienceFolderPath + Path.DirectorySeparatorChar + "ExperienceMap.yml";
    public static readonly string StaticExperienceFilePath = ExperienceFolderPath + Path.DirectorySeparatorChar + "StaticExperienceMap.yml";

    public static readonly string CustomBackgroundFilePath = FolderPath + Path.DirectorySeparatorChar + "CustomBackground";

    public static void CreateFolders()
    {
        if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);
        if (!Directory.Exists(ExperienceFolderPath)) Directory.CreateDirectory(ExperienceFolderPath);
        if (!Directory.Exists(CustomBackgroundFilePath)) Directory.CreateDirectory(CustomBackgroundFilePath);
    }
}