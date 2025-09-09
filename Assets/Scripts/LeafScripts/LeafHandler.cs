using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class LeafHandler : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private SpriteRenderer boundarySprite;
        [SerializeField] private LeafType leafType;
        [SerializeField] private SpriteRenderer leafSpriteRenderer;
        private Vector3 bigLeafScale;
        private LeafHandler[] allLeaves;
        private Bounds bounds;
        private Vector3 dragOffset;
        private Vector3 startPos;
        private Vector3 ogScale;
        private int touchId = -1;
        private bool isInDropZone = false;
        private bool callOnce;


        public bool isDragging { get; private set; }
        public void SetLeafType(LeafType l) => leafType = l;
        public LeafType GetLeafType() => leafType;
        public void SetIsInDropone(bool status) => isInDropZone = status;
        public SpriteRenderer GetLeafSpriteRenderer() => leafSpriteRenderer;
        public void DisableCollider() => _collider.enabled = false;
        public void EnableCollider() => _collider.enabled = true;
        public void SetBigLeafScale(Vector3 s) => bigLeafScale = s;
        private void EnableObject()=>gameObject.SetActive(true);


        void Start()
        {
            ogScale = transform.localScale;
            startPos = transform.position;
            bounds = boundarySprite.bounds;
            allLeaves = GameManager.Instance.LevelManager.GetAllLeafHandlers();
            GameManager.Instance.OnLevelWin += OnLevelWin;
            GameManager.Instance.OnLevelStart += OnLevelStart;
        }
        private void Update()
        {
            DetectDrag();
        }       
        public void SetLeafData(Sprite s, LeafType l)
        {
            leafSpriteRenderer.sprite = s;
            leafType = l;
        }
        private void OnLevelStart()
        {
            EnableObject();
        }
        private void OnLevelWin()
        {
            ResetDragObject();
            gameObject.SetActive(false);
            UnFadeLeaf();
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
                    float clampedX = Mathf.Clamp(targetPos.x, bounds.min.x + 0.5f, bounds.max.x - 0.5f);
                    float clampedY = Mathf.Clamp(targetPos.y, bounds.min.y + 0.5f, bounds.max.y - 0.5f);
                    transform.position = new Vector3(clampedX, clampedY, transform.position.z);
                    if (targetPos.x < startPos.x) // Dragging left
                    {
                        FadeOtherLeaves(0.3f, this);
                    }
                    ScaleUpWhileDragging();
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
        private void UnFadeLeaf()
        {
            GetLeafSpriteRenderer().DOFade(1f, 0f);
        }
        public void FadeOtherLeaves(float targetAlpha, LeafHandler l)
        {
            foreach (var leaf in allLeaves)
            {
                if (leaf != l) // skip self
                {
                    leaf.GetLeafSpriteRenderer().DOFade(targetAlpha, 0.5f);
                }
            }
        }
        public void DisableAllCollider()
        {
            foreach (var leaf in allLeaves)
            {
                leaf.DisableCollider();
            }
        }
        private void EnableAllCollider()
        {
            foreach (var leaf in allLeaves)
            {
                if (leaf != this)
                {
                    leaf.EnableCollider();
                }
            }
        }
        private void ScaleUpWhileDragging()
        {
            if (!callOnce)
            {
                callOnce = true;
                transform.DOScale(bigLeafScale, 0.5f);
            }
        }
        public void ResetDragObject()
        {
            callOnce = false;
            FadeOtherLeaves(1f, this);
            _collider.enabled = false;
            transform.DOScale(ogScale, 0.5f);
            transform.DOMove(startPos, 0.5f).OnComplete(() =>
            {
                touchId = -1;
                isInDropZone = false;
                _collider.enabled = true;
                EnableAllCollider();
            });
        }
        private void OnDestroy()
        {
            GameManager.Instance.OnLevelWin -= OnLevelWin;
            GameManager.Instance.OnLevelStart -= OnLevelStart;
        }
    }
}
