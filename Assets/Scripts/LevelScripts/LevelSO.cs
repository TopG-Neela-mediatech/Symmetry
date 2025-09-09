using UnityEngine;

namespace TMKOC.SYMMETRY
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable Objects/LevelSO")]
    public class LevelSO : ScriptableObject
    {
        public LevelData[] levelData;
    }
    [System.Serializable]
    public class LevelData
    {
        public Sprite correctLeafSprite;
        public Sprite correctHalfLeafSprtie;
        public LeafData[] LeafData;
    }
    [System.Serializable]
    public class LeafData
    {
        public Sprite leafSprite;
        public LeafType leafType;
    }
}
