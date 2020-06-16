using System;
using Extensions;
using PlayerScripts;
using UnityEngine;

    public enum GameState                                          
    {
        Play,
        Pause
    }

    public class GameController : MonoBehaviour
    {
        public Action OnScreenEventCalled;
        private static GameController _instance;
        private GameState _state;


        public static GameController Instance                  
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameController>();
                }

                if (_instance == null)
                {
                    _instance = new GameObject("GameController", typeof(GameController)).GetComponent<GameController>();
                }

                return _instance;
            }
        }

        public GameState State                                     
        {
            get { return _state; }
            set
            {
                if (value == GameState.Play)
                {
                    Time.timeScale = 1.0f;
                }
                else
                {
                    Time.timeScale = 0.0f;
                }

                _state = value;
            }
        }

        private void Awake()
        {
            State = GameState.Pause;
        }

        public void InstantiatePlayer()
        {
        var newPlayer = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
        newPlayer.RemoveCloneFromName();
        }

}

