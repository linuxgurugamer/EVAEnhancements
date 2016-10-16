using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.UI.Screens;

namespace EVAEnhancementsContinued
{
    internal class SettingsWindow
    {
        internal ApplicationLauncherButton launcherButton = null;
        internal IButton blizzyButton = null;

        internal bool showWindow;
        internal static bool windowRectDefined = false;
        internal static Rect windowRect;

        internal Vector2 scrollPos = new Vector2(0f, 0f);
        internal int windowId;
        internal Settings settings;
        internal ModStyle modStyle;

        private bool settingPitchDown = false;
        private bool settingPitchUp = false;
        private bool settingRollLeft = false;
        private bool settingRollRight = false;




        internal SettingsWindow()
        {
            settings = SettingsWrapper.Instance.gameSettings;
            modStyle = SettingsWrapper.Instance.modStyle;
            showWindow = false;
            if (!windowRectDefined)
                windowRect = new Rect((Screen.width - 300) / 1, (Screen.height - 300) / 2, 250, 300);
            windowRectDefined = true;
            windowId = GUIUtility.GetControlID(FocusType.Passive);
        }

        internal void draw()
        {
            if (showWindow)
            {
                GUI.skin = modStyle.skin;
                windowRect = GUILayout.Window(windowId, windowRect, drawWindow, "");
            }
        }

        internal void drawWindow(int id)
        {


            GUI.skin = modStyle.skin;
            GUILayout.BeginVertical();
            GUILayout.Label("EVA Enhancements - Settings", modStyle.guiStyles["titleLabel"]);
            GUILayout.EndVertical();
            
            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            GUILayout.Label("Default Jetpack Power: " + settings.defaultJetPackPower.ToString("P0"));
            float newJetPackPower = GUILayout.HorizontalSlider(settings.defaultJetPackPower, 0f, 1f);
            if (newJetPackPower != settings.defaultJetPackPower)
            {
                settings.defaultJetPackPower = newJetPackPower;
                settings.Save();
                
            }
            GUILayout.Space(10f);

            GUILayout.Label("Default Precision Jetpack Power: " + settings.defaultPrecisionModePower.ToString("P0"));
            float newPrecisionJetPackPower = GUILayout.HorizontalSlider(settings.defaultPrecisionModePower, 0f, 1f);
            if (newPrecisionJetPackPower != settings.defaultPrecisionModePower)
            {
                settings.defaultPrecisionModePower = newPrecisionJetPackPower;
                settings.Save();

            }
            GUILayout.Space(10f);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Pitch Down",GUILayout.ExpandWidth(true));

            if (settingPitchDown)
            {
                GUILayout.Label("<Press any key>");
                if (Event.current.isKey)
                {
                    settings.pitchDown = Event.current.keyCode;
                    settings.Save();
                    settingPitchDown = false;
                }
            }
            else
            {
                if (GUILayout.Button(new GUIContent(settings.pitchDown.ToString()),GUILayout.Width(125)))
                {
                    settingPitchDown = true;
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Pitch Up",GUILayout.ExpandWidth(true));

            if (settingPitchUp)
            {
                GUILayout.Label("<Press any key>");
                if (Event.current.isKey)
                {
                    settings.pitchUp = Event.current.keyCode;
                    settings.Save();
                    settingPitchUp = false;
                }
            }
            else
            {
                if (GUILayout.Button(new GUIContent(settings.pitchUp.ToString()),GUILayout.Width(125)))
                {
                    settingPitchUp = true;
                }
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Roll Left",GUILayout.ExpandWidth(true));

            if (settingRollLeft)
            {
                GUILayout.Label("<Press any key>");
                if (Event.current.isKey)
                {
                    settings.rollLeft = Event.current.keyCode;
                    settings.Save();
                    settingRollLeft = false;
                }
            }
            else
            {
                if (GUILayout.Button(new GUIContent(settings.rollLeft.ToString()),GUILayout.Width(125)))
                {
                    settingRollLeft = true;
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Roll Right",GUILayout.ExpandWidth(true));

            if (settingRollRight)
            {
                GUILayout.Label("<Press any key>");
                if (Event.current.isKey)
                {
                    settings.rollRight = Event.current.keyCode;
                    settings.Save();
                    settingRollRight = false;
                }
            }
            else
            {
                if (GUILayout.Button(new GUIContent(settings.rollRight.ToString()),GUILayout.Width(125)))
                {
                    settingRollRight = true;
                }
            }
            GUILayout.EndHorizontal();

            bool newUseStockToolbar;
            if (ToolbarManager.ToolbarAvailable)
            {
                GUILayout.Space(10f);
                newUseStockToolbar = GUILayout.Toggle(settings.useStockToolbar, "Use stock toolbar");
            }
            else
            {
                newUseStockToolbar = true;
            }

            if (newUseStockToolbar != settings.useStockToolbar)
            {
                settings.useStockToolbar = newUseStockToolbar;
                settings.Save();
            }

            GUILayout.EndScrollView();
            GUILayout.Space(25f);
            GUILayout.EndVertical();

            if (GUI.Button(new Rect(windowRect.width - 18, 3f, 15f, 15f), new GUIContent("X")))
            {
                showWindow = false;
                launcherButton.SetFalse();
            }

            GUI.DragWindow();

        }

    }
}
