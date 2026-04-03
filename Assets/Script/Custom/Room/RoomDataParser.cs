using Game.Map;
using GameFramework.Resource;
using System;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityGameFramework.Runtime;
using TextAsset = UnityEngine.TextAsset;

// ÆÄ½Ì °á°ú µ¥ÀÌÅÍ ±¸Á¶Ã¼
public struct RoomData
{
    public bool SpawnMonster;
    public int monsterDensity;
    public Vector2 teleportPoint;
    public uint type;
    public string passages;

    public int[,] ground;
    public int[,] upperGround;
    public int[,] wall;
    public int[,] cliff;
}

public static class RoomDataParser
{
    // --------------------------------------------
    // Public functions
    // --------------------------------------------

    public static void LoadTextFile(string path, Action<RoomData> onLoaded)
    {
        ResourceComponent resource = GameEntry.GetComponent<ResourceComponent>();
        if (resource == null)
        {
            Debug.LogError("ResourceComponent not found.");
            return;
        }

        resource.LoadAsset(path, new LoadAssetCallbacks(
            // ¼º°ø
            (assetName, asset, duration, userData) =>
            {
                TextAsset txt = asset as TextAsset;
                if (txt == null)
                {
                    Debug.LogError($"Asset '{assetName}' is not TextAsset.");
                    return;
                }

                RoomData roomData = parse(txt);
                onLoaded?.Invoke(roomData);
            },
            // ½ÇÆÐ
            (assetName, status, errorMessage, userData) =>
            {
                Debug.LogError($"Failed to load asset '{assetName}': {errorMessage}");
            }
        ));
    }

    // --------------------------------------------
    // Private functions
    // --------------------------------------------

    private static RoomData parse(TextAsset roomData)
    {
        RoomData result = new RoomData();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(roomData.text);

        // Attribute ÆÄ½Ì
        XmlNode xmlNode = xmlDoc.SelectSingleNode("//room");
        result.SpawnMonster = bool.Parse(xmlNode.Attributes["spawnMonster"].Value);
        result.monsterDensity = int.Parse(xmlNode.Attributes["monsterDensity"].Value);
        // TODO : teleportPoint

        result.ground = parseTileArray(xmlDoc.SelectSingleNode("//ground").InnerText);
        result.upperGround = parseTileArray(xmlDoc.SelectSingleNode("//upperGround").InnerText);
        result.wall = parseTileArray(xmlDoc.SelectSingleNode("//wall").InnerText);
        result.cliff = parseTileArray(xmlDoc.SelectSingleNode("//cliff").InnerText);

        return result;
    }


    private static int[,] parseTileArray(string text)
    {
        string[] lines = text.Split(
            new[] { '\n', '\r' },
            StringSplitOptions.RemoveEmptyEntries);

        int height = lines.Length;
        int width = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;

        int[,] result = new int[height, width];

        for (int y = 0; y < height; y++)
        {
            string[] tokens = lines[y].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int x = 0; x < width; x++)
            {
                result[y, x] = tokens[x] == "X"
                    ? -1        // ºó Ä­
                    : int.Parse(tokens[x]);
            }
        }

        return result;
    }
}
