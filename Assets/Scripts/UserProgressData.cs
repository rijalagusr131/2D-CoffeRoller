using System.Collections.Generic;

[System.Serializable]
public class UserProgressData{
    public bool isSoundMuted = true;

    public bool IsSoundMuted{ get{ return isSoundMuted; } set{ isSoundMuted = value; } }
}