using System;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System.Linq;
using Mono.CompilerServices.SymbolWriter;
using Il2CppInterop.Runtime;

namespace MCI.Button;
[HarmonyPatch(typeof(MainMenuManager))]
//参考COG

public class MainButton
{
    [HarmonyPatch(nameof(MainMenuManager.Start))]
    [HarmonyPrefix]
    static void LoadButtons(MainMenuManager __instance)
    {
        Buttons.Clear();
        var template = __instance.creditsButton;

        if (!template) return;

        CreateButton(__instance, template, GameObject.Find("RightPanel")?.transform, new(0.2f, 0.38f), "中国大陆玩家点我查看使用说明", () => { Application.OpenURL("https://gitee.com/xigua_ya/AmongUs.MultiClientInstancing/blob/main/Resources/ChineseHTU.md"); }, Color.blue);
        CreateButton(__instance, template, GameObject.Find("RightPanel")?.transform, new(0.4f, 0.38f), "Choose Me to see how to use", () => { Application.OpenURL("https://github.com/Night-GUA/AmongUs.MultiClientInstancing/blob/main/Resources/EnglishHTU.md"); }, Color.cyan);
        if(MCIPlugin.IfChinese) CreateButton(__instance, template, GameObject.Find("RightPanel")?.transform, new(0.6f, 0.38f), "Yu-Bilibili", () => { Application.OpenURL("https://space.bilibili.com/1638639993"); }, Color.magenta);
    }

    private static readonly List<PassiveButton> Buttons = new();
    /// <summary>
    /// 在主界面创建一个按钮
    /// </summary>
    /// <param name="__instance">MainMenuManager 的实例</param>
    /// <param name="template">按钮模板</param>
    /// <param name="parent">父游戏物体</param>
    /// <param name="anchorPoint">与父游戏物体的相对位置</param>
    /// <param name="text">按钮文本</param>
    /// <param name="action">点击按钮的动作</param>
    /// <returns>返回这个按钮</returns>
    static void CreateButton(MainMenuManager __instance, PassiveButton template, Transform? parent, Vector2 anchorPoint, string text, Action action, Color color)
    {
        if (!parent) return;

        var button = UnityEngine.Object.Instantiate(template, parent);
        button.GetComponent<AspectPosition>().anchorPoint = anchorPoint;
        SpriteRenderer buttonSprite = button.transform.FindChild("Inactive").GetComponent<SpriteRenderer>();
        buttonSprite.color = color;
        __instance.StartCoroutine(Effects.Lerp(0.5f, new Action<float>((p) => {
            button.GetComponentInChildren<TMPro.TMP_Text>().SetText(text);
        })));

        button.OnClick = new();
        button.OnClick.AddListener(action);

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