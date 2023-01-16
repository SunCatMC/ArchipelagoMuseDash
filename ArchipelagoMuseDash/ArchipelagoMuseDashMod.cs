﻿using System;
using ArchipelagoMuseDash;
using ArchipelagoMuseDash.Archipelago;
using ArchipelagoMuseDash.Logging;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(ArchipelagoMuseDashMod), "Archipelago Muse Dash", "0.2.0", "DeamonHunter")]
[assembly: MelonGame("PeroPeroGames", "MuseDash")]

namespace ArchipelagoMuseDash {
    /// <summary>
    /// The Archipelago mod. MelonLoader's entry point.
    /// </summary>
    public class ArchipelagoMuseDashMod : MelonMod {
        public override void OnInitializeMelon() {
            base.OnInitializeMelon();
            ArchipelagoStatic.ArchLogger = new ArchLogger();
            ArchipelagoStatic.Login = new ArchipelagoLogin();
            ArchipelagoStatic.SessionHandler = new SessionHandler();
            LoadExternalAssets();
        }

        void LoadExternalAssets() {
            using (var stream = MelonAssembly.Assembly.GetManifestResourceStream("ArchipelagoMuseDash.Assets.ArchIcon.png")) {
                if (stream == null) {
                    ArchipelagoStatic.ArchLogger.Warning("LoadExternalAssets", "Help");
                    return;
                }

                using (var ms = new System.IO.MemoryStream()) {
                    stream.CopyTo(ms);

                    var archIconTexture = new Texture2D(1, 1);
                    archIconTexture.hideFlags |= HideFlags.DontUnloadUnusedAsset;
                    archIconTexture.wrapMode = TextureWrapMode.Clamp;

                    ImageConversion.LoadImage(archIconTexture, ms.ToArray());
                    ArchipelagoStatic.ArchipelagoIcon = archIconTexture;
                }
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName) {
            base.OnSceneWasLoaded(buildIndex, sceneName);

            try {
                ArchipelagoStatic.ArchLogger.Log("Scene Load", $"{sceneName} was loaded.");

                ArchipelagoStatic.CurrentScene = sceneName;

                if (ArchipelagoStatic.Login.HasBeenShown || sceneName != "UISystem_PC")
                    return;

                ArchipelagoStatic.Login.ShowLoginScreen();
            }
            catch (Exception e) {
                ArchipelagoStatic.ArchLogger.Error("Scene Load", e);
            }
        }

        public override void OnUpdate() {
            base.OnUpdate();

            if (!ArchipelagoStatic.SessionHandler.IsLoggedIn)
                return;

            ArchipelagoStatic.SessionHandler.OnUpdate();
        }

        public override void OnLateUpdate() {
            base.OnLateUpdate();
            if (!ArchipelagoStatic.SessionHandler.IsLoggedIn)
                return;

            ArchipelagoStatic.SessionHandler.OnLateUpdate();
        }
    }
}
