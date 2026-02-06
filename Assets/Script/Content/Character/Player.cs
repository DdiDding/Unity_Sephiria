using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

public class Player : EntityLogic
{
    private IFsm<Player> m_Fsm;
    private Animator animator;
    protected internal override void OnInit(object userData)
    {
        base.OnInit(userData);
        animator = GetComponent<Animator>();
    }

    protected internal override void OnShow(object userData)
    {
        base.OnShow(userData);
        CreateFsm();
        m_Fsm.Start<PlayerIdleState>();
    }

    protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        Loging();
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
    private void Loging()
    {
        Debug.Log("플레이어 상태 : " + m_Fsm.CurrentState.GetType().Name);

    }

    public void SetMove(bool moving)
    {
        animator.SetBool("IsMove", moving);
    }
}
