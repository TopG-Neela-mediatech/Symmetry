using DG.Tweening;
using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class LeafHandler : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Rigidbody2D rb;
        private Vector3 dragOffset;
        //private bool isInDropZone = false; 
        //[SerializeField] private Transform originalPosition;
        private Vector3 startPos;
        private int touchId = -1;


        public bool isDragging { get; private set; }
        //public void SetIsInDropone(bool status) => isInDropZone = status;


        void Start()
        {
            startPos = transform.position;
        }
        void Update()
        {
            DetectDrag();
        }
        private void DetectDrag()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 touchWorldPosition = cam.ScreenToWorldPoint(touch.position);
                    RaycastHit2D hit = Physics2D.Raycast(touchWorldPosition, Vector2.zero);
                    if (hit.collider == _collider)
                    {
                        isDragging = true;
                        touchId = touch.fingerId;
                        dragOffset = transform.position - touchWorldPosition;
                    }
                }
                else if (touch.phase == TouchPhase.Moved && isDragging && touch.fingerId == touchId)
                {
                    Vector3 touchWorldPosition = cam.ScreenToWorldPoint(touch.position);
                    Vector3 targetPos = touchWorldPosition + dragOffset;
                    rb.MovePosition(targetPos);
                }
                else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
                {
                    isDragging = false;
                    if (touch.fingerId == touchId)
                    {
                        /*if (isInDropZone)
                        {
                            return;
                        }*/
                        ResetDragObject();
                    }
                }
            }
        }
        public void ResetDragObject()
        {
            _collider.enabled = false;
            transform.DOMove(startPos, 0.5f).OnComplete(() =>
            {
                touchId = -1;
                //isInDropZone = false;
                _collider.enabled = true;
                //GameManager.Instance.LevelManager.EnableHintButton();
            });
        }
    }
}
