using System;
using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private AnimationController animController;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private StartInfoScript startInfoScript;
        [SerializeField] private LivesManager livesmanager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private HandTutorialManager handTutorialManager;
        [SerializeField] private SoundManager soundManager;       
        private static GameManager instance;


        public static GameManager Instance { get { return instance; } }
        public LevelManager LevelManager { get { return levelManager; } }
        public AnimationController AnimationController { get { return animController; } }
        public StartInfoScript StartInfoScript { get { return startInfoScript; } }
        public LivesManager Livesmanager { get { return livesmanager; } }
        public UIManager UIManager { get { return uiManager; } }
        public HandTutorialManager HandTutorialManager { get { return handTutorialManager; } }
        public SoundManager SoundManager { get { return soundManager; } }


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(instance);
            }
        }


        #region Events
        public event Action OnLevelWin;
        public event Action OnLevelLose;
        public event Action OnLevelStart;


        public void InvokeLevelStart() => OnLevelStart?.Invoke();
        public void InvokeLevelWin() => OnLevelWin?.Invoke();
        public void InvokeLevelLose() => OnLevelLose?.Invoke();
        #endregion
    }
}
