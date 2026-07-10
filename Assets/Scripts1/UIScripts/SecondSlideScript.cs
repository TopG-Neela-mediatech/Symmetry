using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace TMKOC.SYMMETRY
{
    public class SecondSlideScript : MonoBehaviour
    {
        [SerializeField] private Image rightImage;
        [SerializeField] private Image lineImage;


        private void OnEnable()
        {
            AnimateOnEnbale();
        }
        private void AnimateOnEnbale()
        {
            lineImage.DOFade(0.25f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            rightImage.transform.DOLocalRotate(Vector3.zero, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }
}
