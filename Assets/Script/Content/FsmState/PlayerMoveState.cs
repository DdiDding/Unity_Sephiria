using GameFramework.Fsm;
using UnityEngine;
public class PlayerMoveState : FsmState<Player>
{

    protected override void OnEnter(IFsm<Player> fsm)
    {
        //play animaiton
        base.OnEnter(fsm);
    }

    protected override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);


    }
}
