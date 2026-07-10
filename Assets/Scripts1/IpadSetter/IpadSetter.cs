using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class IpadSetter : MonoBehaviour
    {
        [SerializeField] private Transform levelMainT;//0.65
        [SerializeField] private Transform sonuT;//0.35 x=-2.5,y=-1.7
        [SerializeField] private float aspectRatioThreshold = 1.4f; // 4:3 aspect ratio


        private void Awake()
        {
            DetectAndSetIpadValues();
        }
        private void DetectAndSetIpadValues()
        {
            float aspectRatio = (float)Screen.width / Screen.height;

            if (aspectRatio < aspectRatioThreshold)
            {
                SetIpadScales();
                return;
            }
        }
        private void SetIpadScales()
        {
            levelMainT.localScale = new Vector3(0.65f, 0.65f, 0.65f);
            sonuT.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            sonuT.position = new Vector2(-4.5f, -3f);
        }
    }
}
