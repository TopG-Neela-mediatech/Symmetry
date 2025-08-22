using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class LeafHandler : MonoBehaviour
    {
        [SerializeField] private Collider2D col;
        private Vector3 startPos;
        private bool dragging;


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
            if (Input.touchCount == 1)
            {

                Touch touch = Input.GetTouch(0);
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.WorldToScreenPoint(transform.position).z));
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider == col)
                        dragging = true; 
                }
                if (dragging && touch.phase == TouchPhase.Moved)
                {                 
                    transform.position = new Vector3(touchPos.x, touchPos.y, startPos.z);
                }
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    dragging = false;
                    transform.position = startPos;
                }
            }
            else
            {
                dragging = false;
                transform.position = startPos;
            }
        }
    }
}
