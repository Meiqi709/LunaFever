using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreateNewTrackTimerData : MonoBehaviour
{
    private float currentTrackId = -999f; 
    public GameObject pointPre;
    public AudioSource bgm;
    private float _cooldown =0.05f;
    public TrackTimerLists_Dic trackTimerLists_Dic;
    // Start is called before the first frame update
    void Start()
    {
        bgm.Pause();

    }

    // Update is called once per frame
    void Update()
    {
        OnClickPlay();
        foreach (var point in trackTimerLists_Dic.trackTimerLists)
        {
          point.gameObject.transform.position = new Vector3(point.trackId, (bgm.time - point.timer)*10,0);
        }

        // _cooldown -= Time.deltaTime;
        // if (_cooldown <= 0)
        // {
        //     _cooldown = 0.05f;
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("update input key D works");
                AddPointFromKeyCode(KeyCode.D);
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("update input key F works");
                AddPointFromKeyCode(KeyCode.F);
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log("update input key J works");
                AddPointFromKeyCode(KeyCode.J);
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                Debug.Log("update input key K works");
                AddPointFromKeyCode(KeyCode.K);
            }

//        }
    }

    void OnClickPlay()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(bgm.isPlaying)
            {
                bgm.Pause();
            }
            else{
                bgm.Play();
            }
        }
    }

    void AddPoint(float trackId, float currentTime)
    {
        PointGameObject pNode = new PointGameObject();
        pNode.timer = currentTime;
        pNode.trackId = trackId;
        pNode.gameObject = Instantiate(pointPre);
        //pNode.gameObject.transform.parent =line;
        trackTimerLists_Dic.trackTimerLists.Add(pNode);
        Debug.Log("TrackId: " + currentTrackId + ", Time: " + bgm.time);

    }

    void AddPointFromKeyCode(KeyCode keyCode)
    {

            switch (keyCode)
            {
                case KeyCode.D:
                currentTrackId = -1.5f; //为音符实际生成的x轴的位置
                break;
                case KeyCode.F:
                currentTrackId = -0.5f;
                break;
                case KeyCode.J:
                currentTrackId = 0.5f;
                break; 
                case KeyCode.K:
                currentTrackId = 1.5f;
                break;
            }
        AddPoint(currentTrackId, bgm.time);
    }

} 