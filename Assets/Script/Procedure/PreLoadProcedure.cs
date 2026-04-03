using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class PreLoadProcedure : ProcedureBase
{

    // -------------------------------
    // Lifr Cycle
    // -------------------------------

    protected override void OnDestroy(ProcedureOwner procedureOwner)
    {
        base.OnDestroy(procedureOwner);
    }

    protected override void OnInit(ProcedureOwner procedureOwner)
    {
        base.OnInit(procedureOwner);
        tileManager = GameEntry.GetComponent<TileComponent>();
    }

    // 한 번만 호출된다는 보장이 없다.
    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);
        tileManager.LoadTileGroup(OnTileLoadComplete);

        
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        if (IsResourceLoadComplete() == true)
        {
            ChangeState<TestProcedure>(procedureOwner);
        }
    }
    protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
    }

    // --------------------------------------------
    // Public Functions
    // --------------------------------------------
    public void OnTileLoadComplete()
    {
        bTileLoadComplete = true;
    }

    // --------------------------------------------
    // Private Functions
    // --------------------------------------------

    /**
     * @brief 모든 리소스가 로드가 완료 되었는지 체크하여 반환하는 함수
     */
    private bool IsResourceLoadComplete()
    {
        return bTileLoadComplete;
    }


    // --------------------------------------------
    // Member Variables
    // --------------------------------------------
    private TileComponent tileManager;
    // 로드가 완료 되면, 다음 프로시저로 넘어가기 위한 bool 변수
    private bool bTileLoadComplete = false;

}
