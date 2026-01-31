using GameFramework.Fsm;
using UnityEditor.EventSystems;
using UnityEngine;
using UnityGameFramework.Runtime;

public class PlayerIdleState : FsmState<Player>
{
    protected InputComponent input;

    protected override void OnInit(IFsm<Player> fsm)
    {
        base.OnInit(fsm);
        input = GameEntry.GetComponent<InputComponent>();
    }
    protected override void OnEnter(IFsm<Player> fsm)
    {
        base.OnEnter(fsm);
    }

    protected override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        Debug.Log("PlayerIdle Update!");

        Vector2 move = input.Move;
        if (move.sqrMagnitude > 0f)
        {
            ChangeState<PlayerMoveState>(fsm);
        }

        //Debug.Log($"Idle Move Input: {move}, sqrMagnitude: {move.sqrMagnitude}");
    }
}
