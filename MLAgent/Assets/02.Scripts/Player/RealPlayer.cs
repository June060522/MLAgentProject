using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using UnityEngine;

public class RealPlayer : Agent, IPoppingObj
{
    Rigidbody _rb;
    [HideInInspector] public PlayerStat stat;

    bool isLastX = false;
    private float lastInputX = 0;
    private float lastInputZ = 0;
    int hp = 5;

    public LayerMask layerMask = (1 << 8) | (1 << 10);
    bool isMoveX = false;
    RaycastHit hit;

    public MapManager mapManager;
    public Game game;
    public PlayerManager playerManager;
    public PositionManager positionManager;
    public ItemManager itemManager;
    public BombManager bombManager;

    public override void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
        mapManager = transform.parent.parent.GetComponentInChildren<MapManager>();
        game = transform.parent.parent.GetComponentInChildren<Game>();
        playerManager = transform.parent.parent.GetComponentInChildren<PlayerManager>();
        positionManager = transform.parent.parent.GetComponentInChildren<PositionManager>();
        itemManager = transform.parent.parent.GetComponentInChildren<ItemManager>();
        bombManager = transform.parent.parent.GetComponentInChildren<BombManager>();
        stat = new();
        stat.speed = 10f;
    }

    public override void OnEpisodeBegin()
    {
        _rb.velocity = Vector3.zero;
        stat = new();
        //transform.position = new Vector3(-11.7f, -8.5f, -7.92f) + transform.parent.position;
        hp = 5;
        //mapManager.Init();
        //game.Init();
        //playerManager.Init();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position.x);
        sensor.AddObservation(transform.position.z);

        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                sensor.AddObservation(mapManager._tileTypes[i, j]);
            }
        }

        // 팝핑 오브젝트(아이템 등)의 상태를 관찰 (예시로 16x14 맵을 1차원 배열로 변환하여 추가)
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                sensor.AddObservation(mapManager.GetPoppingObj(new Vector2Int(i, j)) != null);
            }
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // 입력된 행동을 받아와 이동 처리
        float k = actions.DiscreteActions[1];
        if (k == 0)
            Move(1, 0);
        else if (k == 1)
            Move(-1, 0);
        else if (k == 2)
            Move(0, 1);
        else if (k == 3)
            Move(0, -1);
        else
            Move(0, 0);

        // 보상 및 종료 조건 설정 등 추가 작업 가능
        if (actions.DiscreteActions[0] == 1)
            UseBomb();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // 인간이 직접 제어할 때 행동을 설정하는 코드
        var continuousActionsOut = actionsOut.ContinuousActions;
        var discreteActionsOut = actionsOut.DiscreteActions;

        // 키보드 입력에 따라 행동을 설정
        if (Input.GetKey(KeyCode.D))
            discreteActionsOut[1] = 0;
        else if (Input.GetKey(KeyCode.A))
            discreteActionsOut[1] = 1;
        else if (Input.GetKey(KeyCode.W))
            discreteActionsOut[1] = 2;
        else if (Input.GetKey(KeyCode.S))
            discreteActionsOut[1] = 3;
        else
            discreteActionsOut[1] = 4;

        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    private void UseBomb()
    {
        
        if (stat.maxBomb > stat.useBomb)
        {
            stat.useBomb++;
            bombManager.OnUseBomb(transform.localPosition + transform.parent.position, stat);
        }
    }

    public Vector2Int GetPositionIndex() => positionManager.GetPositionIndex(transform.localPosition) + new Vector2Int(7, 6);

    public void PoppingObj()
    {
    }

    private void Move(float x, float z)
    {
        if (x == 0 && z == 0)
        {
            _rb.velocity = Vector3.zero;
            lastInputX = lastInputZ = 0;
            return;
        }
        

        lastInputX = x;
        lastInputZ = z;

        int y1 = 0;
        int y2 = 0;

        if (lastInputX > 0)
            y1 = 90;
        else if (lastInputX < 0)
            y1 = 270;

        if (lastInputZ > 0)
            y2 = 360;
        else if (lastInputZ < 0)
            y2 = 180;

        if (y1 == 0 || y2 == 0)
            transform.rotation = Quaternion.Euler(0, y1 + y2, 0);
        else
        {
            if (y1 == 90 && y2 == 360)
                transform.rotation = Quaternion.Euler(0, 45, 0);
            if (y1 == 270 && y2 == 180)
                transform.rotation = Quaternion.Euler(0, 225, 0);
            if (y1 == 90 && y2 == 180)
                transform.rotation = Quaternion.Euler(0, 135, 0);
            if (y1 == 270 && y2 == 360)
                transform.rotation = Quaternion.Euler(0, 315, 0);

        }



        Vector3 startPos = transform.position + transform.up;
        Vector3 leftDirection = Quaternion.Euler(0, -30, 0) * transform.forward;
        Vector3 rightDirection = Quaternion.Euler(0, 30, 0) * transform.forward;
        Vector3 forwardDirection = transform.forward;

        Debug.DrawRay(startPos, forwardDirection, Color.red);
        Debug.DrawRay(startPos, leftDirection, Color.red);
        Debug.DrawRay(startPos, rightDirection, Color.red);

        if (Physics.Raycast(startPos, forwardDirection, out RaycastHit hit, 0.75f, layerMask) ||
            Physics.Raycast(startPos, rightDirection, out RaycastHit rightHit, 0.75f, layerMask) ||
            Physics.Raycast(startPos, leftDirection, out RaycastHit leftHit, 0.75f, layerMask))
        {
            lastInputX = lastInputZ = 0;
        }
        else
            AddReward(1f);
        // 마지막 입력 방향으로 이동
        Vector3 move = new Vector3(lastInputX, 0f, lastInputZ);
        _rb.velocity = move * stat.speed;
        lastInputX = lastInputZ = 0;
    }

    public void AddKillAward()
    {
        AddReward(50f);
    }

    public void BrickObject()
    {
        AddReward(5f);
    }

    public void EatItem()
    {
        AddReward(10f);
    }
}