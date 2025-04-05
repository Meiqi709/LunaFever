using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFromTrackTimerData : MonoBehaviour
{
    [Header("Track time fichier")]
    public TrackTimerLists_Dic trackTimerLists_Dic;

    public AudioSource bgm;

    public GameObject pointPre;

    private List<PointGameObject> tempTrackList = new List<PointGameObject>();

    private void Start()
    {
        bgm.Pause();
        CreateNecks();
    }

    public void CreateNecks()
    {
        if (trackTimerLists_Dic)
        {
            foreach (var item in trackTimerLists_Dic.trackTimerLists)
            {
                AddPoint(item.trackId, item.timer);
            }
        }
    }

    private void AddPoint(float trackId, float timer)
    {
        PointGameObject pNode = new PointGameObject();
        pNode.trackId = trackId;
        pNode.timer = timer;
        pNode.gameObject = Instantiate(pointPre);
        NoteObject noteScript = pNode.gameObject.GetComponent<NoteObject>();

    if (noteScript != null)
    {
        noteScript.keyToPress = GetKeyCodeFromTrackId(trackId);
    }
    else
    {
        Debug.LogError("NoteObject 脚本未找到，检查 Prefab 是否正确挂载！");
    }
        tempTrackList.Add(pNode);
    }

    private KeyCode GetKeyCodeFromTrackId(float trackId)
    {
        switch (trackId)
        {
            case -1.5f: return KeyCode.D;
            case -0.5f: return KeyCode.F;
            case 0.5f:  return KeyCode.J;
            case 1.5f:  return KeyCode.K;
            default:    return KeyCode.None;
        }
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

    void Update()
    {
        OnClickPlay();
        foreach (var item in tempTrackList)
        {
            // 使用 item 来访问 PointGameObject
            item.gameObject.transform.position = new Vector3(item.trackId, (bgm.time - item.timer)*(-5), 0);
            // x 位置是根据 trackId 设置
            // y 位置是根据当前时间与音符时间的差值，乘以-15来控制掉落速度
            // 例如，如果 (20s - 1.02s) * -15 = -285，意味着音符的位置会更低
            // 负位置 (20s - 1.02s) * -15 = -285
            // 正位置 (0.5s - 1.02s) * -15 = 7.5
            
        }
    }
}
