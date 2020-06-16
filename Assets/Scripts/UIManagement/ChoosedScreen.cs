using System;
using UnityEngine;
using UnityEngine.Events;

public class ChoosedScreen : BaseScreen
{
    public static Action OnChoosedScreenEvent;

    public void OnEnable()
    {
        OnChoosedScreenEvent?.Invoke();
        ScreenController.OnScreenEnabled += SentMessage;
        StartDisplay.OnStartDisplayEvent += DestroyScreen;
    }

    private void SentMessage()
    {
        OnChoosedScreenEvent?.Invoke();
    }

    private void DestroyScreen()
    {
        Debug.Log("Skin choosed. Close start display.");
        OnChoosedScreenEvent?.Invoke();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        ScreenController.OnScreenEnabled -= SentMessage;
        StartDisplay.OnStartDisplayEvent += DestroyScreen;
    }
}
