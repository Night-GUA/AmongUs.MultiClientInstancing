using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System;
using UnityEngine.SceneManagement;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MCI
{
    [BepInAutoPlugin("dragonbreath.au.mci", "MCI", VersionString)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(SubmergedCompatibility.SUBMERGED_GUID, BepInDependency.DependencyFlags.SoftDependency)]
    public partial class MCIPlugin : BasePlugin
    {
        public const string VersionString = "0.0.8";
        internal static Version vVersion = new(VersionString);
        public Harmony Harmony { get; } = new(Id);

        public static string[] language = CultureInfo.CurrentCulture.Name.Split("-");

        public static MCIPlugin Singleton { get; private set; } = null;

        public static bool Enabled { get; set; } = true;
        public static bool IKnowWhatImDoing { get; set; } = false;
        public static bool IfChinese = language[0] == "zh" ? true : false;
        public static bool IfDebug = false;
#if Debug
IfDebug = true();
#endif

        public static string RobotName { get; set; } = IfChinese ? "Yu宝机器人" : "Yu Bot";

        public override void Load()
        {
            if (Singleton != null) return;
            Singleton = this;

            Harmony.PatchAll();
            UpdateChecker.CheckForUpdate();

            SubmergedCompatibility.Initialize();

            SceneManager.add_sceneLoaded((Action<Scene, LoadSceneMode>)((scene, _) =>
            {
                if (scene.name == "MainMenu")
                {
                    ModManager.Instance.ShowModStamp();
                }
            }));
        }

        internal static bool Persistence = true;
    }

    [HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.Update))]
    public static class CountdownPatch
    {
        public static void Prefix(GameStartManager __instance)
        {
            __instance.countDownTimer = 0;
        }
    }
}