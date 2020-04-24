using System;
using FullSerializer;
using System.IO;
using UnityEngine;

namespace PlayerScripts
{
    public static class LoadSaveData
    {
        private static readonly fsSerializer _serializer = new fsSerializer();

        [ContextMenu("Save")]
        public static void SavePlayer(PlayerData playerData)
        {
            var json = Serialize(typeof(PlayerData), playerData);
            File.WriteAllText(GetFilePath(), json);
        }

        public static PlayerData LoadPlayer()
        {
            var pathToSaveFile = GetFilePath();

            if (!File.Exists(pathToSaveFile))
            {
                return null;
            }

            var fileContent = File.ReadAllText(pathToSaveFile);
            return Deserialize(typeof(PlayerData), fileContent) as PlayerData;
        }

        private static string GetFilePath()
        {
            var path = Application.persistentDataPath + "/Savings.json";
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

