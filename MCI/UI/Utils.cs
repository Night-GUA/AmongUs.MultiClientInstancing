//From KPD TownOfNext,will on 0.0.8~0.1.0 Okey
// using AmongUs.Data;
// using AmongUs.GameOptions;
// using Hazel;
// using Il2CppInterop.Runtime.InteropTypes;
// using InnerNet;
// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Diagnostics;
// using System.IO;
// using System.Linq;
// using System.Reflection;
// using System.Text;
// using System.Text.RegularExpressions;
// using TONX.Modules;
// using TONX.Roles.AddOns.Crewmate;
// using TONX.Roles.AddOns.Impostor;
// using TONX.Roles.Core;
// using TONX.Roles.Core.Interfaces;
// using TONX.Roles.Crewmate;
// using TONX.Roles.Impostor;
// using TONX.Roles.Neutral;
// using UnityEngine;
// using static TONX.Translator;
//
// namespace TONX;
//
// public static class Utils
// {
//     public static Dictionary<string, Sprite> CachedSprites = new();
//
//     public static Sprite LoadSprite(string path, float pixelsPerUnit = 1f)
//     {
//         try
//         {
//             if (CachedSprites.TryGetValue(path + pixelsPerUnit, out var sprite)) return sprite;
//             Texture2D texture = LoadTextureFromResources(path);
//             sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f),
//                 pixelsPerUnit);
//             sprite.hideFlags |= HideFlags.HideAndDontSave | HideFlags.DontSaveInEditor;
//             return CachedSprites[path + pixelsPerUnit] = sprite;
//         }
//         catch
//         {
//             Debug.LogError($"*From KPD LoadImage*读入Texture失败：{path}");
//         }
//
//         return null;
//     }
// }
