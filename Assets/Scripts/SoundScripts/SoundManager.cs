using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SoundSO englishSO;
        [SerializeField] private AudioMapper audioMapper;
        /*[SerializeField] private SoundSO hindiSO;
        [SerializeField] private SoundSO marathiSO;
        [SerializeField] private SoundSO tamilSO;
        [SerializeField] private SoundSO frenchSO;
        [SerializeField] private SoundSO malayalamSO;
        [SerializeField] private SoundSO punjabiSO;
        [SerializeField] private SoundSO bengaliSO;
        [SerializeField] private AudioSource levelAudioSource;
        [SerializeField] private string audioLocalization;
        private SoundSO soundData;*/


        private void Start()
        {
            GameManager.Instance.OnLevelLose += PlayRetryAudio;
            if (GameManager.Instance.LevelManager.GetCurrentLevelIndex() != 0)
            {
                PlayGenericQuestions();
            }
        }
        public void PlayIntro()//for level one play intro/welcome audio
        {
            RuntimeAudioLoader.Instance.PlayRuntimeAudio(audioMapper.Intro);
        }
        public void PlayFirstSlideAudio()
        {
            RuntimeAudioLoader.Instance.PlayRuntimeAudio(audioMapper.TutorialIntros[0]);
        }
        public void PlaySecondSlideAudio()
        {
            RuntimeAudioLoader.Instance.PlayRuntimeAudio(audioMapper.TutorialIntros[1]);
        }
        public void PlayOutro()//for level one play intro/welcome audio
        {
            RuntimeAudioLoader.Instance.PlayRuntimeAudio(audioMapper.Outro);
        }
        public void PlayGenericQuestions()
        {
            int rand = UnityEngine.Random.Range(0, audioMapper.GenericIntros.Length);
            RuntimeAudioLoader.Instance.PlayRuntimeAudio(audioMapper.GenericIntros[rand]);
        }
        public void PlayCorrectAudio()
        {
            RuntimeAudioLoader.Instance.PlayCorrectAudioClip();
        }
        public void PlayInCorrectAudio()
        {
            RuntimeAudioLoader.Instance.PlayIncorrectAudioClip();
        }
        private void PlayRetryAudio()
        {
            RuntimeAudioLoader.Instance.PlayRetryAudioClip();
        }
        public void PlayLevelPassAudio()
        {
            RuntimeAudioLoader.Instance.PlayNextLevelAudioClip();
        }
        /*private void SetLanguage()
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
                case "Malayalam":
                    soundData = malayalamSO;
                    break;
                case "Tamil":
                    soundData = tamilSO;
                    break;
                 case "French":
                     soundData = frenchSO;
                     break;
                case "Bengali":
                    soundData = bengaliSO;
                    break;
                default:
                    soundData = englishSO;
                    break;
            }
        }*/
        private void OnDestroy()
        {
            GameManager.Instance.OnLevelLose -= PlayRetryAudio;
        }
    }
}
