using System;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    public static Action OnScreenEnabled;
    [SerializeField] private GameObject _screenPrefab;

    private void Awake()
    {
        OpenScreenPrefab();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChoosedScreen.OnChoosedScreenEvent += PlayTheGame;
    }

    private void PlayTheGame()
    {
        GameController.Instance.State = GameState.Play;
        GameController.Instance.InstantiatePlayer();
    }

    private void OpenScreenPrefab()
    {
        Instantiate(_screenPrefab, transform.parent);
    }

    private void OnDisable()
    {
        ChoosedScreen.OnChoosedScreenEvent += PlayTheGame;
    }

}
