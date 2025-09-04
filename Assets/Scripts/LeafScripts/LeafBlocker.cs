using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class LeafBlocker : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<LeafHandler>( out LeafHandler L))
            {
                if (L != null)
                {
                    L.ResetDragObject();
                }
            }
        }
    }
}
