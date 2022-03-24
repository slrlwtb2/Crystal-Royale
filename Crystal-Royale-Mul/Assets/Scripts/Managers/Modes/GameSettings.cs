using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//public enum GameMode
//{
//    Origin = 0,
//    Phantom = 1,
//    Swap =2
//}
[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private string _gameVersion = "0.0.0";
    public string GameVersion { get { return _gameVersion; } }
    [SerializeField]
    private string _nickName = "Crystal_Royale";

    public string NickName
    {
        get
        {
            int value = Random.Range(0, 9999);
            return _nickName + value.ToString();
        }
    }

    // public static GameMode GameMode = GameMode.Origin;


}

