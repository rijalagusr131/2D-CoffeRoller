using System.Collections.Generic;

[System.Serializable]
public class UserProgressData{
    public bool isSoundMuted = true;
    public bool isTutorialDone;

    public bool IsSoundMuted{ get{ return isSoundMuted; } set{ isSoundMuted = value; } }

    public bool IsTutorialDone { get { return isTutorialDone; } set { isTutorialDone = value; } }
}