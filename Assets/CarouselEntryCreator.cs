#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CarouselEntryCreator : MonoBehaviour
{
    [Header("UI References")]
    public Sprite defaultCover;
    public TMP_InputField songTitleInputField;
    public AudioSource bgm;
    public GameObject fileSelectionUI;

    [Header("Track Editor")]
    public CreateNewTrackTimerData trackEditor; // Reference to the track editor script

    private TrackTimerLists_Dic currentTrackData;
    private AudioClip loadedClip;

    public void SelectAudioFile()
    {
    #if UNITY_EDITOR
        string path = EditorUtility.OpenFilePanel("Select Audio", "", "mp3,wav,ogg");
        if (!string.IsNullOrEmpty(path))
        {
            StartCoroutine(LoadAudioClipFromPath(path));
        }
    #endif
    }

    private IEnumerator LoadAudioClipFromPath(string filePath)
    {
        string url = "file:///" + filePath;
        using (WWW www = new WWW(url))
        {
            yield return www;

            loadedClip = www.GetAudioClip(false, false);
            if (loadedClip != null)
            {
                bgm.clip = loadedClip;
                bgm.Play();

                // Automatically create an empty track data ScriptableObject
                currentTrackData = ScriptableObject.CreateInstance<TrackTimerLists_Dic>();
                currentTrackData.trackTimerLists = new System.Collections.Generic.List<PointGameObject>();

                // Pass data to the track editor
                trackEditor.bgm = bgm;
                trackEditor.trackTimerLists_Dic = currentTrackData;

                Debug.Log("Audio loaded: " + loadedClip.name);
            }
            else
            {
                Debug.LogError("Failed to load audio.");
            }
        }
    }

    public void OnClickSaveButton()
    {
    #if UNITY_EDITOR
        if (loadedClip == null || currentTrackData == null)
        {
            Debug.LogWarning("Missing audio or track data.");
            return;
        }

        string title = string.IsNullOrEmpty(songTitleInputField.text) ? loadedClip.name : songTitleInputField.text;

        // 1. Save TrackTimerLists_Dic
        string trackPath = $"Assets/Rhythm Game Tutorial/TrackData/{title}_TrackData.asset";
        AssetDatabase.CreateAsset(currentTrackData, trackPath);

        // 2. Create CarouselEntry asset
        CarouselEntry entry = ScriptableObject.CreateInstance<CarouselEntry>();

        SerializedObject so = new SerializedObject(entry);
        so.FindProperty("<AudioClip>k__BackingField").objectReferenceValue = loadedClip;
        so.FindProperty("<trackData>k__BackingField").objectReferenceValue = currentTrackData;
        so.FindProperty("<EntryGraphic>k__BackingField").objectReferenceValue = defaultCover;
        so.FindProperty("<Headline>k__BackingField").stringValue = title;
        so.ApplyModifiedProperties();

        string entryPath = $"Assets/Rhythm Game Tutorial/SongData/{title}.asset";
        AssetDatabase.CreateAsset(entry, entryPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        trackEditor.isReady = true;
        fileSelectionUI.SetActive(false);

        Debug.Log("Save successful: " + entryPath);
    #endif
    }
}
