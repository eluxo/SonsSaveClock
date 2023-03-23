/**
 * Sons Save Clock - Save Game Clock for Sons of the Forest
 * Copyright (C) 2023  eluxo <code@eluxo.net>
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 */

using System;
using System.Reflection;
using UnityEngine;

namespace SonsSaveClock
{
    internal class TextureLoader
    {
        private byte[] buffer;
        private int width;
        private int height;
        private Texture2D? texture;

        public TextureLoader(string name)
        {
            Plugin.log.LogInfo("loading texture data for " + name);
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(name);
            if (stream != null)
            {
                var reader = new System.IO.BinaryReader(stream);
                this.buffer = new byte[stream.Length];
                reader.Read(this.buffer);
                this.width = 256;
                this.height = 256;
                Plugin.log.LogInfo("texture data loaded");
            } else
            {
                this.buffer = new byte[16];
                this.width = 2;
                this.height = 2;
                Plugin.log.LogInfo("failed to load texture");
            }
        }

        public Texture2D getTexture()
        {
            if (this.texture == null)
            {
                Plugin.log.LogInfo("create texture object");
                Texture2D tex = new Texture2D(this.width, this.height, TextureFormat.RGBA32, false);
                unsafe
                {
                    fixed (byte* p = buffer)
                    {
                        IntPtr ptr = (IntPtr)p;
                        tex.LoadRawTextureData(ptr, buffer.Length);
                    }
                }
                tex.Apply();
                this.texture = tex;
            }

            return this.texture;
        }
    }
}
