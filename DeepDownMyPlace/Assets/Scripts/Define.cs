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

    public enum Sound // Sound 타입 관리
    {
        Bgm,
        Effect,
        MaxCount // Sound 타입의 총 수가 몇개인지 파악하기 위한 용도 // MaxCount의 번호 = Sound 타입의 수, 현재는 2
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