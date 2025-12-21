using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class TileAtlasDebugVisualizer : EditorWindow
{
    [MenuItem("Tools/Tile Atlas Debug Visualizer")]
    public static void OpenWindow()
    {
        GetWindow<TileAtlasDebugVisualizer>("Tile Atlas Debug");
    }

    public Texture2D atlasTexture;
    public TextAsset jsonFile;

    private GameObject parentGO;

    private void OnGUI()
    {
        GUILayout.Label("Tile Atlas Debug Visualizer", EditorStyles.boldLabel);

        atlasTexture = (Texture2D)EditorGUILayout.ObjectField(
            "Atlas Texture", atlasTexture, typeof(Texture2D), false);

        jsonFile = (TextAsset)EditorGUILayout.ObjectField(
            "JSON File", jsonFile, typeof(TextAsset), false);

        GUI.enabled = atlasTexture != null && jsonFile != null;

        if (GUILayout.Button("Visualize Tiles in Scene"))
        {
            VisualizeTiles();
        }

        if (GUILayout.Button("Clear Visualization"))
        {
            ClearVisualization();
        }

        GUI.enabled = true;
    }

    void VisualizeTiles()
    {
        if (atlasTexture == null || jsonFile == null) return;

        ClearVisualization();

        parentGO = new GameObject("TileAtlas_Debug");

        // JSON 파싱
        var root = Newtonsoft.Json.Linq.JObject.Parse(jsonFile.text);
        var renderDataMap = root["m_RenderDataMap"] as Newtonsoft.Json.Linq.JArray;
        var spriteNamesArray = root["m_PackedSpriteNamesToIndex"] as Newtonsoft.Json.Linq.JArray;
        List<string> spriteNames = new List<string>();
        if (spriteNamesArray != null)
        {
            foreach (var n in spriteNamesArray)
                spriteNames.Add(n.ToString());
        }

        int atlasHeight = atlasTexture.height;

        for (int i = 0; i < renderDataMap.Count; i++)
        {
            var tileData = renderDataMap[i] as Newtonsoft.Json.Linq.JObject;
            if (tileData == null) continue;

            var value = tileData["Value"] as Newtonsoft.Json.Linq.JObject;
            if (value == null) continue;

            var rect = value["m_TextureRect"] as Newtonsoft.Json.Linq.JObject;
            if (rect == null) continue;

            int x = rect["m_X"].Value<int>();
            int y = rect["m_Y"].Value<int>();
            int w = rect["m_Width"].Value<int>();
            int h = rect["m_Height"].Value<int>();

            // offset 적용
            var offset = value["m_TextureRectOffset"] as Newtonsoft.Json.Linq.JObject;
            int offsetX = offset?["m_X"]?.Value<int>() ?? 0;
            int offsetY = offset?["m_Y"]?.Value<int>() ?? 0;

            Rect spriteRect = new Rect(
                x + offsetX,
                atlasHeight - y - h + offsetY,
                w,
                h
            );

            Vector2 pivot = new Vector2(0f, 1f); // 좌상단 기준
            Sprite s = Sprite.Create(atlasTexture, spriteRect, pivot, 100f);

            string name = (i < spriteNames.Count) ? spriteNames[i] : $"Tile_{i}";

            // Scene에 배치
            GameObject go = new GameObject(name);
            go.transform.parent = parentGO.transform;

            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = s;

            // 격자처럼 배치
            go.transform.position = new Vector3((i % 10) * (w / 100f), -(i / 10) * (h / 100f), 0);
        }

        Debug.Log("<color=green>Tile visualization completed. Check Scene view!</color>");
    }

    void ClearVisualization()
    {
        if (parentGO != null)
        {
            DestroyImmediate(parentGO);
            parentGO = null;
        }
    }
}
