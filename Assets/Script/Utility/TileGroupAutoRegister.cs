#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using GameFramework.Sound;
using TreeEditor;
using System.Text.RegularExpressions;

/**
 * @class TileGroupAutoRegister
 * @briff TileEntityRegistry에 TileEntity를 자동으로 등록하는 툴
 */
public static class TileGroupAutoRegister
{
    [MenuItem("Tools/Tile/TileGroup Auto Register")]
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
        private TileGroupType tileType;
        private GroundTileGroup groundGroup;
        private WallTileGroup wallGroup;
        private TileGroupBase tileGroup;
        //private CliffTileGroup cliffGroups;

        public static void ShowWindow()
        {
            var window = GetWindow<tileEntityAutoRegistrarWindow>("TileGroup Auto Register");
            window.minSize = new Vector2(300, 120);
            window.position = new Rect(800, 300, 350, 150);
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("채울 Registry 선택", EditorStyles.boldLabel);

            // 설정하려는 Tile entity 종류 선택
            tileType = (TileGroupType)EditorGUILayout.EnumPopup("Tile Tpye", tileType);


            tileGroup = (TileGroupBase)EditorGUILayout.ObjectField(
                        "Target Tile Group",
                        tileGroup,
                        typeof(TileGroupBase),
                        false);

            EditorGUILayout.Space(15);

            if (GUILayout.Button("Populate"))
            {
                if (tileGroup != null)
                {
                    Populate(tileGroup, tileType);
                    EditorUtility.DisplayDialog("Success", "작업을 수행했습니다.", "OK");
                }
                else
                    EditorUtility.DisplayDialog("Error", "TileEntityContainer를 선택하세요.", "OK");
            }
        }
    }

    /**
     * @briff TileEntity를 등록하는 로직
     */
    public static void Populate(TileGroupBase tileGroup, TileGroupType tileType)
    {
        tileGroup.Clear();

        // TileType에 맞는 폴더 경로 가져오기
        string folder = GetFolder(tileType);

        // 해당 폴더에서 TileEntityBase 검색하여 guid 가져오기
        string[] guids = AssetDatabase.FindAssets("t:TileEntityBase", new[] { folder });

        // TODO : index기반일시 다시 실행하기
        // 그룹 사이즈 설정
        //tileGroup.SetSize(guids.Length);

        // 검색된 모든 Asset을 순회
        foreach (var guid in guids)
        {
            // GUID → 실제 Asset 경로로 변환
            // 예: Assets/Tiles/TileEntities/Ground/0-Cave.asset
            string path = AssetDatabase.GUIDToAssetPath(guid);

            // 경로에서 Asset을 로드
            // type이 GroundTileEntity라면 해당 타입으로 로드된다.
            var asset = AssetDatabase.LoadAssetAtPath<TileEntityBase>(path);

            //경로에서 파일 이름만 추출(확장자 제거)
            //예: "0-Cave"
            string fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            
            // '-' 기준으로 문자열 분리
            //["0", "Cave"]
            string[] parts = fileName.Split('_');
            
            //첫 번째 요소를 ID로 변환
            // "0" → 0
            int id = int.Parse(parts[0]);
            
            tileGroup.Add(asset);
            //ID를 List index로 사용하여 TileGroup에 저장
            // tiles[id] = asset
            //tileGroup.Add(id, asset);
        }


    }

    /**
     * @briff 설정한 TileType에 따라 폴더를 자동으로 매핑해주는 함수
     */
    private static string GetFolder(TileGroupType type)
    {
        switch (type)
        {
            case TileGroupType.Ground:
                return "Assets/ScriptableObjects/Tiles/TileEntity/Ground";
            case TileGroupType.Wall:
                return "Assets/ScriptableObjects/Tiles/TileEntity/WallRoof";
        }

        return "";
    }
}
#endif