/**
 * Sons Save Clock - Save Game Clock for Sons of the Forest
 * Copyright (C) 2023  eluxo <code@eluxo.net>
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 */

using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using System;
using System.Reflection;
using UnityEngine;

namespace SonsSaveClock
{
    [BepInPlugin(BuildInfo.PLUGIN_GUID, BuildInfo.PLUGIN_NAME, BuildInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        public static ManualLogSource log;
        public static ConfigEntry<int> configFontSize;
        public static ConfigEntry<int> configBoxHeight;
        public static ConfigEntry<int> configBoxWidth;
        public static ConfigEntry<bool> configShowIcon;
        public static ConfigEntry<bool> configCenterHorizontal;
        public static ConfigEntry<int> configLeft;
        public static ConfigEntry<int> configTop;

        public Plugin()
        {
            log = Log;
        }

        public override void Load()
        {
            Log.LogInfo("Plugin " + BuildInfo.PLUGIN_GUID + " is loaded!");
            try
            {
                LoadConfig();
                RegisterTypeOptions();
                RegisterPatches();
            }
            catch(Exception ex)
            {
                Log.LogError("Loading plugin has failed.");
                Log.LogError(ex);
            }
        }

        private void LoadConfig()
        {
            configFontSize = Config.Bind<int>(
                "General",
                "FontSize",
                24,
                "Change the size of the font on your taste. Make sure that you also adjust height and width of the box accordingly.");

            configBoxHeight = Config.Bind<int>(
                "General",
                "BoxHeight",
                28,
                "Height of the box shown in the game.");

            configBoxWidth = Config.Bind<int>(
                "General",
                "BoxWidth",
                160,
                "Width of the box shown in the game.");

            configShowIcon = Config.Bind<bool>(
                "General",
                "ShowIcon",
                true,
                "Shows the disk icon next to the text label. Set to false, if you don't like to see it."
                );

            configCenterHorizontal = Config.Bind<bool>(
                "General",
                "CenterHorizontal",
                true,
                "Adjusts the box on the screen center. If disabled, it will be adjusted from the left of your screen."
                );

            configLeft = Config.Bind<int>(
                "General",
                "Left",
                0,
                "Left adjustment of the box."
                );

            configTop = Config.Bind<int>(
                "General",
                "Top",
                20,
                "Top adjustment of the box"
                );
        }

        private void RegisterTypeOptions()
        {
            Log.LogInfo("Register IL2CPP object.");
            ClassInjector.RegisterTypeInIl2Cpp<ModUiComponent>();
            GameObject gameObject = new GameObject(BuildInfo.PLUGIN_GUID);
            gameObject.AddComponent<ModUiComponent>();
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            GameObject.DontDestroyOnLoad(gameObject);
            Log.LogInfo("Registering IL2CPP object done.");
        }

        private void RegisterPatches()
        {
            Log.LogInfo("Register Harmony patches.");
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
            Log.LogInfo("Registering Harmony patches done.");
        }
    }
}
