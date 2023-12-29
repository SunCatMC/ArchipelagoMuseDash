﻿using Archipelago.MultiClient.Net.Models;
using Il2CppGameLogic;
using Il2CppPeroPeroGames.GlobalDefines;

namespace ArchipelagoMuseDash.Archipelago.Traps;

/// <summary>
/// This trap activates a white noise texture over the screen. It makes the game unplayable, so its left deactivated
/// </summary>
public class OldTVTrap : ITrap {
    public string TrapName => "Old TV";
    public string TrapMessage => "★★ Trap Activated ★★\nOld TV!";
    public NetworkItem NetworkItem { get; set; }

    public void PreGameSceneLoad() { }

    public void LoadMusicDataByFilenameHook() { }

    public void SetRuntimeMusicDataHook(List<MusicData> data) {
        ArchipelagoStatic.ArchLogger.LogDebug("MutownTrap", "SetRuntimeMusicDataHook");

        var oldTVNoteData = CreateOldTVNoteData();
        TrapHelper.InsertAtStart(data, TrapHelper.CreateDefaultMusicData(oldTVNoteData.uid, oldTVNoteData));

        for (int i = data.Count - 1; i > 1; i--) {
            var bmsUid = data[i].noteData.bmsUid;
            if (bmsUid != BmsNodeUid.OldTv && bmsUid != BmsNodeUid.OldTvOver)
                continue;
            TrapHelper.RemoveIndex(data, i);
        }

        TrapHelper.FixIndexes(data);
    }

    public void OnEnd() { }

    private NoteConfigData CreateOldTVNoteData() => new NoteConfigData() {
        id = "91",
        ibms_id = "2G",
        uid = "000809",
        mirror_uid = "000809",
        scene = "0",
        des = "花屏",
        prefab_name = "000809",
        type = 27,
        effect = "0",
        key_audio = "0",
        boss_action = "0",
        left_perfect_range = 0,
        left_great_range = 0,
        right_perfect_range = 0,
        right_great_range = 0,
        damage = 0,
        pathway = 0,
        speed = 1,
        score = 0,
        fever = 0,
        missCombo = false,
        addCombo = false,
        jumpNote = false,
        isShowPlayEffect = false,
        m_BmsUid = BmsNodeUid.OldTv,
        sceneChangeNames = null
    };
}