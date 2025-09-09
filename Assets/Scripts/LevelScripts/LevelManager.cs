using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LeafHandler[] leafHandlers;
        [SerializeField] private GameObject correctTextCanvas;
        [SerializeField] private LeafDropHandler leafDropHandler;
        [SerializeField] private SpriteRenderer correctSpriteR;
        [SerializeField] private GameObject correctAnimationSprite;      
        [SerializeField] private LevelSO levelSO;
        private int currentLevelIndex;


        public LeafHandler[] GetAllLeafHandlers() => leafHandlers;
        private void StartLevel() => GameManager.Instance.InvokeLevelStart();


        private void Start()
        {
            GameManager.Instance.OnLevelStart += OnLevelStart;
            GameManager.Instance.OnLevelWin += OnLevelWin;
            SetBigLeafScale();
            currentLevelIndex = 0;
            StartLevel();
        }
        private void OnLevelStart()
        {
            SetLevel();
        }
        private IEnumerator LoadNextLevelAfterDelay()
        {
            yield return new WaitForSeconds(3f);
            correctSpriteR.gameObject.SetActive(false);
            correctTextCanvas.SetActive(false);
            correctAnimationSprite.gameObject.SetActive(false);
            currentLevelIndex++;
            if(currentLevelIndex >levelSO.levelData.Length-1)
            {
                Debug.Log("GameOver");
                yield break;
            }
            StartLevel();
        }
        private void SetLevel()
        {
            LevelData l = levelSO.levelData[currentLevelIndex];
            if (l != null)
            {
                int[] randNumbers = GetRandomArray();
                for (int i = 0; i < leafHandlers.Length; i++)
                {
                    int n = randNumbers[i];
                    leafHandlers[i].SetLeafData(levelSO.levelData[currentLevelIndex].LeafData[n].leafSprite,
                        levelSO.levelData[currentLevelIndex].LeafData[n].leafType);
                }
            }
            leafDropHandler.SetCorrectHalfLeafSprite(levelSO.levelData[currentLevelIndex].correctHalfLeafSprtie);
        }
        public int[] GetRandomArray()
        {
            int[] result = { 0, 1, 2, 3 };
            for (int i = result.Length - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                int temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }
            return result;
        }
        private void SetBigLeafScale()
        {
            foreach (var handler in leafHandlers)
            {
                handler.SetBigLeafScale(leafDropHandler.transform.localScale);
            }
        }
        private void MoveFullLeafToCenter()
        {
            correctSpriteR.sprite = levelSO.levelData[currentLevelIndex].correctLeafSprite;
            correctSpriteR.gameObject.SetActive(true);
            correctSpriteR.transform.DOLocalMoveX(3f, 1);
        }
        private void OnLevelWin()
        {
            correctTextCanvas.SetActive(true);
            DOVirtual.DelayedCall(1f, DoWinningAnimation);
            MoveFullLeafToCenter();
            StartCoroutine(LoadNextLevelAfterDelay());
        }
        private void DoWinningAnimation()
        {
            correctAnimationSprite.SetActive(true);
            correctAnimationSprite.transform.DOShakeRotation(2f, 10f, 5);
        }
    }
}
