using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game
    }

    public enum Sound // Sound Ÿ�� ����
    {
        Bgm,
        Effect,
        MaxCount // Sound Ÿ���� �� ���� ����� �ľ��ϱ� ���� �뵵 // MaxCount�� ��ȣ = Sound Ÿ���� ��, ����� 2
    }

    public enum CameraMode
    {
        QuarterView
    }

    public enum MouseEvent
    {
        Press,
        Click
    }

    public enum UIEvent
    {
        Click,
        Drag
    }

    public enum Element
    {
        Spade,
        Diaamond,
        Heart,
        Club,
        Count
    }
}