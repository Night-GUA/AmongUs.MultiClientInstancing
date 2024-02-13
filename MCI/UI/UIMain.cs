//From KPD TownOfNext,will on 0.0.8~0.1.0 Okey
// using HarmonyLib;
// using System.Collections.Generic;
// using System.Text;
// using TMPro;
// using TONX.Templates;//WAIT ERROR
// using UnityEngine;
//
// internal class TitleLogoPatch
// {
//     public static GameObject ModStamp;
//     public static GameObject TONX_Background;
//     public static GameObject Ambience;
//     public static GameObject Starfield;
//     public static GameObject LeftPanel;
//     public static GameObject RightPanel;
//     public static GameObject CloseRightButton;
//     public static GameObject Tint;
//     public static GameObject Sizer;
//     public static GameObject AULogo;
//     public static GameObject BottomButtonBounds;
//
//     public static Vector3 RightPanelOp;
//
//     private static void Postfix(MainMenuManager __instance)
//     {
//         GameObject.Find("BackgroundTexture")?.SetActive(!TONX.MainMenuManagerPatch.ShowedBak);
//
//         if (!(ModStamp = GameObject.Find("ModStamp"))) return;
//         ModStamp.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
//
//         TONX_Background = new GameObject("TONX Background");
//         TONX_Background.transform.position = new Vector3(0, 0, 520f);
//         var bgRenderer = TONX_Background.AddComponent<SpriteRenderer>();
//         bgRenderer.sprite = Utils.LoadSprite("TONX.Resources.Images.TONX-BG.jpg", 179f);
//
//         if (!(Ambience = GameObject.Find("Ambience"))) return;
//         if (!(Starfield = Ambience.transform.FindChild("starfield").gameObject)) return;
//         StarGen starGen = Starfield.GetComponent<StarGen>();
//         starGen.SetDirection(new Vector2(0, -2));
//         Starfield.transform.SetParent(TONX_Background.transform);
//         Object.Destroy(Ambience);
//
//         if (!(LeftPanel = GameObject.Find("LeftPanel"))) return;
//         LeftPanel.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
//         static void ResetParent(GameObject obj) => obj.transform.SetParent(LeftPanel.transform.parent);
//         LeftPanel.ForEachChild((Il2CppSystem.Action<GameObject>)ResetParent);
//         LeftPanel.SetActive(false);
//
//         Color shade = new(0f, 0f, 0f, 0f);
//         var standardActiveSprite = __instance.newsButton.activeSprites.GetComponent<SpriteRenderer>().sprite;
//         var minorActiveSprite = __instance.quitButton.activeSprites.GetComponent<SpriteRenderer>().sprite;
//
//         Dictionary<List<PassiveButton>, (Sprite, Color, Color, Color, Color)> mainButtons = new()
//         {
//             {new List<PassiveButton>() {__instance.playButton, __instance.inventoryButton, __instance.shopButton},
//                 (standardActiveSprite, new(1f, 0.524f, 0.549f, 0.8f), shade, Color.white, Color.white) },
//             {new List<PassiveButton>() {__instance.newsButton, __instance.myAccountButton, __instance.settingsButton},
//                 (minorActiveSprite, new(1f, 0.825f, 0.686f, 0.8f), shade, Color.white, Color.white) },
//             {new List<PassiveButton>() {__instance.creditsButton, __instance.quitButton},
//                 (minorActiveSprite, new(0.526f, 1f, 0.792f, 0.8f), shade, Color.white, Color.white) },
//         };
//
//         void FormatButtonColor(PassiveButton button, Sprite borderType, Color inActiveColor, Color activeColor, Color inActiveTextColor, Color activeTextColor)
//         {
//             button.activeSprites.transform.FindChild("Shine")?.gameObject?.SetActive(false);
//             button.inactiveSprites.transform.FindChild("Shine")?.gameObject?.SetActive(false);
//             var activeRenderer = button.activeSprites.GetComponent<SpriteRenderer>();
//             var inActiveRenderer = button.inactiveSprites.GetComponent<SpriteRenderer>();
//             activeRenderer.sprite = minorActiveSprite;
//             inActiveRenderer.sprite = minorActiveSprite;
//             activeRenderer.color = activeColor.a == 0f ? new Color(inActiveColor.r, inActiveColor.g, inActiveColor.b, 1f) : activeColor;
//             inActiveRenderer.color = inActiveColor;
//             button.activeTextColor = activeTextColor;
//             button.inactiveTextColor = inActiveTextColor;
//         }
//
//         foreach (var kvp in mainButtons)
//             kvp.Key.Do(button => FormatButtonColor(button, kvp.Value.Item1, kvp.Value.Item2, kvp.Value.Item3, kvp.Value.Item4, kvp.Value.Item5));
//
//         GameObject.Find("Divider")?.SetActive(false);
//
//         if (!(RightPanel = GameObject.Find("RightPanel"))) return;
//         var rpap = RightPanel.GetComponent<AspectPosition>();
//         if (rpap) Object.Destroy(rpap);
//         RightPanelOp = RightPanel.transform.localPosition;
//         RightPanel.transform.localPosition = RightPanelOp + new Vector3(10f, 0f, 0f);
//         RightPanel.GetComponent<SpriteRenderer>().color = new(1f, 0.78f, 0.9f, 1f);
//
//         CloseRightButton = new GameObject("CloseRightPanelButton");
//         CloseRightButton.transform.SetParent(RightPanel.transform);
//         CloseRightButton.transform.localPosition = new Vector3(-4.78f, 1.3f, 1f);
//         CloseRightButton.transform.localScale = new(1f, 1f, 1f);
//         CloseRightButton.AddComponent<BoxCollider2D>().size = new(0.6f, 1.5f);
//         var closeRightSpriteRenderer = CloseRightButton.AddComponent<SpriteRenderer>();
//         closeRightSpriteRenderer.sprite = Utils.LoadSprite("TONX.Resources.Images.RightPanelCloseButton.png", 100f);
//         closeRightSpriteRenderer.color = new(1f, 0.78f, 0.9f, 1f);
//         var closeRightPassiveButton = CloseRightButton.AddComponent<PassiveButton>();
//         closeRightPassiveButton.OnClick = new();
//         closeRightPassiveButton.OnClick.AddListener((System.Action)MainMenuManagerPatch.HideRightPanel);
//         closeRightPassiveButton.OnMouseOut = new();
//         closeRightPassiveButton.OnMouseOut.AddListener((System.Action)(() => closeRightSpriteRenderer.color = new(1f, 0.78f, 0.9f, 1f)));
//         closeRightPassiveButton.OnMouseOver = new();
//         closeRightPassiveButton.OnMouseOver.AddListener((System.Action)(() => closeRightSpriteRenderer.color = new(1f, 0.68f, 0.99f, 1f)));
//
//         Tint = __instance.screenTint.gameObject;
//         var ttap = Tint.GetComponent<AspectPosition>();
//         if (ttap) Object.Destroy(ttap);
//         Tint.transform.SetParent(RightPanel.transform);
//         Tint.transform.localPosition = new Vector3(-0.0824f, 0.0513f, Tint.transform.localPosition.z);
//         Tint.transform.localScale = new Vector3(1f, 1f, 1f);
//
//         if (!DebugModeManager.AmDebugger)
//         {
//             __instance.howToPlayButton.gameObject.SetActive(false);
//             __instance.howToPlayButton.transform.parent.Find("FreePlayButton").gameObject.SetActive(false);
//         }
//
//         var creditsScreen = __instance.creditsScreen;
//         if (creditsScreen)
//         {
//             var csto = creditsScreen.GetComponent<TransitionOpen>();
//             if (csto) Object.Destroy(csto);
//             var closeButton = creditsScreen.transform.FindChild("CloseButton");
//             closeButton?.gameObject.SetActive(false);
//         }
//
//         if (!(Sizer = GameObject.Find("Sizer"))) return;
//         if (!(AULogo = GameObject.Find("LOGO-AU"))) return;
//         Sizer.transform.localPosition += new Vector3(0f, 0.12f, 0f);
//         AULogo.transform.localScale = new Vector3(0.66f, 0.67f, 1f);
//         AULogo.transform.position += new Vector3(0f, 0.1f, 0f);
//         var logoRenderer = AULogo.GetComponent<SpriteRenderer>();
//         logoRenderer.sprite = Utils.LoadSprite("TONX.Resources.Images.TONX-Logo.png");
//
//         if (!(BottomButtonBounds = GameObject.Find("BottomButtonBounds"))) return;
//         BottomButtonBounds.transform.localPosition -= new Vector3(0f, 0.1f, 0f);
//     }
// }