using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "newTrackTimer", menuName = "CreateData/Create New TrackTimerData")]
public class TrackTimerLists_Dic : ScriptableObject
{
    public List<PointGameObject> trackTimerLists;
} 

public class PointData
{
    public float timer;
    public float trackId;

    public PointData() { }

    public PointData(float timer, int trackId)
    {
        this.timer = timer;
        this.trackId = trackId;
    }
}

[System.Serializable]
public class PointGameObject : PointData
{
    public GameObject gameObject;
}
