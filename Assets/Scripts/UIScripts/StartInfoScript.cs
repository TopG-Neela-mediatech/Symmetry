using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.SYMMETRY
{
    public class StartInfoScript : MonoBehaviour
    {
        [SerializeField] private GameObject canvasObject;
        [SerializeField] private Image infoImage;
        [SerializeField] private Sprite[] infoSprites;
        [SerializeField] private Button nextButton;
        private int index;


        private void Start()
        {           
            nextButton.onClick.AddListener(LoadNextImage);
        }
        public void StartInfoPanel()
        {
            canvasObject.SetActive(true);
            OnLevelStart();
        }
        private void OnLevelStart()
        {
            infoImage.sprite = infoSprites[0];
            index = 0;
        }
        private IEnumerator EnableBUttonAfterDelay(float d)
        {
            yield return new WaitForSeconds(d);
            nextButton.interactable = true;
        }
        private void LoadNextImage()
        {
            nextButton.interactable = false;
            index++;
            if (index == infoSprites.Length)
            {
                canvasObject.SetActive(false);               
                return;
            }
            StartCoroutine(EnableBUttonAfterDelay(1f));
            infoImage.sprite = infoSprites[index];
        }
    }
}
