using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class Player : Agent, IPoppingObj
{
    Rigidbody _rb;
    [HideInInspector]public PlayerStat stat;

    public override void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
        stat = new();
    }

    public override void OnEpisodeBegin()
    {
        _rb.velocity = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 에이전트가 환경을 관찰하는 코드
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // 입력된 행동을 받아와 이동 처리
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        Vector3 move = new Vector3(moveX, 0f, moveZ);

        _rb.velocity = move * stat.speed;

        // 보상 및 종료 조건 설정 등 추가 작업 가능
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // 인간이 직접 제어할 때 행동을 설정하는 코드
        var continuousActionsOut = actionsOut.ContinuousActions;

        // 키보드 입력에 따라 행동을 설정
        continuousActionsOut[0] = Input.GetAxisRaw("Horizontal");
        continuousActionsOut[1] = Input.GetAxisRaw("Vertical");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            UseBomb();
    }

    private void UseBomb()
    {
        if (stat.maxBomb > stat.useBomb)
        {
            stat.useBomb++;
            BombManager.Instance.OnUseBomb(transform.position, stat);
        }
    }

    public Vector2Int GetPositionIndex() => PositionManager.Instance.GetPositionIndex(transform.position);

    public void PoppingObj()
    {
        EndEpisode();
        //Destroy(gameObject);
    }
}
