using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Unknown,
        Login,
        Game,
        Library,
        Class1,
        Class2,
        Class3,
        Class4,
        Class5,
        Science,
        Music,
        Staffroom
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

    public enum Character
    {
        Player,
        Follower
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

   
}