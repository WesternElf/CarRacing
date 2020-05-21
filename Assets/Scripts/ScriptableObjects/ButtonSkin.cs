using ScriptableObjects;
using Extensions;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSkin : MonoBehaviour
{
    private Button _button;
    private PlayerSkin playerSkin;

    private void Awake()
    {
        _button = gameObject.GetComponent<Button>();
        gameObject.RemoveCloneFromName();
        _button.onClick.AddListener(ChoseSkin);
    }

    private void ChoseSkin()
    {
        var text = gameObject.GetComponentInChildren<Text>();
        playerSkin = Resources.Load<PlayerSkin>($"CarSkins/{text.text}");

        Debug.Log("Click! "+ playerSkin);
        PlayerData playerData = new PlayerData();
        playerData.SkinName = playerSkin.Name;

        Debug.Log(playerData.SkinName);

        GameController.Instance.State = GameState.Play;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ChoseSkin);
    }
}
