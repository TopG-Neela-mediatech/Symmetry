using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        [SerializeField] private Button playSchoolBackButton;
        [SerializeField] private int gameID;
        private int currentLevelIndex;
        private GameCategoryDataManager gameCategoryDataManager;
        private UpdateCategoryApiManager updateCategoryApiManager;


        public LeafHandler[] GetAllLeafHandlers() => leafHandlers;
        private void StartLevel() => GameManager.Instance.InvokeLevelStart();
        private void SetCurrentLevelIndex()
        {
            currentLevelIndex = gameCategoryDataManager.GetCompletedLevel;
            if (currentLevelIndex >= levelSO.levelData.Length)
            {
                currentLevelIndex = 0;
            }
        }


        private void Awake()
        {
            #region GameID
#if PLAYSCHOOL_MAIN
             // assign varaible in this to get the  game ID from main app
             gameID  = PlayerPrefs.GetInt("currentGameId");
#endif
            #endregion
            gameCategoryDataManager = new GameCategoryDataManager(gameID, "symmetry");
            updateCategoryApiManager = new UpdateCategoryApiManager(gameID);
            SetCurrentLevelIndex();
        }
        private void Start()
        {
            GameManager.Instance.OnLevelStart += OnLevelStart;
            GameManager.Instance.OnLevelWin += OnLevelWin;
            playSchoolBackButton.onClick.AddListener(() => SceneManager.LoadScene(TMKOCPlaySchoolConstants.TMKOCPlayMainMenu));
            SetBigLeafScale();
            StartLevel();
        }

        private void OnLevelStart()
        {
            SetLevel();
            correctSpriteR.transform.DOLocalMoveX(0f, 0f);
        }
        private IEnumerator LoadNextLevelAfterDelay()
        {
            yield return new WaitForSeconds(3f);
            correctSpriteR.gameObject.SetActive(false);
            correctTextCanvas.SetActive(false);
            correctAnimationSprite.gameObject.SetActive(false);
            SaveLevel();
            if (currentLevelIndex > levelSO.levelData.Length - 1)
            {
#if PLAYSCHOOL_MAIN
                EffectParticleControll.Instance.SpawnGameEndPanel();
                GameOverEndPanel.Instance.AddTheListnerRetryGame();
#else
                //Your testing End panel
                Debug.Log("GameOVer");
                yield break;
#endif                
            }
            StartLevel();
        }
        public void SaveFailedAttempt() => updateCategoryApiManager.SetAttemps();
        public void SaveLevel()
        {
            currentLevelIndex++;//incrementing level here
            gameCategoryDataManager.SaveLevel(currentLevelIndex, levelSO.levelData.Length);
            SendStars();
        }
        private void SendStars()
        {
            int star = gameCategoryDataManager.Getstar;
            if (star >= 5)
            {
                updateCategoryApiManager.SetGameDataMore(currentLevelIndex, levelSO.levelData.Length, star);
            }
            else
            {
                updateCategoryApiManager.SetGameDataMore(currentLevelIndex, levelSO.levelData.Length, star);
            }
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
