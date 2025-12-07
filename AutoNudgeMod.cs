using MelonLoader;
using BTD_Mod_Helper;
using AutoNudge;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.ModOptions;
using UnityEngine;

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

    public static readonly ModSettingHotkey NudgeLeft = new(KeyCode.LeftArrow)
    {
        icon = VanillaSprites.PS4_Dpad_Left
    };

    public static readonly ModSettingHotkey NudgeRight = new(KeyCode.RightArrow)
    {
        icon = VanillaSprites.PS4_Dpad_Right
    };

    public static readonly ModSettingHotkey NudgeUp = new(KeyCode.UpArrow)
    {
        icon = VanillaSprites.PS4_Dpad_Up
    };

    public static readonly ModSettingHotkey NudgeDown = new(KeyCode.DownArrow)
    {
        icon = VanillaSprites.PS4_Dpad_Down
    };

    public static readonly ModSettingHotkey ConfirmPlacement = new(KeyCode.Return)
    {
        icon = VanillaSprites.SelectedTick
    };

    public static MelonLogger.Instance MelonLogger => Melon<AutoNudgeMod>.Logger;

    public override void OnUpdate() => AutoNudgeUtility.Update();
}
