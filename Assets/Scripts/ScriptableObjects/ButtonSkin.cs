using ScriptableObjects;
using Extensions;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonSkin : MonoBehaviour
{
    public static event Action OnSkinChoosedEvent;
    public static PlayerSkin NewSkin;
    private Button _button;

    private void Awake()
    {
        _button = gameObject.GetComponent<Button>();

        gameObject.RemoveCloneFromName();
        _button.onClick.AddListener(ChoseSkin);
    }

    private void ChoseSkin()
    {
        NewSkin = Resources.Load<PlayerSkin>($"CarSkins/{gameObject.name}");

        OnSkinChoosedEvent?.Invoke();
    }

}
