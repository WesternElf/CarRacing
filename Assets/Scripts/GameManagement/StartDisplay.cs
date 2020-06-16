using System;
using PlayerScripts;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class StartDisplay : MonoBehaviour
{
    public static event Action OnStartDisplayEvent;
    [SerializeField] private Button newButton;
    [SerializeField] private RectTransform buttonPanel;

    public PlayerData PlayerData { get; set; }

    private void OnEnable()
    {
        InstantiateButton();

        ButtonSkin.OnSkinChoosedEvent += LevelStarting;

    }

    private void InstantiateButton()
    {
        var playerSkins = Resources.LoadAll<PlayerSkin>("CarSkins");

        for (int i = 0; i < playerSkins.Length; i++)
        {
            GetSkinButton(playerSkins[i]);

        }
    }

    private void GetSkinButton(PlayerSkin skin)
    {
        var button = Instantiate(newButton, buttonPanel.transform.position, buttonPanel.transform.rotation) as Button;
        var text = button.GetComponentInChildren<Text>();
        text.text = skin.Name;
        button.name = skin.Name;
        button.transform.SetParent(buttonPanel.transform);
    }

    private void LevelStarting()
    {

        OnStartDisplayEvent?.Invoke();
    }

    private void OnDestroy()
    {
        ButtonSkin.OnSkinChoosedEvent -= LevelStarting;
        ChoosedScreen.OnChoosedScreenEvent -= InstantiateButton;

    }

}
