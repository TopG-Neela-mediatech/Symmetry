using System;
using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class GameManager : MonoBehaviour
    {
        //[SerializeField] private UIManager uiManager;
        [SerializeField] private LevelManager levelManager;
        //[SerializeField] private LivesManager livesManager;
        //[SerializeField] private SoundManager soundManager;       
        private static GameManager instance;


        public static GameManager Instance { get { return instance; } }
        public LevelManager LevelManager { get { return levelManager; } }
        //public UIManager UIManager { get { return uiManager; } }
        //public LivesManager LivesManager { get { return livesManager; } }
        //public SoundManager SoundManager { get { return soundManager; } }


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
