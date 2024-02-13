using System;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System.Linq;
using Mono.CompilerServices.SymbolWriter;
using Il2CppInterop.Runtime;
using UnityEngine.UI;
using static UnityEngine.UI.Button;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;
using AmongUs.Data;
using Assets.InnerNet;
using Version = Steamworks.Version;

namespace MCI.UI;
[HarmonyPatch(typeof(MainMenuManager))]
//一天天的净给我惹麻烦QAQ
public class MainButton
{
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
    [HarmonyPrefix]
    static void LoadButtons(MainMenuManager __instance)
    {
        Buttons.Clear();
        //我tm生气了
        //只能用设置和退出按钮 不能用其他的 md那怎么办啊

        UrlButton(__instance, "CreditsButton", new Vector2(0.542f, 0.5f),new Vector2(0.625f, 0.5f),new Vector3(1.8f, 0.9f, 0.9f),new Vector3(0.42f, 0.84f, 0.84f),new Vector3(0.42f, 0.84f, 0.84f),new Vector3(-1.1f, 0f, 0f),"使用说明","https://gitee.com/xigua_ya/AmongUs.MultiClientInstancing/blob/main/MCI/Resources/ChineseHTU.md");
        UrlButton(__instance, "ExitGameButton", new Vector2(0.462f, 0.5f),new Vector2(0.378f, 0.5f),new Vector3(1.8f, 0.9f, 0.9f),new Vector3(0.42f, 0.84f, 0.84f),new Vector3(0.42f, 0.84f, 0.84f),new Vector3(-1.1f, 0f, 0f),"how to use","https://github.com/Night-GUA/AmongUs.MultiClientInstancing/blob/main/MCI/Resources/EnglishHTU.md");
    }

    private static readonly List<GameObject> Buttons = new();
    /// <summary>
    /// 在主界面创建一个按钮
    /// </summary>
    /// <param name="__instance">MainMenuManager 的实例</param>
    /// <param name="parent">父游戏物体</param>
    /// <param name="anchorPoint">与父游戏物体的相对位置</param>
    /// <param name="fatherPoint">父游戏物体的相对位置</param>
    /// <param name="textPoint">字体相对对象</param>
    /// <param name="resizing">按钮缩放</param>
    /// <param name="fatherresizing">父按钮缩放</param>
    /// <param name="textresizing">文字缩放</param>
    /// <param name="text">按钮文本</param>
    /// <param name="url">点击按钮打开链接</param>
    /// <returns>返回这个按钮</returns>
    private static void UrlButton(MainMenuManager __instance, string parent, Vector2 anchorPoint,Vector2 fatherPoint,Vector3 textPoint,Vector3 resizing,Vector3 fatherresizing,Vector3 textresizing, string text, string url)
    {
        var template = GameObject.Find(parent);
        
        if (template == null) return;
        
        var button = Object.Instantiate(template, template.transform.parent);
        
        if (button == null) return;
        
        template.transform.localScale = fatherresizing;
        //Debug.LogError($"*From Yu*,buttonText is {template.transform.localScale}.");
        template.GetComponent<AspectPosition>().anchorPoint = fatherPoint;
        //Debug.LogError($"*From Yu*,buttonText is {template.GetComponent<AspectPosition>().anchorPoint}.");
        template.transform.FindChild("FontPlacer").transform.localScale = textresizing;
        template.transform.FindChild("FontPlacer").transform.localPosition = textPoint;
        button.transform.localScale = resizing;
        button.GetComponent<AspectPosition>().anchorPoint = anchorPoint;

        var buttonText = button.transform.GetComponentInChildren<TMPro.TMP_Text>();
        //Debug.LogError($"*From Yu*,buttonText is {buttonText}.");
        __instance.StartCoroutine(Effects.Lerp(0.5f,
            new System.Action<float>((p) => { buttonText.SetText(text); })));
        PassiveButton passiveButton = button.GetComponent<PassiveButton>();

        passiveButton.OnClick = new Button.ButtonClickedEvent();
        passiveButton.OnClick.AddListener((System.Action)(() =>
            Application.OpenURL(url)));
        
        Buttons.Add(button);
    }

    [HarmonyPatch(nameof(MainMenuManager.OpenAccountMenu))]
    [HarmonyPatch(nameof(MainMenuManager.OpenCredits))]
    [HarmonyPatch(nameof(MainMenuManager.OpenGameModeMenu))]
    [HarmonyPostfix]
    static void Hide()
    {
        foreach (var btn in Buttons) btn.gameObject.SetActive(false);
    }
    
    [HarmonyPatch(nameof(MainMenuManager.ResetScreen))]
    [HarmonyPostfix]
    static void Show()
    {
        foreach (var btn in Buttons)
        {
            if (btn == null || btn.gameObject == null) continue;
            btn.gameObject.SetActive(true);
        }
    }
}