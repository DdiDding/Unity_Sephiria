using GameFramework.Resource;
using System;
using System.Xml;
using UnityEditor;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

namespace Game.Map.IO
{
    
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

    /**
	 * @class LoadTextMapData
	 * @brief 텍스트 맵 파일을 파싱하여 각 데이터를 저장
	 */
    public class LoadTextMapData
    {
        public event Action OnMapLoaded;
        public void Load()
        {
            OnMapLoaded?.Invoke();
        }

        /**
         * @function LoadTextMap
         * @brief 텍스트 파일을 불러와 파싱한 값을 반환
         * @param path 불러올 텍스트 파일 경로
         * @return 파싱한 값을 구조체로 반환
         */
        public void LoadTextMap(string path)
        {
            // 가져올 경로 임시로ㅇㅇ
            string tempPath = "Assets/Resources/Rooms/Test_Moleland.txt";
    

            // ResourceComponent 가져오기
            ResourceComponent resource;
            {
                resource = GameEntry.GetComponent<ResourceComponent>();
                if (resource == null) return;
            }
    

            // 비동기 로딩임
            resource.LoadAsset(tempPath, new LoadAssetCallbacks(
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
                    MapData mapData = ParseMap(txt);

                    // 여기서 다른 함수로 넘기기
                    EventComponent eventComponent = GameEntry.GetComponent<EventComponent>();
                    eventComponent.Fire(this, MapLoadedEventArgs.Create(mapData));

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
}