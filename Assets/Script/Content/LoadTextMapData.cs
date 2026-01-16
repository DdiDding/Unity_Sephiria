using GameFramework.Resource;
using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Map
{
    // 파싱 결과 데이터
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
     * @brief 텍스트 맵 파일을 로드하고 파싱하여 MapData를 생성
     */
    public class LoadTextMapData
    {
        /**
         * @brief 텍스트 맵 로드 및 파싱
         * @param path 리소스 경로
         * @param onLoaded MapData와 필요한 타일 ID 집합 콜백
         */
        public void LoadTextMap(
            string path,
            Action<MapData, HashSet<int>> onLoaded)
        {
            ResourceComponent resource = GameEntry.GetComponent<ResourceComponent>();
            if (resource == null)
            {
                Debug.LogError("ResourceComponent not found.");
                return;
            }

            resource.LoadAsset(path, new LoadAssetCallbacks(
                // 성공
                (assetName, asset, duration, userData) =>
                {
                    TextAsset txt = asset as TextAsset;
                    if (txt == null)
                    {
                        Debug.LogError($"Asset '{assetName}' is not TextAsset.");
                        return;
                    }

                    MapData mapData = ParseMap(txt);

                    // 필요한 타일 ID 계산
                    HashSet<int> neededTileIds = CollectNeededTileIds(mapData);

                    onLoaded?.Invoke(mapData, neededTileIds);
                },
                // 실패
                (assetName, status, errorMessage, userData) =>
                {
                    Debug.LogError($"Failed to load asset '{assetName}': {errorMessage}");
                }
            ));
        }

        // -------------------------------
        // Parsing
        // -------------------------------

        private MapData ParseMap(TextAsset textAsset)
        {
            MapData mapData = new MapData();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);

            XmlNode roomNode = xmlDoc.SelectSingleNode("//room");
            mapData.SpawnMonster = bool.Parse(roomNode.Attributes["spawnMonster"].Value);

            mapData.ground = ParseInt2D(xmlDoc.SelectSingleNode("//ground").InnerText);
            mapData.upperGround = ParseInt2D(xmlDoc.SelectSingleNode("//upperGround").InnerText);
            mapData.wall = ParseInt2D(xmlDoc.SelectSingleNode("//wall").InnerText);
            mapData.cliff = ParseInt2D(xmlDoc.SelectSingleNode("//cliff").InnerText);

            return mapData;
        }

        private int[,] ParseInt2D(string text)
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
                        ? -1        // 빈 칸
                        : int.Parse(tokens[x]);
                }
            }

            return result;
        }

        // -------------------------------
        // Needed Tile ID Collection
        // -------------------------------

        private HashSet<int> CollectNeededTileIds(MapData mapData)
        {
            HashSet<int> needed = new HashSet<int>();

            CollectFromLayer(mapData.ground, needed);
            CollectFromLayer(mapData.upperGround, needed);
            CollectFromLayer(mapData.wall, needed);
            CollectFromLayer(mapData.cliff, needed);

            return needed;
        }

        private void CollectFromLayer(int[,] layer, HashSet<int> needed)
        {
            if (layer == null)
                return;

            foreach (int tileId in layer)
            {
                if (tileId >= 0) // -1은 빈 칸
                {
                    needed.Add(tileId);
                }
            }
        }
    }
}