using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class AudioMapper : MonoBehaviour
    {

        [SerializeField] private AudioSource levelAudioSource;

        public string introClip;
        public string outroClip;
        public string slide1;
        public string slide2;
        public string[] genericIntro;


        private void PlayLevelAudio(string clip)
        {
            if (clip != null)
            {
                levelAudioSource.PlayOneShot(RuntimeAudioLoader.Instance.GetClip(clip));
            }
        }

        public void PlayIntro()//for level one play intro/welcome audio
        {
            if (levelAudioSource.isPlaying) { levelAudioSource.Stop(); }
            PlayLevelAudio(introClip);
            //return soundData.introClip.length;
        }
        public void PlayFirstSlideAudio()
        {
            if (levelAudioSource.isPlaying) { levelAudioSource.Stop(); }
            PlayLevelAudio(slide1);
        }
        public void PlaySecondSlideAudio()
        {
            if (levelAudioSource.isPlaying) { levelAudioSource.Stop(); }
            PlayLevelAudio(slide2);
        }

    }
}
