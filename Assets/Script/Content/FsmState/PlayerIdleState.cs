using GameFramework.Fsm;
using UnityEngine;

public class PlayerIdleState : FsmState<Player>
{

    protected override void OnEnter(IFsm<Player> fsm)
    {
        base.OnEnter(fsm);
        //play animaiton
    }

    protected override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);


    }
}
