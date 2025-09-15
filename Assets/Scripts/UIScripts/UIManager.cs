using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TMKOC.SYMMETRY
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject losePanel;      
        [SerializeField] private Button restartButton;
        [SerializeField] private Button playSchoolBackButton;


        private void Start()
        {
            restartButton.onClick.AddListener(RestartLevel);
            GameManager.Instance.OnLevelLose += EnableLosePanel;          
            restartButton.onClick.AddListener(RestartLevel);
            playSchoolBackButton.onClick.AddListener(() => SceneManager.LoadScene(TMKOCPlaySchoolConstants.TMKOCPlayMainMenu));
        }
        private void EnableLosePanel()
        {
            restartButton.enabled = true;
            losePanel.SetActive(true);
        }
        private void RestartLevel()
        {
            restartButton.enabled = false;
            DisablePanels();
            GameManager.Instance.LevelManager.RestartLevel();
        }
        private void DisablePanels()
        {           
            losePanel.SetActive(false);
        }
        private void OnDestroy()
        {
            GameManager.Instance.OnLevelLose -= EnableLosePanel;
        }
    }
}
