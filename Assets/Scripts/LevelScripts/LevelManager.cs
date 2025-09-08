using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LeafHandler[] leafHandlers;


        public LeafHandler[] GetAllLeafHandlers() => leafHandlers;
    }
}
