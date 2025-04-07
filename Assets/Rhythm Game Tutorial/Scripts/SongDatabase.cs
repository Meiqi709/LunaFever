using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "SongDatabase", menuName = "Scriptable Objects/SongDatabase")]
public class SongDatabase : ScriptableObject
{
    public List<SongData> songs;

}

public class SongData
{
    public string songName;
    public AudioClip audioClip;
    public TrackTimerLists_Dic trackData; 
    public Sprite coverImage;
}

