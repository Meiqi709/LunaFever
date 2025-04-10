using UnityEngine;
using UnityEngine.SceneManagement;

    [CreateAssetMenu(fileName = "New Carousel Entry", menuName = "UI/Carousel Entry", order = 0)]
    public class CarouselEntry : ScriptableObject
    {
        [field:SerializeField] public Sprite EntryGraphic { get; private set; }
        [field:SerializeField] public string Headline { get; private set; }
        [field:SerializeField] public AudioClip AudioClip { get; private set; }
        [field:SerializeField] public TrackTimerLists_Dic trackData { get; private set; }

        
        [Header("Interaction")] 
        [SerializeField] private string levelNameToLoad="PlayScene";
        
        public void Interact()
        {
            SongPlayingManager.Instance.SelectedAudioClip = AudioClip;
            SongPlayingManager.Instance.SelectedTrackData = trackData;

            SceneManager.LoadScene("PlayScene");  
        }
    }

