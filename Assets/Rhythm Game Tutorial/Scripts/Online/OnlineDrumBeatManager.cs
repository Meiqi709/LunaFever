using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineDrumBeatManager : MonoBehaviour
{
    private float currentTrackId = -999f;
    public GameObject pointPre;
    public AudioSource bgm;
    public TrackTimerLists_Dic trackTimerLists_Dic;

    void Start()
    {
        bgm.Pause();
    }

    void Update()
    {
        if (OnlineGameManager.Instance != null && OnlineGameManager.Instance.startPlaying && !bgm.isPlaying)
        {
            Debug.Log("DrumBeatManager: StartPlaying detected, starting BGM.");
            bgm.Play();
        }

        foreach (var point in trackTimerLists_Dic.trackTimerLists)
        {
            point.gameObject.transform.position = new Vector3(point.trackId, (bgm.time - point.timer) * 10, 0);
        }

        if (Input.GetKeyDown(KeyCode.D)) AddPointFromKeyCode(KeyCode.D);
        else if (Input.GetKeyDown(KeyCode.F)) AddPointFromKeyCode(KeyCode.F);
        else if (Input.GetKeyDown(KeyCode.J)) AddPointFromKeyCode(KeyCode.J);
        else if (Input.GetKeyDown(KeyCode.K)) AddPointFromKeyCode(KeyCode.K);
    }

    void AddPoint(float trackId, float currentTime)
    {
        PointGameObject pNode = new PointGameObject();
        pNode.timer = currentTime;
        pNode.trackId = trackId;
        pNode.gameObject = Instantiate(pointPre);
        trackTimerLists_Dic.trackTimerLists.Add(pNode);
        Debug.Log("TrackId: " + currentTrackId + ", Time: " + bgm.time);
    }

    void AddPointFromKeyCode(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.D: currentTrackId = -1.5f; break;
            case KeyCode.F: currentTrackId = -0.5f; break;
            case KeyCode.J: currentTrackId = 0.5f; break;
            case KeyCode.K: currentTrackId = 1.5f; break;
        }
        AddPoint(currentTrackId, bgm.time);
    }
}
