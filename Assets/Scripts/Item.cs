using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool stackable = false;
    public string readableContent;

    [Header("Both")]
    public Sprite imageSprite;
    public string itemModelTag;
    public string itemUnlockKey; // indicate which lock to open

    public enum ItemType
    {
        Document,
        AudioRecorder,
        Photograph,
        Key,
        Misc

    }

    public enum ActionType
    {
        Unlock,
        Read,
        PlayAudio,
        Observe
    }
}
