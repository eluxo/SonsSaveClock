/**
 * Sons Save Clock - Save Game Clock for Sons of the Forest
 * Copyright (C) 2023  eluxo <code@eluxo.net>
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 */

using UnityEngine;
using TheForest.Utils;
using System;
using Sons.Save;
using System.Reflection;

namespace SonsSaveClock
{
    internal class ModUiComponent : MonoBehaviour
    {
        private bool old_flag;
        private DateTime lastSaveTime;
        private Il2CppSystem.Action<bool> saveCallback;
        private Il2CppSystem.Action loadCallback;
        private TextureLoader discTexture;
        
        public ModUiComponent()
        {
            this.saveCallback = (Il2CppSystem.Action<bool>)delegate (bool v)
            {
                this.updateSaveTime();
            };

            this.loadCallback = (Il2CppSystem.Action)delegate ()
            {
                this.updateSaveTime();
            };

            this.updateSaveTime();
            this.discTexture = new TextureLoader("SonsSaveClock.Assets.Textures.disc.tex");
        }

        void updateSaveTime()
        {
            Plugin.log.LogDebug("Update timestamp of last save.");
            this.lastSaveTime = DateTime.UtcNow;
        }

        void OnGUI()
        {
            bool flag = LocalPlayer.IsInWorld;

            if (flag != this.old_flag)
            {
                if (flag)
                {
                    Plugin.log.LogInfo("Switching into world.");
                    this.wrapCallback();
                }
                this.old_flag = flag;
            }

            if (flag)
            {
                string timeText = this.getTimeText();
                this.drawText(timeText);
            }
        }

        private void wrapCallback()
        {
            SaveGameManager manager = GameObject.Find("_SonsSaveGameManager_").GetComponent<SaveGameManager>();
            if (manager == null)
            {
                Plugin.log.LogError("SaveGameManager instance is null");
                return;
            }

            if (!manager._afterSaveCallback.Contains(this.saveCallback))
            {
                Plugin.log.LogInfo("register save callback");
                manager._afterSaveCallback.Add(this.saveCallback);
            }

            if (!manager._afterLoadCallback.Contains(this.loadCallback))
            {
                Plugin.log.LogInfo("register load callback");
                manager._afterLoadCallback.Add(this.loadCallback);
            }
        }

        private string getTimeText()
        {
            TimeSpan diff = DateTime.UtcNow - this.lastSaveTime;
            return String.Format("{0,2:D2}:{1,2:D2}:{2,2:D2}", diff.Hours, diff.Minutes, diff.Seconds);
        }

        private void drawText(string timeText)
        {
            Texture2D tex = this.discTexture.getTexture();

            int center = Screen.width / 2;
            int width = Plugin.configBoxWidth.Value;
            int height = Plugin.configBoxHeight.Value;

            int left = Plugin.configLeft.Value;
            if (Plugin.configCenterHorizontal.Value)
            {
                left += center - (width / 2);
            }
            int top = Plugin.configTop.Value;

            GUI.BeginGroup(new Rect(left, top, width, height));
            if (Plugin.configShowIcon.Value) { 
                GUI.DrawTexture(new Rect(width - height, 0, height, height), tex, ScaleMode.ScaleToFit);
            }

            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.black;
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = Plugin.configFontSize.Value;
            style.fontStyle = FontStyle.Bold;
            GUI.Label(new Rect(0, 0, width, height), timeText, style);

            style.normal.textColor = Color.white;
            GUI.Label(new Rect(- 2, - 2, width, height), timeText, style);
            GUI.EndGroup();
        }
    }
}
