using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.SYMMETRY
{
    public class FirstSlideScript : MonoBehaviour
    {       
        [SerializeField] private Image rightImage;      
        private void OnEnable()
        {
            AnimateOnEnbale();
        }
        private void AnimateOnEnbale()
        {         
            rightImage.DOFade(0.5f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);           
        }
    }
}
