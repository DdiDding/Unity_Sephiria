using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class LoadResourceProcedure : ProcedureBase
{
    public TileProvider mapData = new TileProvider();
    //public LoadTextMapData loadTextMapData = new LoadTextMapData();
    protected override void OnInit(ProcedureOwner procedureOwner)
    {
        base.OnInit(procedureOwner);

        // 모든 타일 불러오기
        mapData.LoadTiles();
    }

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        // 텍스트 맵 데이터 가져와 불러오기
        //MapData mapData = new MapData();
        //TileMapInstaller.Install(loadTextMapData.LoadTextMap(ref mapData));
    }

 
}
