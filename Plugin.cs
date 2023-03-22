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

        public Plugin()
        {
            log = Log;
        }

        public override void Load()
        {
            Log.LogInfo("Plugin " + BuildInfo.PLUGIN_GUID + " is loaded!");
            try
            {
                RegisterTypeOptions();
                RegisterPatches();
            }
            catch(Exception ex)
            {
                Log.LogError("Loading plugin has failed.");
                Log.LogError(ex);
            }
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
