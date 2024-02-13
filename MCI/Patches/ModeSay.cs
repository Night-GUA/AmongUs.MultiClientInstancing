using HarmonyLib;
using UnityEngine;

namespace MCI.Patches
{
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
    public sealed class ModeSay
    {
        public static void Postfix()
        {
            var inf = new GameObject("Info");
            inf.transform.position = new Vector3(0, -1.75f, 0);
            var tmp = inf.AddComponent<TMPro.TextMeshPro>();
            tmp.alignment = TMPro.TextAlignmentOptions.Center;
            tmp.horizontalAlignment = TMPro.HorizontalAlignmentOptions.Center;
#if Debug
tmp.text = MCIPlugin.IfChinese ? "<color=#0000FF>你现处于</color><color#FF0000>Debug</color><color=#0000FF>模式</color>" : "<color=#0000FF>You now at</color><color#FF0000>Debug</color><color=#0000FF> Mode</color>";
#endif
#if CANARYPRI
            tmp.text = MCIPlugin.IfChinese ? "<color=#0000FF>你现处于</color><color#FF0000>Canary Private</color><color=#0000FF>模式\n仅测试不分享</color>" : "<color=#0000FF>You now at</color><color#FF0000>Canary Private</color><color=#0000FF> Mode\nThis Mode Only Test,Can't Share</color>";
#endif
#if canarypub
tmp.text = MCIPlugin.IfChinese ? "<color=#0000FF>你现处于</color><color#FF0000>Canary Public</color><color=#0000FF>模式\n仅测试</color>" : "<color=#0000FF>You now at</color><color#FF0000>Canary Public</color><color=#0000FF> Mode\nThis Mode Only Test</color>";
#endif
            //tmp.color = Color.red;
            tmp.fontSize = 2f;

            var pos = inf.AddComponent<AspectPosition>();
            pos.Alignment = AspectPosition.EdgeAlignments.RightBottom;
            pos.DistanceFromEdge = new Vector3(3.2f, -0.5f, 200);
            pos.AdjustPosition();
        }
    }
}
