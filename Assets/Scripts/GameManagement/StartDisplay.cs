using PlayerScripts;
using ScriptableObjects;
using UnityEngine;

public class StartDisplay : MonoBehaviour
{
    private PlayerSkin playerSkin;
    private PlayerData playerData;

    public PlayerData PlayerData { get; set; }

    private void Awake()
    {
        GameController.Instance.State = GameState.Pause;
       
    }

    public void ChooseCar(PlayerSkin skin)                                          //метод для вибору скіна через он клік івент
    {
        playerSkin = Resources.Load<PlayerSkin>($"CarSkins/{skin.name}");
        Debug.Log(playerSkin.name + "  " + playerSkin.Speed);

        PlayerData = new PlayerData();
        PlayerData.Speed = playerSkin.Speed;
        playerData.FuelCount = playerSkin.FuelCount;

        GameController.Instance.State = GameState.Play;
        gameObject.SetActive(false);
        
    }



}
