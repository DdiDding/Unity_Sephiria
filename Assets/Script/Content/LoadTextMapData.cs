using GameFramework.Resource;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

namespace Game.Map
{
    public struct MapData
    {
        public bool SpawnMonster;
        public uint monsterDensity;
        public Vector2 teleportPoint;
        public uint type;
        public string passages;
        public int[,] ground;
        public int[,] upperGround;
        public int[,] wall;
        public int[,] cliff;
    }

    /**
	 * @class LoadTextMapData
	 * @brief 텍스트 맵 파일을 파싱하여 각 데이터를 저장
	 */
    public class LoadTextMapData
    {
        /**
         * @function LoadTextMap
         * @brief 텍스트 파일을 불러와 파싱한 값을 반환
         * @param path 불러올 텍스트 파일 경로
         * @return 파싱한 값을 구조체로 반환
         */
        public void LoadTextMap(string path, Action<MapData> onLoaded)
        {
            // ResourceComponent 가져오기
            ResourceComponent resource;
            {
                resource = GameEntry.GetComponent<ResourceComponent>();
                if (resource == null) return;
            }

            // 비동기 로딩
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
    
                    // 불러온 텍스트 파일을 파싱
                    MapData mapData = ParseMap(txt);

                    // 필요한 타일 넘버 저장
                    HashSet<int> needTileID = new HashSet<int>();


                    // 콜백 함수가 있으면 mapData를 매개변수로 호출하고, 없으면 생략하는 의미의 코드
                    onLoaded?.Invoke(mapData);
                },
                // 실패 콜백
                (assetName, status, errorMessage, userData) =>
                {
                    Debug.LogError($"Failed to load asset '{assetName}': {errorMessage}");
                }
            ));
        }

        private MapData ParseMap(TextAsset textAsset)
        {
            MapData mapData = new MapData();
    
            // 텍스트 파일 파싱
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);
    
            // Room node
            XmlNode roomNode = xmlDoc.SelectSingleNode("//room");
            mapData.SpawnMonster = bool.Parse(roomNode.Attributes["spawnMonster"].Value);
    
            // Main Layer
            mapData.ground = ParseInt2D(xmlDoc.SelectSingleNode("//ground").InnerText);

            // upperGround layer
            mapData.upperGround = ParseInt2D(xmlDoc.SelectSingleNode("//upperGround").InnerText);

            // wall layer
            mapData.wall = ParseInt2D(xmlDoc.SelectSingleNode("//wall").InnerText);

            // cliff layer
            mapData.cliff = ParseInt2D(xmlDoc.SelectSingleNode("//cliff").InnerText);

            return mapData;
        }

        private int[,] ParseInt2D(string text)
        {
            string[] lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int height = lines.Length;
            int width = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;

            int[,] result = new int[height, width];

            for (int y = 0; y < height; y++)
            {
                string[] tokens = lines[y].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int x = 0; x < width; x++)
                {
                    string token = tokens[x];
                    if (token == "X")
                        result[y, x] = -1; // 블록 없음
                    else
                        result[y, x] = int.Parse(token); // 숫자는 그대로
                }
            }

            return result;
        }

        // 파싱된 데이터를 읽고 필요한 타일의 id를 저장하는 함수
        private void SaveNeedTileID(ref MapData mapData,ref HashSet<int> needed)
        {
            // ground Check
            foreach (var x in mapData.ground)
            {
                needed.Add(x);
            }
        }
    }
}