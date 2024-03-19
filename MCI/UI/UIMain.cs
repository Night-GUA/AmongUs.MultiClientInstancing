using HarmonyLib;
using AmongUs.Data;
using UnityEngine;
using System;
using System.Security.Cryptography;

namespace MCI;


[HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
public static class VersionShower_Start
{
    public static void Postfix(VersionShower __instance)
    {
        __instance.text.text = $"MCI-Yu v{MCIPlugin.VersionString} (v{Application.version})";
    }
}