using GameFramework.Resource;
using System;
using System.Xml;
using UnityEditor;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

public struct MapData
{
    public bool SpawnMonster;
    public uint monsterDensity;
    public Vector2 teleportPoint;
    public uint type;
    public string passages;
    public string[] ground;
    public string[] upperGround;
    public string[] wall;
    public string[] cliff;
}

public class LoadTextMapData
{
    public void LoadTextMap(ref MapData mapData)
    {
        // 가져올 경로
        string path = "Assets/Resources/Rooms/Test_Moleland.txt";

        // ResourceComponent 가져오기
        ResourceComponent resource;
        {
            resource = GameEntry.GetComponent<ResourceComponent>();
            if (resource == null) return;
        }

        // 비동기 로딩임 !!
        resource.LoadAsset(path, new LoadAssetCallbacks(
            // 성공 콜백
            (assetName, asset, duration, userData) =>
            {
                TextAsset txt = asset as TextAsset;
                if (txt == null)
                {
                    Debug.LogError($"Asset '{assetName}' is not TextAsset!");
                    return;
                }

                string[] entries = txt.text.Split(new char[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries);

                // 불러온 텍스트 파일을 파싱
                mapData = ParseMap(txt);
            },
            // 실패 콜백
            (assetName, status, errorMessage, userData) =>
            {
                Debug.LogError($"Failed to load asset '{assetName}': {errorMessage}");
            }
        ));
    }


    MapData ParseMap(TextAsset textAsset)
    {
        MapData mapData = new MapData();

        // 텍스트 파일 파싱
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        // Room node
        XmlNode roomNode = xmlDoc.SelectSingleNode("//room");
        mapData.SpawnMonster = bool.Parse(roomNode.Attributes["spawnMonster"].Value);

        // Main Layer
        XmlNode groundNode = xmlDoc.SelectSingleNode("//ground");
        mapData.ground = groundNode.InnerText.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        XmlNode upperGroundNode = xmlDoc.SelectSingleNode("//upperGround");
        mapData.upperGround = upperGroundNode.InnerText.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        XmlNode wallNode = xmlDoc.SelectSingleNode("//wall");
        mapData.wall = wallNode.InnerText.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        XmlNode cliffNode = xmlDoc.SelectSingleNode("//wall");
        mapData.cliff = cliffNode.InnerText.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        return mapData;
    }


}