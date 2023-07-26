using MelonLoader;
using BTD_Mod_Helper;
using AutoNudge;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Helpers;
using BTD_Mod_Helper.Api.ModOptions;
using HarmonyLib;
using Il2Cpp;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.Settings;
using Il2CppGeom;
using UnityEngine;
using UnityEngine.InputSystem;

[assembly: MelonInfo(typeof(AutoNudgeMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace AutoNudge;

public class AutoNudgeMod : BloonsTD6Mod
{
    public static readonly ModSettingHotkey AutoNudgeHotkey = new(KeyCode.Tab)
    {
        description = "HotKey that will automatically nudge towers to the closest valid spot while placing them.",
        icon = ModContent.GetTextureGUID<AutoNudgeMod>("Icon")
    };

    public override void OnUpdate()
    {
        if (InGame.instance == null || InGame.instance.bridge == null || !AutoNudgeHotkey.JustPressed()) return;

        var inputManager = InGame.instance.InputManager;

        if (!inputManager.IsInPlacementMode) return;

        var realCursorPos = InputSystemController.MousePosition;

        var i = 0;
        while (!CanPlace(realCursorPos))
        {
            realCursorPos = InputSystemController.MousePosition + Vector2.up.Rotate(i * 10) * i / 10;
            realCursorPos = new Vector2((int) realCursorPos.x, (int) realCursorPos.y);

            if (i++ > 20000)
            {
                ModHelper.Msg<AutoNudgeMod>("No spot found");
                return;
            }
        }

        Mouse.current.WarpCursorPosition(realCursorPos);
    }
    
    public static bool CanPlace(Vector2 realCursorPos)
    {
        var inputManager = InGame.instance.InputManager;
        var bridge = InGame.instance.bridge;
        var cursorWorld = InGame.instance.GetWorldFromPointer(realCursorPos);

        return bridge.CanPlaceTowerAt(cursorWorld, inputManager.placementModel, bridge.MyPlayerNumber,
            inputManager.placementEntityId);
    }
}
