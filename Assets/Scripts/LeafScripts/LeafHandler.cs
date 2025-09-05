using DG.Tweening;
using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class LeafHandler : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private SpriteRenderer boundarySprite;
        [SerializeField] private LeafType leafType;
        private Bounds bounds;
        private Vector3 dragOffset;
        private Vector3 startPos;
        private int touchId = -1;
        private bool isInDropZone = false;


        public bool isDragging { get; private set; }
        public void SetLeafType(LeafType l) => leafType = l;
        public LeafType GetLeafType() => leafType;
        public void SetIsInDropone(bool status) => isInDropZone = status;


        void Start()
        {
            startPos = transform.position;
            bounds = boundarySprite.bounds;
        }
        private void Update()
        {
            DetectDrag();
        }
        private void DetectDrag()
        {
            if (Input.touchCount > 0 && !isInDropZone)
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
                    float clampedX = Mathf.Clamp(targetPos.x, bounds.min.x+0.5f, bounds.max.x - 0.5f);
                    float clampedY = Mathf.Clamp(targetPos.y, bounds.min.y+0.5f, bounds.max.y-0.5f);
                    transform.position = new Vector3(clampedX, clampedY, transform.position.z);
                }
                else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
                {
                    isDragging = false;
                    if (touch.fingerId == touchId)
                    {
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
                isInDropZone = false;
                _collider.enabled = true;
            });
        }
    }
}
