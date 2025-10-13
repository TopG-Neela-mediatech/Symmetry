using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SoundSO englishSO;
        [SerializeField] private SoundSO hindiSO;
        [SerializeField] private SoundSO marathiSO;
        /*[SerializeField] private SoundSO tamilSO;
        [SerializeField] private SoundSO frenchSO;
        [SerializeField] private SoundSO malayalamSO;*/
        [SerializeField] private SoundSO punjabiSO;
        [SerializeField] private AudioSource levelAudioSource;
        [SerializeField] private string audioLocalization;
        private SoundSO soundData;


        private void Awake()
        {
            SetLanguage();
        }
        private void Start()
        {                     
            GameManager.Instance.OnLevelLose += PlayRetryAudio;
        }
        private void PlayLevelAudio(AudioClip clip)
        {
            if (clip != null)
            {
                levelAudioSource.PlayOneShot(clip);
            }
        }
        public void PlayIntro()//for level one play intro/welcome audio
        {
            if (levelAudioSource.isPlaying) { levelAudioSource.Stop(); }
            PlayLevelAudio(soundData.introClip);
            //return soundData.introClip.length;
        }
        public void PlayFirstSlideAudio()
        {
            if (levelAudioSource.isPlaying) { levelAudioSource.Stop(); }
            PlayLevelAudio(soundData.slide1);
        }
        public void PlaySecondSlideAudio()
        {
            if (levelAudioSource.isPlaying) { levelAudioSource.Stop(); }
            PlayLevelAudio(soundData.slide2);
        }
        public void PlayOutro()//for level one play intro/welcome audio
        {
            if (levelAudioSource.isPlaying) { levelAudioSource.Stop(); }
            PlayLevelAudio(soundData.outroClip);
        }
        public void PlayGenericQuestions()
        {            
            if (levelAudioSource.isPlaying) { levelAudioSource.Stop(); }
            int rand = UnityEngine.Random.Range(0, soundData.genericIntro.Length);
            PlayLevelAudio(soundData.genericIntro[rand]);
        }
        public void PlayCorrectAudio()
        {
            if (levelAudioSource.isPlaying) { levelAudioSource.Stop(); }
            int rand = UnityEngine.Random.Range(0, soundData.correctAudio.Length);
            PlayLevelAudio(soundData.correctAudio[rand]);
        }
        public void PlayInCorrectAudio()
        {
            if (levelAudioSource.isPlaying) { levelAudioSource.Stop(); }
            int rand = UnityEngine.Random.Range(0, soundData.incorrectAudio.Length);
            PlayLevelAudio(soundData.incorrectAudio[rand]);
        }
        private void PlayRetryAudio()
        {
            if (levelAudioSource.isPlaying) { levelAudioSource.Stop(); }
            int rand = UnityEngine.Random.Range(0, soundData.retryAudio.Length);
            PlayLevelAudio(soundData.retryAudio[rand]);
        }
        public void PlayLevelPassAudio()
        {
            if (levelAudioSource.isPlaying) { levelAudioSource.Stop(); }
            int rand = UnityEngine.Random.Range(0, soundData.levelPassAudio.Length);
            PlayLevelAudio(soundData.levelPassAudio[rand]);
        }
        private void SetLanguage()
        {
            audioLocalization = PlayerPrefs.GetString("PlayschoolLanguageAudio", audioLocalization);
            switch (audioLocalization)
            {
                case "English":
                    soundData = englishSO;
                    break;
                case "EnglishUS":
                    soundData = englishSO;
                    break;
                case "Hindi":
                    soundData = hindiSO;
                    break;
                case "Marathi":
                    soundData = marathiSO;
                    break;
                case "Punjabi":
                    soundData = punjabiSO;
                    break;
               /* case "Tamil":
                    soundData = tamilSO;
                    break;
                case "French":
                    soundData = frenchSO;
                    break;
                
                case "Malayalam":
                    soundData = malayalamSO;
                    break;*/
                default:
                    soundData = englishSO;
                    break;
            }
        }
        private void OnDestroy()
        {           
            GameManager.Instance.OnLevelLose -= PlayRetryAudio;           
        }
    }
}
