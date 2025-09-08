using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LeafHandler[] leafHandlers;
        [SerializeField] private GameObject correctTextCanvas;

        public LeafHandler[] GetAllLeafHandlers() => leafHandlers;

        private void Start()
        {
            GameManager.Instance.OnLevelWin += OnLevelWin; 
        }
        private void OnLevelWin()
        {
            correctTextCanvas.SetActive(true);
        }
    }
}
