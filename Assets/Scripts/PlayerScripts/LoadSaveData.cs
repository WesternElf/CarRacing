using System;
using FullSerializer;
using System.IO;
using UnityEngine;

namespace PlayerScripts
{
    public class LoadSaveData : MonoBehaviour
    {
        private static readonly fsSerializer _serializer = new fsSerializer();

        readonly PlayerData _playerData = new PlayerData(12f, 100f, Color.red);


        private void Awake()
        {
            LoadPlayer();
        }


        [ContextMenu("Save")]
        public string SavePlayer()
        {
            var json = Serialize(typeof(PlayerData), _playerData);
            File.WriteAllText(GetFilePath(), json);
            return json;
        }


        public void LoadPlayer()
        {
            var data = Deserialize(typeof(PlayerData), SavePlayer()) as PlayerData;
            PlayerData.NewSpeedValue = data.Speed;
            PlayerData.NewFuelValue = data.FuelCount;
            PlayerData.NewMaterial = data.CarMaterial;
        }


        private string GetFilePath()
        {
            var path = Application.dataPath + "/Savings.json";
            return path;
        }

        public static string Serialize(Type type, object value)
        {
            _serializer.TrySerialize(type, value, out var data).AssertSuccessWithoutWarnings();

            return fsJsonPrinter.CompressedJson(data);
        }

        public static object Deserialize(Type type, string serializedState)
        {
            var data = fsJsonParser.Parse(serializedState);
            object deserialized = null;
            _serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();

            return deserialized;
        }
        
    }

}

