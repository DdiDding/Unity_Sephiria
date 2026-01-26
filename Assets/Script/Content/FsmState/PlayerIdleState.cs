using GameFramework.Fsm;
using UnityEditor.EventSystems;
using UnityEngine;
using UnityGameFramework.Runtime;

public class PlayerIdleState : FsmState<Player>
{

    protected override void OnEnter(IFsm<Player> fsm)
    {
        base.OnEnter(fsm);
        Debug.Log("PlayerIdle Enter!");
        //play animaiton
    }

    protected override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        Debug.Log("PlayerIdle Update!");

    }
}
