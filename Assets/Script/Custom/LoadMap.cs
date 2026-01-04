using GameFramework.Resource;
using System;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

public class LoadMap : MonoBehaviour
{
    struct MapData
    {
        public bool SpawnMonster;
        public uint monsterDensity;
        public Vector2 teleportPoint;
        public uint type;
        public string passages;
        public string[] ground;
        public string[] upperGround;
    }
    MapData mapData;

    void ParseMap(TextAsset textAsset)
    {
        // 텍스트 파일 파싱
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        // Room node
        XmlNode roomNode = xmlDoc.SelectSingleNode("//room");
        mapData.SpawnMonster = bool.Parse(roomNode.Attributes["spawnMonster"].Value);
        Debug.Log($"SpawnMonster: {mapData.SpawnMonster}");

        // Main Layer
        XmlNode groundNode = xmlDoc.SelectSingleNode("//ground");
        string gtile = groundNode.InnerText;

        string[] gtileSplit = gtile.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        int a = 3;
        //XmlNode upperNode = xmlDoc.SelectSingleNode("//upperGround");
        //Debug.Log($"Upper size: {upperTiles.GetLength(0)}x{upperTiles.GetLength(1)}");
    }

    public void LoadStage()
    {

        // 가져올 경로
        string path = "Assets/Resources/Rooms/Test_Moleland.txt";

        // ResourceComponent 가져오기
        ResourceComponent resource;
        {
            resource = GameEntry.GetComponent<ResourceComponent>();
            if (resource == null)
            {
                int a = 3;
                return;
            }
        }


        // LoadAsset의 콜백 정의
        LoadAssetCallbacks callbacks = new LoadAssetCallbacks(
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
                ParseMap(txt);
            },
            // 실패 콜백
            (assetName, status, errorMessage, userData) =>
            {
                Debug.LogError($"Failed to load asset '{assetName}': {errorMessage}");
            }
        );

        // 리소스 로드
        resource.LoadAsset(path, callbacks);
    }

    bool once = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        once = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (once == true)
        {
            LoadStage();
            once = false;
        }
    }
}