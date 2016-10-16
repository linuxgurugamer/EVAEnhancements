using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.UI.Screens;

namespace EVAEnhancementsContinued
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class SettingsWindowBehaviour : MonoBehaviour
    {
        

        Settings settings = SettingsWrapper.Instance.gameSettings;
        SettingsWindow settingsWindow = null;

        bool visibleUI = true;

        internal void Awake()
        {
            SettingsWrapper.Instance.gameSettings.Load();
            SettingsWrapper.Instance.gameSettings.Save();

            if (SettingsWrapper.Instance.gameSettings.useStockToolbar)
            {
                GameEvents.onGUIApplicationLauncherReady.Add(OnGUIApplicationLauncherReady);
            }

            GameEvents.onGameSceneLoadRequested.Add(onSceneChange);

        }

        internal void Start()
        {
            // Add hooks for showing/hiding on F2
            GameEvents.onShowUI.Add(showUI);
            GameEvents.onHideUI.Add(hideUI);

            settingsWindow = new SettingsWindow();
            addLauncherButtons();
        }

        // Remove the launcher button when the scene changes
        internal void onSceneChange(GameScenes scene)
        {
            removeLauncherButtons();
        }

        internal void showUI() // triggered on F2
        {
            visibleUI = true;
        }

        internal void hideUI() // triggered on F2
        {
            visibleUI = false;
        }

        internal void OnGUIApplicationLauncherReady()
        {
            if (settingsWindow.launcherButton == null && settings.useStockToolbar)
            {
                settingsWindow.launcherButton = ApplicationLauncher.Instance.AddModApplication(showWindow, hideWindow, null, null, null, null, ApplicationLauncher.AppScenes.FLIGHT | ApplicationLauncher.AppScenes.MAPVIEW, SettingsWrapper.Instance.modStyle.GetImage("EVAEnhancementsContinued/textures/toolbar", 38, 38));
            }
        }

        internal void addLauncherButtons()
        {
            // Load Blizzy toolbar
            if (settingsWindow.blizzyButton == null)
            {
                if (ToolbarManager.ToolbarAvailable)
                {
                    // Create button
                    settingsWindow.blizzyButton = ToolbarManager.Instance.add("EVAEnhancementsContinued", "blizzyButton");
                    settingsWindow.blizzyButton.TexturePath = "EVAEnhancementsContinued/textures/blizzyToolbar";
                    settingsWindow.blizzyButton.ToolTip = "EVA Enhancements";
                    settingsWindow.blizzyButton.OnClick += (e) => toggleWindow();
                }
                else
                {
                    // Blizzy Toolbar not available, fall back to stock launcher
                    SettingsWrapper.Instance.gameSettings.useStockToolbar = true;
                }
            }

            // Load Application Launcher
            if (settingsWindow.launcherButton == null && SettingsWrapper.Instance.gameSettings.useStockToolbar)
            {
                OnGUIApplicationLauncherReady();
            }
        }

        internal void removeLauncherButtons()
        {
            if (settingsWindow.launcherButton != null)
            {
                removeApplicationLauncher();
            }
            if (settingsWindow.blizzyButton != null)
            {
                settingsWindow.blizzyButton.Destroy();
            }
        }

        internal void removeApplicationLauncher()
        {
            GameEvents.onGUIApplicationLauncherReady.Remove(OnGUIApplicationLauncherReady);
            ApplicationLauncher.Instance.RemoveModApplication(settingsWindow.launcherButton);
        }

        internal void showWindow()  // triggered by application launcher
        {
            settingsWindow.showWindow = true;
        }

        internal void hideWindow() // triggered by application launcher
        {
            settingsWindow.showWindow = false;
        }

        internal void toggleWindow()
        {
            if (settingsWindow.launcherButton != null)
            {
                if (settingsWindow.showWindow)
                {
                    settingsWindow.launcherButton.SetFalse();
                }
                else
                {
                    settingsWindow.launcherButton.SetTrue();
                }
            }
            else
            {
                if (settingsWindow.showWindow)
                {
                    hideWindow();
                }
                else
                {
                    showWindow();
                }
            }
        }

        internal void OnGUI()
        {
            if (visibleUI)
            {
                settingsWindow.draw();
            }
        }

        internal void Update()
        {
            // Load Application Launcher
            if (settingsWindow.launcherButton == null && SettingsWrapper.Instance.gameSettings.useStockToolbar)
            {
                OnGUIApplicationLauncherReady();
                if (settingsWindow.showWindow)
                {
                    settingsWindow.launcherButton.SetTrue();
                }
            }

            // Destroy application launcher
            if (settingsWindow.launcherButton != null && SettingsWrapper.Instance.gameSettings.useStockToolbar == false)
            {
                removeApplicationLauncher();
            }

        }

        internal void OnDestroy()
        {
            settingsWindow.showWindow = false;

            removeLauncherButtons();

            GameEvents.onGameSceneLoadRequested.Remove(onSceneChange);
            GameEvents.onShowUI.Remove(showUI);
            GameEvents.onHideUI.Remove(hideUI);

        }
    }
}
