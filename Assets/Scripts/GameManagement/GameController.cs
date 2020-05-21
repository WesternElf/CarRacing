using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using ScriptableObjects;
using UnityEngine;

    public enum GameState                                           //ігровий стан - пауза і гра
    {
        Play,
        Pause
    }

    public class GameController : MonoBehaviour
    {
        private static GameController _instance;
        private GameState _state;
        private Player player;

        public static GameController Instance                       //синглтон
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

        public GameState State                                      //якщо гра на паузі, то скейл часу = 0, якщо ні, то 1
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

    }

