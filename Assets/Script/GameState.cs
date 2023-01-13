using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueState
{
    INTERACTION,
    INTERACTION_ACTION,
    DRAW,
    DRAW_ACTION
}


[Serializable]
public enum Pos
{
    RIGHT,
    LEFT,
}

[Serializable]
public struct DialogueID
{
    public string name;
    public Sprite persoSprite;
    public Pos pos;
    public int lineId;
    public int columnId;
}