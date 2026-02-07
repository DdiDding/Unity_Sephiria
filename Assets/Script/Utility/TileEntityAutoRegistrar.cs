#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

/**
 * @class TileEntityAutoRegistrar
 * @briff TileEntityRegistry에 TileEntity를 자동으로 등록하는 툴
 */
public static class TileEntityAutoRegistrar
{
    [MenuItem("Tools/TileEntity/Auto Populate Container")]
    public static void ShowWindow()
    {
        tileEntityAutoRegistrarWindow.ShowWindow();
    }

    /**
     * @class TileEntityAutoRegistrarWindow 
     * @briff 툴 창을 열기 위한 클래스
     */
    private class tileEntityAutoRegistrarWindow : EditorWindow
    {
        private TileEntityRegistry targetContainer;

        public static void ShowWindow()
        {
            var window = GetWindow<tileEntityAutoRegistrarWindow>("Auto Populate Tiles");
            window.minSize = new Vector2(300, 120);

            window.position = new Rect(800, 300, 350, 150);
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("채울 Registry 선택", EditorStyles.boldLabel);

            targetContainer = (TileEntityRegistry)EditorGUILayout.ObjectField(
                "Target Container",
                targetContainer,
                typeof(TileEntityRegistry),
                false);

            EditorGUILayout.Space();

            if (GUILayout.Button("Populate"))
            {
                if (targetContainer != null)
                    Populate(targetContainer);
                else
                    EditorUtility.DisplayDialog("Error", "TileEntityContainer를 선택하세요.", "OK");
            }
        }
    }

    /**
     * @briff TileEntity를 등록하는 로직
     */
    public static void Populate(TileEntityRegistry registry)
    {
        // Regist GroundTileEntity
        // 각 entity의 guid를 가져온다.
        string[] guids = AssetDatabase.FindAssets("t:GroundTileEntity", new[] { "Assets/Resources/Tiles/TileEntities/GroundTiles" });

        for (int i = 0; i < guids.Length; i++)
        {
            // guid 이용해 경로 가져오기
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            // 등록
            registry.grounds.Add(i, AssetDatabase.LoadAssetAtPath<GroundTileEntity>(path));
        }
    }
}
#endif
