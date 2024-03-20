using HarmonyLib;
using UnityEngine;
using MCI.UI;
using Il2CppSystem.Collections.Generic;
using System;
using AmongUs.GameOptions;

namespace MCI.Patches
{
    [HarmonyPatch(typeof(KeyboardJoystick), nameof(KeyboardJoystick.Update))]
    public sealed class Keyboard_Joystick
    {
        private static int controllingFigure;

        public static void Postfix()
        {
#if RELEASE
            if (!MCIPlugin.Enabled) return;
#endif
            if (Input.GetKeyDown(KeyCode.F5))
            {
                controllingFigure = PlayerControl.LocalPlayer.PlayerId;
                if (PlayerControl.AllPlayerControls.Count == 15 && !Input.GetKeyDown(KeyCode.F6)) return; //press f6 and f5 to bypass limit
                if(controllingFigure != 0)
                    controllingFigure = 0;
                Utils.CleanUpLoad();
                Utils.CreatePlayerInstance(MCIPlugin.RobotName);
            }

            if (Input.GetKeyDown(KeyCode.F9))
            {
                controllingFigure++;
                controllingFigure = Mathf.Clamp(controllingFigure, 0, PlayerControl.AllPlayerControls.Count - 1);
                InstanceControl.SwitchTo((byte)controllingFigure);
            }

            if (Input.GetKeyDown(KeyCode.F10))
            {
                controllingFigure--;
                controllingFigure = Mathf.Clamp(controllingFigure, 0, PlayerControl.AllPlayerControls.Count - 1);
                InstanceControl.SwitchTo((byte)controllingFigure);
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F6))
            {
                MCIPlugin.IKnowWhatImDoing = !MCIPlugin.IKnowWhatImDoing;
                Utils.UpdateNames(MCIPlugin.RobotName);
            }
            else if (Input.GetKeyDown(KeyCode.F6))
            {
                MCIPlugin.Persistence = !MCIPlugin.Persistence;
            }

            if (Input.GetKeyDown(KeyCode.F11))
            {
                if(controllingFigure != 0)
                    controllingFigure = 0;
                Utils.RemoveAllPlayers();
            }
        }
    }
}
