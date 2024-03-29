using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using Reptile;

namespace ChromaCam
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class Core : BaseUnityPlugin
    {
        public const string PluginGUID = "trpg.brc.chromacam";
        public const string PluginName = "ChromaCam";
        public const string PluginVersion = "1.0.0";

        public static ConfigEntry<KeyCode> toggleKey;
        public static ConfigEntry<Color> bgColor;

        private void Awake()
        {
            toggleKey = Config.Bind("General",
                "Toggle Key",
                KeyCode.Equals);

            bgColor = Config.Bind("General",
                "Background Color",
                Color.green);
        }

        private void Update()
        {
            if (Input.GetKeyDown(toggleKey.Value) && Reptile.Core.Instance.BaseModule.IsPlayingInStage && !Reptile.Core.Instance.BaseModule.IsLoading)
            {
                Camera cam = Traverse.Create(WorldHandler.instance.GetCurrentPlayer()).Field("cam").Field<Camera>("cam").Value;

                if (cam.clearFlags != CameraClearFlags.Color)
                {
                    cam.clearFlags = CameraClearFlags.Color;
                    cam.backgroundColor = bgColor.Value;
                    cam.cullingMask = 32768;
                }
                else
                {
                    cam.clearFlags = CameraClearFlags.Skybox;
                    cam.backgroundColor = EffectsUI.niceBlack;
                    cam.cullingMask = -1292392941;
                }
            }
        }
    }
}
