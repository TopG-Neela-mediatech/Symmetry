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
        [SerializeField] private GameObject firstSlideObject;
        [SerializeField] private GameObject secondSlideObject;
        private Tween infoTween;
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
        private void LoadNextImage()
        {
            nextButton.interactable = false;
            if (infoTween != null)
            {
                infoTween.Pause();
            }
            index++;
            if (index == infoStrings.Length)
            {
                canvasObject.SetActive(false);
                return;
            }
            StartCoroutine(TypeText(infoStrings[index]));
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
                    nextButton.interactable = true;
                    StringObjectAnimation(index);
                }
                yield return new WaitForSeconds(.05f);
            }
        }
        private void SetTextSize(int ind)
        {
            switch (ind)
            {
                case 0:
                    infoText.fontSize = 80;
                    break;
                case 1:
                    infoText.fontSize = 50;                   
                    break;
                case 2:
                    infoText.fontSize = 50;
                    firstSlideObject.SetActive(false);
                    break;
                default:return;
            }
        }
        private void StringObjectAnimation(int ind)
        {
            switch (ind)
            {
                case 0:                   
                    infoTween = infoTextT.DOScale(1.1f, 1.5f).SetLoops(-1, LoopType.Yoyo);
                    break;
                case 1:                   
                    infoTextT.DOLocalMoveY(150f, 0.5f).OnComplete(() =>
                    {
                        infoTween.Play();
                        firstSlideObject.SetActive(true);
                    });
                    break;
                case 2:                   
                    infoTween.Play();
                    secondSlideObject.SetActive(true);
                    break;
                default:return;
            }
        }
    }
}
