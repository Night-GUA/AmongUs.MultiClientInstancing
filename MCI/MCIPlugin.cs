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
using UnityEngine.Scripting;

namespace MCI
{
    [BepInAutoPlugin("yu.au.mci", "MCI", VersionString)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(SubmergedCompatibility.SUBMERGED_GUID, BepInDependency.DependencyFlags.SoftDependency)]
    public partial class MCIPlugin : BasePlugin
    {
        public const string VersionString = "0.0.9";
        public const string AfterVersionString = "20240608";
        internal static Version vVersion = new(VersionString);
        public Harmony Harmony { get; } = new(Id);

        public static string[] language = CultureInfo.CurrentCulture.Name.Split("-");

        public static MCIPlugin Singleton { get; private set; } = null;

        public static bool Enabled { get; set; } = true;
        public static bool IKnowWhatImDoing { get; set; } = false;
        public static bool IfChinese = language[0] == "zh" ? true : false;
        public static string VersionTextString = IfChinese ? "春天都过一半了 你的春天呢？~" : "The spring equinox is coming";

        public static string RobotName { get; set; } = IfChinese ? "Yu机器人" : "Yu Bot";

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
    /*
     * Todo:
     * 重写更新（加入对AVS变量的判断）
     * 删除背景（如TONX）
     * 换LOGO（如TONX）
     * 换首页按钮图标（如TONX）
     * 添加技能图标（如TOR）
     */
}