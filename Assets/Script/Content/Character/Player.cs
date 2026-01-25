using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

public class Player : EntityLogic
{
    private IFsm<Player> m_Fsm;
    protected internal override void OnInit(object userData)
    {
        base.OnInit(userData);
    }

    protected internal override void OnShow(object userData)
    {
        base.OnShow(userData);
        CreateFsm();
    }
    protected internal override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        DestoryFsm();
    }


    private void CreateFsm()
    {
        var fsmComponent = GameEntry.GetComponent<FsmComponent>();

        m_Fsm = fsmComponent.CreateFsm(
            this,
            new PlayerIdleState(),
            new PlayerMoveState()
            );
    }

    private void DestoryFsm()
    {
        if (m_Fsm != null)
        {
            GameEntry.GetComponent<FsmComponent>().DestroyFsm(m_Fsm);
            m_Fsm = null;
        }
    }
}
