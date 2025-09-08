using DG.Tweening;
using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LeafHandler[] leafHandlers;
        [SerializeField] private GameObject correctTextCanvas;
        [SerializeField] private LeafDropHandler leafDropHandler;
        [SerializeField] private GameObject correctAnimationSprite;

        public LeafHandler[] GetAllLeafHandlers() => leafHandlers;


        private void Start()
        {
            GameManager.Instance.OnLevelWin += OnLevelWin;
            SetBigLeafScale();
        }
        private void SetBigLeafScale()
        {
            foreach (var handler in leafHandlers)
            {
                handler.SetBigLeafScale(leafDropHandler.transform.localScale);
            }
        }
        private void OnLevelWin()
        {
            correctTextCanvas.SetActive(true);
            DOVirtual.DelayedCall(1f, DoWinningAnimation);
        }
        private void DoWinningAnimation()
        {
            correctAnimationSprite.SetActive(true);
            correctAnimationSprite.transform.DOShakeRotation(2f, 10f, 5);
        }
    }
}
