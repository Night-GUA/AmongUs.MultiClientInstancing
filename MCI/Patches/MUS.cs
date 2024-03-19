using HarmonyLib;
using InnerNet;
using System;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MCI.Patches.ClientOptions;

public static class ModUnloaderScreen
{
    public static SpriteRenderer Popup { get; private set; }
    public static TextMeshPro WarnText { get; private set; }
    public static ToggleButtonBehaviour CancelButton { get; private set; }
    public static ToggleButtonBehaviour UnloadButton { get; private set; }

    public static void Init(OptionsMenuBehaviour optionsMenuBehaviour)
    {
        Popup = Object.Instantiate(optionsMenuBehaviour.Background, ClientActionItem.CustomBackground.transform);
        Popup.name = "UnloadModPopup";
        Popup.transform.localPosition = new(0f, 0f, -8f);
        Popup.transform.localScale = new(0.8f, 0.8f, 1f);
        Popup.gameObject.SetActive(false);

        WarnText = Object.Instantiate(optionsMenuBehaviour.DisableMouseMovement.Text, Popup.transform);
        WarnText.name = "Warning";
        WarnText.transform.localPosition = new(0f, 1f, -1f);
        WarnText.transform.localScale = new(2.5f, 2.5f, 1f);
        WarnText.gameObject.SetActive(true);
    }

    public static void Show()
    {
        if (Popup != null)
        {
            Popup.gameObject.SetActive(true);

            if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started)
            {
                WarnText.text = "游戏中无法关";
                UnloadButton.gameObject.SetActive(false);
            }
            else
            {
                WarnText.text = "卸载警告";
                UnloadButton.gameObject.SetActive(true);
            }
        }
    }
    public static void Hide()
    {
        if (Popup != null)
        {
            Popup.gameObject.SetActive(false);
        }
    }
}
