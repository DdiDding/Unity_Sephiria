using GameFramework.Resource;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

public class LoadMap : MonoBehaviour
{
    public bool IsLoadComplete { get; private set; } = false;

    public void LoadStage()
    {
        IsLoadComplete = false;

        string path = "Assets/Resources/Rooms/DeepCave_Combat_00.txt";
    

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

                Debug.Log($"Map '{assetName}' loaded in {duration} seconds");
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

    public static class MapLoader
    {
        public static int[,] Parse(string text)
        {
            var lines = text.Split('\n');
            int rows = lines.Length;
            int cols = lines[0].Trim().Length;

            int[,] map = new int[rows, cols];

            for (int y = 0; y < rows; y++)
            {
                var line = lines[y].Trim();
                for (int x = 0; x < cols; x++)
                {
                    map[y, x] = line[x] - '0'; // '0' → 0, '1' → 1 ...
                }
            }
            return map;
        }
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
