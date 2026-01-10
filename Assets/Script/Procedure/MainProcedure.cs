using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class MainProcedure : ProcedureBase
{
    public TileMapData mapData = new TileMapData();

    // 한 번만 호출된다는 보장이 없다.
    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        //멥 데이터 Dictionary를 저장
        mapData.LoadTiles();
    }
}
