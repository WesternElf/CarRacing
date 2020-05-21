using PlayerScripts;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class StartDisplay : MonoBehaviour
{
    [SerializeField] private Button newButton;
    [SerializeField] private RectTransform buttonPanel;

    public PlayerData PlayerData { get; set; }

    private void Awake()
    {
        GameController.Instance.State = GameState.Pause;

        var playerSkins = Resources.LoadAll<PlayerSkin>("CarSkins");

        Debug.Log("Objects: " + playerSkins.Length);
        for (int i = 0; i < playerSkins.Length; i++)
        {
            InstantiateButton(playerSkins[i]);
            
            Debug.Log("Succes");
        }
        Debug.Log("Instantiate succesfully");
    }

    private void InstantiateButton(PlayerSkin skin)
    {
        var button = Instantiate(newButton, buttonPanel.transform.position, buttonPanel.transform.rotation) as Button;
        var text = button.GetComponentInChildren<Text>();
        text.text = skin.Name;
        button.transform.SetParent(buttonPanel.transform);
    }

}
