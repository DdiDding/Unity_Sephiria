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
    MapData testMapp;

    void ParseMap(TextAsset textAsset)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNode roomNode = xmlDoc.SelectSingleNode("//room");
        testMapp.SpawnMonster = bool.Parse(roomNode.Attributes["spawnMonster"].Value);
        Debug.Log($"SpawnMonster: {testMapp.SpawnMonster}");

        XmlNode groundNode = xmlDoc.SelectSingleNode("//ground");
        string gtile = groundNode.InnerText;
        Debug.Log($"Ground Data \n {gtile}");

        //XmlNode upperNode = xmlDoc.SelectSingleNode("//upperGround");
        //Debug.Log($"Upper size: {upperTiles.GetLength(0)}x{upperTiles.GetLength(1)}");
    }

    public void LoadStage()
    {

        string path = "Assets/Resources/Rooms/Test_Moleland.txt";


        // ResourceComponent 가져오기
        var resource = GameEntry.GetComponent<ResourceComponent>();
        if (resource == null)
        {
            int a = 3;
        }

        // 콜백 정의
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


                // 임시 Debug 코드
                {
                    ParseMap(txt);

                    // 모든 줄 가져오기
                    string[] lines = txt.text.Split('\n');

                    // 최대 3줄까지 처리
                    int rowCount = Mathf.Min(3, lines.Length);

                    // 각 줄을 char 배열로 변환하여 2D 배열에 저장
                    char[][] mapRows = new char[rowCount][];
                    for (int y = 0; y < rowCount; y++)
                    {
                        mapRows[y] = lines[y].Trim().ToCharArray();
                    }

                    // 확인용 출력
                    for (int y = 0; y < rowCount; y++)
                    {
                        string lineStr = new string(mapRows[y]);
                        Debug.Log($"Row {y}: {lineStr}");
                    }
                }
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