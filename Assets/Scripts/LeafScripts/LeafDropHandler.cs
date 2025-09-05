using DG.Tweening;
using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class LeafDropHandler : MonoBehaviour
    {
        [SerializeField] private LeafType leafType;
        [SerializeField] private SpriteRenderer halfLeafRenderer;
        private Bounds bounds;


        private void Start()
        {
            bounds = halfLeafRenderer.bounds;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<LeafHandler>(out var l))
            {
                if (l != null)
                {
                    l.SetIsInDropone(true);
                    DoCheckingAnimation(l);
                }
            }
        }
        private void CheckIfCorrectOrNot(LeafHandler l)
        {
            if (leafType == l.GetLeafType())
            {
                Debug.Log("Correct");
            }
            else
            {
                Debug.Log("Incorrect");
                l.ResetDragObject();
            }
        }
        private void DoCheckingAnimation(LeafHandler l)
        {
            if (l != null)
            {
                l.transform.DOMove(new Vector3((bounds.max.x + 0.5f), (bounds.max.y-1f)), 1f).OnComplete(() =>
                {
                    CheckIfCorrectOrNot(l);
                });
            }
        }
    }
}
