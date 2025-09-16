using DG.Tweening;
using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class LeafDropHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer halfLeafRenderer;
        private Bounds bounds;


        public void SetCorrectHalfLeafSprite(Sprite s)=>halfLeafRenderer.sprite = s;


        private void Start()
        {
            bounds = halfLeafRenderer.bounds;
            GameManager.Instance.OnLevelStart += OnLevelStart;
            GameManager.Instance.OnLevelLose += OnLevelLose;
        }
        private void OnLevelStart()
        {
            halfLeafRenderer.enabled = true;
        }
        private void OnLevelLose()
        {
            halfLeafRenderer.enabled = false;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<LeafHandler>(out var l))
            {
                if (l != null)
                {
                    l.SetIsInDropone(true);
                    DoCheckingAnimation(l);
                    GameManager.Instance.LevelManager.DisableAllLeafCollider();
                }
            }
        }
        private void CheckIfCorrectOrNot(LeafHandler l)
        {
            if (l.GetLeafType() == LeafType.Correct)
            {
                GameManager.Instance.InvokeLevelWin();
                halfLeafRenderer.enabled = false;
                GameManager.Instance.AnimationController.PlayCorrectAnimation();
            }
            else
            {
                GameManager.Instance.AnimationController.PlayIncorrectAnimation();
                GameManager.Instance.Livesmanager.ReduceLives();
                l.ResetDragObject(false);
            }
        }
        private void DoCheckingAnimation(LeafHandler l)
        {
            if (l != null)
            {
                //new Vector3((bounds.max.x + 0.5f), (bounds.max.y - 1f))
                l.transform.DOMove(transform.position, 0.5f).OnComplete(() =>
                {
                    CheckIfCorrectOrNot(l);
                });
            }
        }
        private void OnDestroy()
        {
            GameManager.Instance.OnLevelStart -= OnLevelStart;
            GameManager.Instance.OnLevelLose -= OnLevelLose;
        }
    }
}
