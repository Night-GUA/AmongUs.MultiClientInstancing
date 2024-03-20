using HarmonyLib;
using UnityEngine;

namespace MCI.Patches
{
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
    public sealed class LocalOnly
    {
        public static void Postfix()
        {
            var inf = new GameObject("Info");
            inf.transform.position = new Vector3(0, -1.75f, 0);
            var tmp = inf.AddComponent<TMPro.TextMeshPro>();
            tmp.alignment = TMPro.TextAlignmentOptions.Center;
            tmp.horizontalAlignment = TMPro.HorizontalAlignmentOptions.Center;
            if(MCIPlugin.IfChinese)
                tmp.text = $"MCI-Yu只能在本地模式运行\nMCI开发者(0.0.1~0.0.6)：MyDragonBreath, whichTwix\nMCI-Yu开发者(0.0.7~{MCIPlugin.VersionString}_{MCIPlugin.AfterVersionString})：Yu";
            else
                tmp.text = $"MCI-Yu only supports localhosted lobbies.\nMCI Developer(0.0.1~0.0.6): MyDragonBreath, whichTwix\n MCI-Yu Developer(0.0.7~{MCIPlugin.VersionString}_{MCIPlugin.AfterVersionString}) : Yu";
            tmp.color = Color.red;
            tmp.fontSize = 2f;

            var pos = inf.AddComponent<AspectPosition>();
            pos.Alignment = AspectPosition.EdgeAlignments.RightBottom;
            pos.DistanceFromEdge = new Vector3(3.2f, 1f, 200);
            pos.AdjustPosition();
        }
    }

    [HarmonyPatch(typeof(LobbyBehaviour), nameof(LobbyBehaviour.Start))]
    public sealed class LobbyCheck
    {
        public static void Postfix()
        {
            MCIPlugin.Enabled = AmongUsClient.Instance.NetworkMode == NetworkModes.LocalGame;
        }
    }
}
