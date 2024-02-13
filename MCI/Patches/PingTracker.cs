using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

namespace MCI.Patches
{
    [HarmonyPriority(Priority.Low)]
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {
        [HarmonyPostfix]
        public static void Postfix(PingTracker __instance)
        {
            var position = __instance.GetComponent<AspectPosition>();
            position.DistanceFromEdge = new Vector3(3.6f, 0.1f, 0);
            position.AdjustPosition();
            __instance.text.text +=
                $"\n<color=#ff6700FF>MCI v{MCIPlugin.VersionString}_{MCIPlugin.AfterVersionString}</color>";

            __instance.text.text += "\n" + MCIPlugin.VersionTextString;

            if (MCIPlugin.Enabled)
                 __instance.text.text += MCIPlugin.IfChinese ? "\n <color=#00ff00FF>[本地]</color>" : "\n <color=#00ff00FF>[Active]</color>";
            else
                 __instance.text.text += MCIPlugin.IfChinese ? "\n <color=#ff0000FF>[非本地]</color>" : "\n <color=#ff0000FF>[InActive]</color>";

            if (MCIPlugin.Enabled)
            {
                if (!MCIPlugin.IKnowWhatImDoing)
                {
                    __instance.text.text += MCIPlugin.Persistence ?
                    " <color=#00ff00FF>[✓]</color>" : " <color=#ff0000FF>[X]</color>";
                }
                else
                {
                    __instance.text.text += MCIPlugin.Persistence ?
                                    " <color=#00ff00FF>[<color=#ccaa00FF>✓</color>]</color>" : " <color=#ff0000FF>[<color=#ccaa00FF>X</color>]</color>";
                }
                __instance.text.text += (SubmergedCompatibility.Loaded && GameOptionsManager.Instance.currentNormalGameOptions.MapId == 6) ? " <color=#00ccccFF>[Submerged]</color>" : " ";
            }
            if (UpdateChecker.needsUpdate) __instance.text.text += MCIPlugin.IfChinese ? "\n- <color=#ff0000FF>有更新</color>":"\n- <color=#ff0000FF>UPDATE AVAILABLE</color>";
            __instance.text.text += "\nThis Ver <color=#FFFF00>By</color> <color=#FF0000>Yu</color>";
#if Debug
__instance.text.text += "\n<color=#FFC0CB>Debug</color>";
#endif
#if CANARYPUB
            __instance.text.text += "\n<color=#6A5ACD>Canary_public</color>";
#endif
#if CANARYPRI
            __instance.text.text += "\n<color=#191970>Canary_private</color>";
            __instance.text.text += MCIPlugin.IfChinese
                ? "\n<color=#FF00FF>仅供内测使用 一经传出\n将永久踢出内测资格并纳入\n<color=#DC143C>多模组</color>内测黑名单</color>"
                : "\n<color=#FF00FF>This is for internal testing only.\nIf word gets out,\nyou'll be permanently kicked out of\n internal testing and </color><color=#DC143C>blacklisted from\nmods internal testing.</color>";
#endif

        }
    }
}
