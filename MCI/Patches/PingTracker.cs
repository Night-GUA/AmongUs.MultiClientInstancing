﻿using HarmonyLib;
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
                "\n<color=#ff6700FF>MCI v" + MCIPlugin.VersionString + "</color>";
            __instance.text.text += MCIPlugin.Enabled ? " <color=#00ff00FF>[Active]</color>" : " <color=#ff0000FF>[InActive]</color>";

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
            __instance.text.text += "\nThis Ver By Yu";
#if Debug
__instance.text.text += "<color=#FFC0CB>Debug</color>";
#endif

        }
    }
}
