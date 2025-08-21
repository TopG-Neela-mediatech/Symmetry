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
            OnLevelStart();
            nextButton.onClick.AddListener(LoadNextImage);
        }
        private void OnLevelStart()
        {
            infoImage.sprite = infoSprites[0];
            index = 0;
        }
        private void LoadNextImage()
        {
            index++;
            if (index == infoSprites.Length)
            {
                canvasObject.SetActive(false);
                return;
            }
            infoImage.sprite = infoSprites[index];
        }
    }
}
