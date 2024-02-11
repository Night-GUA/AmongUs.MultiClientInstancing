using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;
using AmongUs.Data;
using Assets.InnerNet;
using System.Linq;
using System.Reflection;
using System.Linq;
using Mono.CompilerServices.SymbolWriter;
using Il2CppInterop.Runtime;

namespace MCI.UI
{
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
    public class MainMenuPatch
    {
        private static AnnouncementPopUp popUp;

        private static void Prefix(MainMenuManager __instance)
        {
            var template = GameObject.Find("SettingsButton");
            var template2 = GameObject.Find("AcountButton");
            var template3 = GameObject.Find("NewsButton");
            if (template == null || template2 == null || template3 == null) return;
            template.transform.localScale = new Vector3(0.42f, 0.84f, 1f);
            template.GetComponent<AspectPosition>().anchorPoint = new Vector2(0.378f, 0.5f);
            template.transform.FindChild("FontPlacer").transform.localScale = new Vector3(1.8f, 0.9f, 0.9f);
            template.transform.FindChild("FontPlacer").transform.localPosition = new Vector3(-1.1f, 0f, 0f);

            template2.transform.localScale = new Vector3(0.42f, 0.84f, 1f);
            //template2.GetComponent<AspectPosition>().anchorPoint = new Vector2(0.378f, 0.5f);
            template2.transform.FindChild("FontPlacer").transform.localScale = new Vector3(1.8f, 0.9f, 0.9f);
            template2.transform.FindChild("FontPlacer").transform.localPosition = new Vector3(-1.1f, 0f, 0f);

            template3.transform.localScale = new Vector3(0.42f, 0.84f, 1f);
            //template3.GetComponent<AspectPosition>().anchorPoint = new Vector2(0.378f, 0.5f);
            template3.transform.FindChild("FontPlacer").transform.localScale = new Vector3(1.8f, 0.9f, 0.9f);
            template3.transform.FindChild("FontPlacer").transform.localPosition = new Vector3(-1.1f, 0f, 0f);
            //qq频道
            var buttonDiscord = UnityEngine.Object.Instantiate(template, template.transform.parent);
            buttonDiscord.transform.localScale = new Vector3(0.42f, 0.84f, 1f);
            //buttonDiscord.GetComponent<AspectPosition>().anchorPoint = new Vector2(0.542f, 0.5f);

            var textDiscord = buttonDiscord.transform.GetComponentInChildren<TMPro.TMP_Text>();
            __instance.StartCoroutine(Effects.Lerp(0.5f,
                new System.Action<float>((p) => { textDiscord.SetText("QQ频道"); })));
            PassiveButton passiveButtonDiscord = buttonDiscord.GetComponent<PassiveButton>();

            passiveButtonDiscord.OnClick = new Button.ButtonClickedEvent();
            passiveButtonDiscord.OnClick.AddListener((System.Action)(() =>
                Application.OpenURL(
                    "http://qm.qq.com/cgi-bin/qm/qr?_wv=1027&k=1YPTXe2Sh93pAUXv1mwv4unI6J_G1FYK&authKey=%2BPzdgfi%2FpbaxyPTVU1Lx8xU69Zo1X4%2FCih0lTozAbZ0%2FsCiO%2FGe8sQ97p6jxEFlV&noverify=0&group_code=647668527")));
            //DEBUG
            var buttonGITHUB = Object.Instantiate(template3, template3.transform.parent);
            buttonGITHUB.transform.localScale = new Vector3(0.42f, 0.84f, 1f);
            //buttonGITHUB.GetComponent<AspectPosition>().anchorPoint = new Vector2(0.542f, 0.5f);

            var textGITHUB = buttonGITHUB.transform.GetComponentInChildren<TMPro.TMP_Text>();
            __instance.StartCoroutine(Effects.Lerp(0.5f,
                new System.Action<float>((p) => { textGITHUB.SetText("QQ频道"); })));
            PassiveButton passiveButtonGITHUB = buttonGITHUB.GetComponent<PassiveButton>();

            passiveButtonGITHUB.OnClick = new Button.ButtonClickedEvent();
            passiveButtonGITHUB.OnClick.AddListener((System.Action)(() =>
                Application.OpenURL(
                    "http://qm.qq.com/cgi-bin/qm/qr?_wv=1027&k=1YPTXe2Sh93pAUXv1mwv4unI6J_G1FYK&authKey=%2BPzdgfi%2FpbaxyPTVU1Lx8xU69Zo1X4%2FCih0lTozAbZ0%2FsCiO%2FGe8sQ97p6jxEFlV&noverify=0&group_code=647668527")));

            //公告
            if (template == null) return;
            var creditsButton = Object.Instantiate(template, template.transform.parent);

            creditsButton.transform.localScale = new Vector3(0.42f, 0.84f, 1f);
            //creditsButton.GetComponent<AspectPosition>().anchorPoint = new Vector2(0.462f, 0.5f);

            var textCreditsButton = creditsButton.transform.GetComponentInChildren<TMPro.TMP_Text>();
            __instance.StartCoroutine(Effects.Lerp(0.5f,
                new System.Action<float>((p) => { textCreditsButton.SetText("TORE公告"); })));
            PassiveButton passiveCreditsButton = creditsButton.GetComponent<PassiveButton>();

            passiveCreditsButton.OnClick = new Button.ButtonClickedEvent();

            passiveCreditsButton.OnClick.AddListener((System.Action)delegate
            {
                // do stuff
                if (popUp != null) Object.Destroy(popUp);
                var popUpTemplate = Object.FindObjectOfType<AnnouncementPopUp>(true);
                if (popUpTemplate == null)
                {
                    return;
                }

                popUp = Object.Instantiate(popUpTemplate);

                popUp.gameObject.SetActive(true);
                string creditsString = @$"<align=""center""><b>Team:</b>
Mallöris    K3ndo    Bavari    Gendelo

<b>团队开发者:</b>
Eisbison (GOAT)    Thunderstorm584    EndOfFile

<b>开发者:</b>
EnoPM    twix    NesTT

<b>Github贡献者:</b>
Alex2911    amsyarasyiq    MaximeGillot
Psynomit    probablyadnf    JustASysAdmin

<b>[https://discord.gg/77RkMJHWsM]Discord[] 贡献者:</b>
Draco Cordraconis    Streamblox (formerly)
感谢所有dicord贡献者的帮助!

感谢miniduikboot & GD对模组的帮助

";
                creditsString += $@"<size=60%> <b>其他贡献 & 资源:</b>
OxygenFilter - 从版本v2.3.0 到v2.6.1, 对TOR有很大帮助
Reactor - 使得我们的模组更好
BepInEx - 使得模组能够运行
Essentials - Custom game options by DorCoMaNdO:
Before v1.6: We used the default Essentials release
v1.6-v1.8: We slightly changed the default Essentials.
v2.0.0 and later: As we were not using Reactor anymore, we are using our own implementation, inspired by the one from DorCoMaNdO
Jackal and Sidekick - Original idea for the Jackal and Sidekick came from Dhalucard
Among-Us-Love-Couple-Mod - Idea for the Lovers modifier comes from Woodi-dev
Jester - Idea for the Jester role came from Maartii
ExtraRolesAmongUs - Idea for the Engineer and Medic role came from NotHunter101. Also some code snippets from their implementation were used.
Among-Us-Sheriff-Mod - Idea for the Sheriff role came from Woodi-dev
TooManyRolesMods - Idea for the Detective and Time Master roles comes from Hardel-DW. Also some code snippets from their implementation were used.
TownOfUs - Idea for the Swapper, Shifter, Arsonist and a similar Mayor role came from Slushiegoose
Ottomated - Idea for the Morphling, Snitch and Camouflager role came from Ottomated
Crowded-Mod - Our implementation for 10+ player lobbies was inspired by the one from the Crowded Mod Team
Goose-Goose-Duck - Idea for the Vulture role came from Slushiegoose
TheEpicRoles - Idea for the first kill shield (partly) and the tabbed option menu (fully + some code), by LaicosVK DasMonschta Nova
TheOtherRolesGMIA - 许多代码来源于此，送葬者等职业也来源于此(感谢imp11的帮助！)[https://github.com/dabao40/TheOtherRolesGMIA]TheOtherRolesGMIA[] 下载传送门
ugackMiner53 - Idea and core code for the Prop Hunt game mode</size>";
                creditsString += "</align>";

                Assets.InnerNet.Announcement creditsAnnouncement = new()
                {
                    Id = "torCredits",
                    Language = 0,
                    Number = 500,
                    Title = "\nThe Other Roles\n贡献 & 资源",
                    ShortTitle = "TOR Credits",
                    SubTitle = "",
                    PinState = false,
                    Date = "02.01.2024",
                    Text = creditsString,
                };
                __instance.StartCoroutine(Effects.Lerp(0.1f, new Action<float>((p) =>
                {
                    if (p == 1)
                    {
                        var backup = DataManager.Player.Announcements.allAnnouncements;
                        DataManager.Player.Announcements.allAnnouncements = new();
                        popUp.Init(false);
                        DataManager.Player.Announcements.SetAnnouncements(
                            new Announcement[] { creditsAnnouncement });
                        popUp.CreateAnnouncementList();
                        popUp.UpdateAnnouncementText(creditsAnnouncement.Number);
                        popUp.visibleAnnouncements._items[0].PassiveButton.OnClick.RemoveAllListeners();
                        DataManager.Player.Announcements.allAnnouncements = backup;
                    }
                })));
            });

        }
    }
}
