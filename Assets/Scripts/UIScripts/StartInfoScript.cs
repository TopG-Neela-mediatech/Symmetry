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
        private int index;


        private void Start()
        {
            nextButton.onClick.AddListener(LoadNextImage);
        }
        public void StartInfoPanel()
        {
            canvasObject.SetActive(true);
            OnInfoStart();
        }
        private void OnInfoStart()
        {
            index = 0;
            StartCoroutine(TypeText(infoStrings[index]));
            nextButton.interactable = false;
        }
        private IEnumerator EnableNextButtonAfterDelay()
        {
            yield return new WaitForSeconds(1.5f);
            nextButton.interactable = true;
        }
        private void LoadNextImage()
        {
            nextButton.interactable = false;
            /*if (infoTween != null)
            {
                infoTween.Pause();
            }*/
            index++;
            if (index == infoStrings.Length)
            {
                StartCoroutine(StartGameAfterTransition());
                return;
            }
            StartCoroutine(TypeText(infoStrings[index]));
        }
        private IEnumerator StartGameAfterTransition()
        {
            GameManager.Instance.LevelManager.PlayTranitionEffectOnly();
            yield return new WaitForSeconds(2.5f);
            canvasObject.SetActive(false);
            GameManager.Instance.Livesmanager.EnableLives();
        }
        private IEnumerator TypeText(string t)
        {
            SetTextSize(index);
            infoText.text = "";
            for (int i = 0; i < t.Length; i++)
            {
                infoText.text += t[i];
                if (i == t.Length - 1)
                {
                    StartCoroutine(EnableNextButtonAfterDelay());
                    StringObjectAnimation(index);
                }
                yield return new WaitForSeconds(.04f);
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
                    break;
                case 2:
                    infoText.fontSize = 50;
                    foreach (var f in firstSlideObject)
                    {
                        f.gameObject.SetActive(false);
                    }
                    break;
                default: return;
            }
        }
        private void StringObjectAnimation(int ind)
        {
            switch (ind)
            {
                case 0:
                    //infoTween = infoTextT.DOScale(1.1f, 1.5f).SetLoops(-1, LoopType.Yoyo);
                    break;
                case 1:
                    infoTextT.DOLocalMoveY(150f, 0.5f).OnComplete(() =>
                    {
                        //infoTween.Play();
                        foreach (var f in firstSlideObject)
                        {
                            f.gameObject.SetActive(true);
                        }
                    });
                    break;
                case 2:
                    //infoTween.Play();
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
