using UnityEngine;

namespace TMKOC.SYMMETRY
{
    [CreateAssetMenu(fileName = "SoundSO", menuName = "Scriptable Objects/SoundSO")]
    public class SoundSO : ScriptableObject
    {
        public AudioClip introClip;
        public AudioClip outroClip;
        public AudioClip slide1;
        public AudioClip slide2;
        public AudioClip[] genericIntro;
        public AudioClip[] correctAudio;
        public AudioClip[] incorrectAudio;
        public AudioClip[] retryAudio;
        public AudioClip[] levelPassAudio;
    }
}
