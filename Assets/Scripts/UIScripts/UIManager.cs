using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class UIManager : MonoBehaviour
    {
        [SerialzieField]
        private void EnableLosePanel()
        {
            restartButton.enabled = true;
            losePanel.SetActive(true);
        }
    }
}
