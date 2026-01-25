using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class MainProcedure : ProcedureBase
{
    // 한 번만 호출된다는 보장이 없다.
    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        GameEntry.GetComponent<EntityComponent>().ShowEntity<Player>(
            (int)EntityId.Player,
            "Assets/Prefabs/Entities/Player.prefab",
            "Player" // entityGroupName
        );

        GameEntry.GetComponent<EntityComponent>().ShowEntity<Player>(
            2,
            "Assets/Prefabs/Entities/Player.prefab",
            "Player" // entityGroupName
        );

        // With priority and user data
        //GameEntry.GetComponent<EntityComponent>().ShowEntity<PlayerEntity>(
        //    1, "Assets/Game/Entities/Player.prefab", "Player", 100, userData
        //);
    }
}
