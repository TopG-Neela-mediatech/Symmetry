using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.SYMMETRY
{
    public class StartInfoScript : MonoBehaviour
    {
        [SerializeField] private GameObject canvasObject;
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private Transform infoTextT;
        [SerializeField] private string[] infoStrings;
        [SerializeField] private Button nextButton;
        [SerializeField] private GameObject titleObject;
        [SerializeField] private FirstSlideScript[] firstSlideObject;
        [SerializeField] private SecondSlideScript[] secondSlideObject;
        //private Tween infoTween;
        private Coroutine typingCoroutine;
        private int index;


        private void Start()
        {
            nextButton.onClick.AddListener(LoadNextImage);
        }
        public void StartInfoPanel()
        {
            GameManager.Instance.SoundManager.PlayIntro();
            canvasObject.SetActive(true);
            OnInfoStart();
            StartCoroutine(EnableNextButtonAfterDelay());
        }
        private void OnInfoStart()
        {
            index = 0;           
            nextButton.interactable = false;
        }
        private IEnumerator EnableNextButtonAfterDelay()
        {
            yield return new WaitForSeconds(0.5f);
            nextButton.interactable = true;
        }
        private void LoadNextImage()
        {
            nextButton.interactable = false;          
            index++;
            if (index == infoStrings.Length)
            {
                StartCoroutine(StartGameAfterTransition());
                return;
            }
            typingCoroutine= StartCoroutine(TypeText(infoStrings[index]));
        }
        private IEnumerator StartGameAfterTransition()
        {
            GameManager.Instance.LevelManager.PlayTranitionEffectOnly();
            yield return new WaitForSeconds(2.5f);
            canvasObject.SetActive(false);
            GameManager.Instance.Livesmanager.EnableLives();
            GameManager.Instance.HandTutorialManager.PlayTutorialOnLevelOne();
            GameManager.Instance.SoundManager.PlayGenericQuestions();
        }
        private IEnumerator TypeText(string t)
        { 
            StartCoroutine(EnableNextButtonAfterDelay());
            SetTextSize(index);
            if(typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            infoText.text = "";
            for (int i = 0; i < t.Length; i++)
            {
                infoText.text += t[i];               
                yield return new WaitForSeconds(.03f);
            }
        }
        private void SetTextSize(int ind)
        {
            switch (ind)
            {
                case 0:
                    infoText.fontSize = 80;
                    titleObject.transform.DOScale(1.1f, 1.5f).SetLoops(-1, LoopType.Yoyo);                 
                    break;
                case 1:
                    titleObject.SetActive(false);
                    infoText.fontSize = 50;
                    GameManager.Instance.SoundManager.PlayFirstSlideAudio();
                    foreach (var f in firstSlideObject)
                    {
                        f.gameObject.SetActive(true);
                    }
                    break;
                case 2:
                    GameManager.Instance.SoundManager.PlaySecondSlideAudio();
                    infoText.fontSize = 50;
                    foreach (var f in firstSlideObject)
                    {
                        f.gameObject.SetActive(false);
                    }
                    foreach (var f in secondSlideObject)
                    {
                        f.gameObject.SetActive(true);
                    }
                    break;
                default: return;
            }
        }      
    }
}
