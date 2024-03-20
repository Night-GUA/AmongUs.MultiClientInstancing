using HarmonyLib;
using System.Collections.Generic;
using System.Text;
using TMPro;
using MCI;
using UnityEngine;
using AmongUs.Data;
using AmongUs.GameOptions;
using Hazel;
using Il2CppInterop.Runtime.InteropTypes;
using InnerNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using MCI;
using MCI.UI;
using MCI.Patches;
using MCI.Patches.ClientOptions;
using UnityEngine;

namespace MCI.UI;

[HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start)), HarmonyPriority(Priority.First)]
internal class TitleLogoPatch
{
    public static GameObject ModStamp;
    public static GameObject MCI_Background;
    public static GameObject Ambience;
    public static GameObject Starfield;
    public static GameObject Sizer;
    public static GameObject AULogo;
    public static GameObject BottomButtonBounds;

    public static void showPopup(string text){
        var popup = GameObject.Instantiate(DiscordManager.Instance.discordPopup, Camera.main!.transform);
        
        var background = popup.transform.Find("Background").GetComponent<SpriteRenderer>();
        var size = background.size;
        size.x *= 2.5f;
        background.size = size;

        popup.TextAreaTMP.fontSizeMin = 2;
        popup.Show(text);
    }
    
    private static void Postfix(MainMenuManager __instance)
    {
        if (!(ModStamp = GameObject.Find("ModStamp"))) return;
        ModStamp.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        
        MCI_Background = new GameObject("MCI Background");
        MCI_Background.transform.position = new Vector3(2.1f, 0.2f, 520f);
        var bgRenderer = MCI_Background.AddComponent<SpriteRenderer>();
        bgRenderer.sprite = LoadSprite("MCI.Resources.MCI-Bg.png", 179f);
        
        if (!(Ambience = GameObject.Find("Ambience"))) return;
        if (!(Starfield = Ambience.transform.FindChild("starfield").gameObject)) return;
        StarGen starGen = Starfield.GetComponent<StarGen>();
        starGen.SetDirection(new Vector2(0, -2));
        Starfield.transform.SetParent(MCI_Background.transform);
        GameObject.Destroy(Ambience);
        
        if (!(Sizer = GameObject.Find("Sizer"))) return;
        if (!(AULogo = GameObject.Find("LOGO-AU"))) return;
        Sizer.transform.localPosition += new Vector3(0f, 0.12f, 0f);
        AULogo.transform.localScale = new Vector3(0.66f, 0.67f, 1f);
        AULogo.transform.position -= new Vector3(0f, 0.1f, 0f);
        var logoRenderer = AULogo.GetComponent<SpriteRenderer>();
        logoRenderer.sprite = LoadSprite("MCI.Resources.Yu-Logo.png",120f);

        if (!(BottomButtonBounds = GameObject.Find("BottomButtonBounds"))) return;
        BottomButtonBounds.transform.localPosition -= new Vector3(0f, 0.1f, 0f);
        
        showPopup(MCIPlugin.IfChinese ? $"欢迎使用MCI-Yu {MCIPlugin.VersionString}_{MCIPlugin.AfterVersionString}！" : $"Welcome to use MCI-Yu {MCIPlugin.VersionString}_{MCIPlugin.AfterVersionString}!");
    }
    
    public static Dictionary<string, Sprite> CachedSprites = new();
    public static Sprite LoadSprite(string path, float pixelsPerUnit = 1f)
    {
        try
        {
            if (CachedSprites.TryGetValue(path + pixelsPerUnit, out var sprite)) return sprite;
            Texture2D texture = LoadTextureFromResources(path);
            sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
            sprite.hideFlags |= HideFlags.HideAndDontSave | HideFlags.DontSaveInEditor;
            return CachedSprites[path + pixelsPerUnit] = sprite;
        }
        catch
        {
            
        }
        return null;
    }
    
    public static Texture2D LoadTextureFromResources(string path)
    {
        try
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            var texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            using MemoryStream ms = new();
            stream.CopyTo(ms);
            ImageConversion.LoadImage(texture, ms.ToArray(), false);
            return texture;
        }
        catch
        {
        }
        return null;
    }
    
    [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
    public static class VersionShower_Start
    {
        public static void Postfix(VersionShower __instance)
        {
            __instance.text.text = $"MCI-Yu v{MCIPlugin.VersionString} (v{Application.version})";
        }
    }
}