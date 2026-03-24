using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TMKOC.SYMMETRY
{
    public class UIManager : MonoBehaviour
    {         
        [SerializeField] private Button playSchoolBackButton;


        private void Start()
        {
            PlayschoolCommon.Instance.SpawnplayschoolWinLosePanel();          
            GameManager.Instance.OnLevelLose += EnableLosePanel;                   
            playSchoolBackButton.onClick.AddListener(() => SceneManager.LoadScene(TMKOCPlaySchoolConstants.TMKOCPlayMainMenu));
        }
        private void EnableLosePanel()
        {
            GameManager.Instance.LevelManager.SaveFailedAttempt();
            WinLosePanelScript.Instance.ShowRetryPopUp(RestartLevel);
        }
        private void RestartLevel()
        {                       
            GameManager.Instance.LevelManager.RestartLevel();
        }    
        private void OnDestroy()
        {
            GameManager.Instance.OnLevelLose -= EnableLosePanel;
        }
    }
}
