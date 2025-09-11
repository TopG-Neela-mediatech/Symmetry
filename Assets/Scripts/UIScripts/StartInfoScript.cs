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
        [SerializeField] private string[] infoStrings;
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
            infoText.text = infoStrings[0];
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
            if (index == infoStrings.Length)
            {
                canvasObject.SetActive(false);               
                return;
            }
            StartCoroutine(EnableBUttonAfterDelay(1f));
            infoText.text = infoStrings[index];
        }
    }
}
