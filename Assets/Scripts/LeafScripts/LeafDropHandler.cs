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
                    l.DisableAllCollider();
                }
            }
        }
        private void CheckIfCorrectOrNot(LeafHandler l, Vector3 ogScale)
        {
            if (leafType == l.GetLeafType())
            {
                Debug.Log("Correct");
            }
            else
            {
                Debug.Log("Incorrect");
                l.transform.DOScale(ogScale, 1f).OnComplete(() =>
                {
                    l.ResetDragObject();
                });
            }
        }
        private void DoCheckingAnimation(LeafHandler l)
        {
            if (l != null)
            {
                //new Vector3((bounds.max.x + 0.5f), (bounds.max.y - 1f))
                l.transform.DOMove(transform.position, 1f).OnComplete(() =>
                {
                    Vector3 leafscale = l.transform.localScale;
                    l.transform.DOScale(transform.localScale, 1f).OnComplete(() =>
                    {
                        CheckIfCorrectOrNot(l, leafscale);
                    });
                });
            }
        }
    }
}
