using GameFramework.Fsm;
using System.Buffers;
using UnityEngine;
using UnityGameFramework.Runtime;
public class PlayerMoveState : FsmState<Player>
{
    protected InputComponent input;
    Player player;
    Vector2 velocity;
    Rigidbody2D rigidbody2D;

    protected override void OnInit(IFsm<Player> fsm)
    {
        base.OnInit(fsm);
        input = GameEntry.GetComponent<InputComponent>();
        player = fsm.Owner;
        velocity = new Vector2(0, 0);
        rigidbody2D = player.GetComponent<Rigidbody2D>();
        rigidbody2D.linearDamping = 10;
    }

    protected override void OnEnter(IFsm<Player> fsm)
    {
        base.OnEnter(fsm);
        //애니메이션 상태 전환
    }
    protected override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        Vector2 direction = input.Move;
        

        // 상태 변환
        if (direction.sqrMagnitude == 0f)
        {
            ChangeState<PlayerIdleState>(fsm);
        }


        // Entity 이동
        // TODO: 위치에 합이 아닌 물리로 이동할 것
        {
            // 대각선 속도 보정
            if (direction.sqrMagnitude > 1f)
                direction.Normalize();

            // TODO: moveSpeed 같은 속성은 Entity Data로 뺄 것
            float moveSpeed = 180f;

            // 이동 수치
            velocity = direction * moveSpeed * elapseSeconds;


            rigidbody2D.AddForce(velocity);
            

            // 속도 제한
            float maxSpeed = 5f;
            if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                rigidbody2D.linearVelocity = velocity.normalized * maxSpeed;
            }

            Debug.Log("속도 : " + rigidbody2D.linearVelocity.magnitude);
        }
        
    }
}
