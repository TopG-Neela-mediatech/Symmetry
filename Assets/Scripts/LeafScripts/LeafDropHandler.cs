using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class LeafDropHandler : MonoBehaviour
    {
        [SerializeField] private LeafType leafType;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<LeafHandler>(out var l))
            {
                if (l != null)
                {
                    l.SetIsInDropone(true);
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
            }
        }
    }
    }
