using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;
public class PlayerMoveState : FsmState<Player>
{
    protected InputComponent input;

    protected override void OnInit(IFsm<Player> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<Player> fsm)
    {
        base.OnEnter(fsm);

        input = GameEntry.GetComponent<InputComponent>();

    }
    protected override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        Debug.Log("Player Move Update!");

        Vector2 move = input.Move;
        if (move.sqrMagnitude == 0f)
        {
            ChangeState<PlayerIdleState>(fsm);
        }
    }
}
