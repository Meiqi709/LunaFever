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
        tempTrackList.Add(pNode);
    }

    private void Update()
    {
        foreach (var item in tempTrackList)
        {
            // 使用 item 来访问 PointGameObject
            item.gameObject.transform.position = new Vector3(item.trackId, (bgm.time - item.timer) * -15, 0);
            // x 位置是根据 trackId 设置
            // y 位置是根据当前时间与音符时间的差值，乘以-15来控制掉落速度
            // 例如，如果 (20s - 1.02s) * -15 = -285，意味着音符的位置会更低
            // 负位置 (20s - 1.02s) * -15 = -285
            // 正位置 (0.5s - 1.02s) * -15 = 7.5
        }
    }
}
