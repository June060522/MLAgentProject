using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IPoppingObj
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

    [SerializeField] TextMeshProUGUI max;
    [SerializeField] TextMeshProUGUI cur;
    [SerializeField] TextMeshProUGUI power;
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI barrior;

    private void Awake()
    {
        mapManager = transform.parent.Find("Manager").GetComponentInChildren<MapManager>();
        game = transform.parent.Find("Manager").GetComponentInChildren<Game>();
        playerManager = transform.parent.Find("Manager").GetComponentInChildren<PlayerManager>();
        positionManager = transform.parent.Find("Manager").GetComponentInChildren<PositionManager>();
        itemManager = transform.parent.Find("Manager").GetComponentInChildren<ItemManager>();
        bombManager = transform.parent.Find("Manager").GetComponentInChildren<BombManager>();
        _rb = GetComponent<Rigidbody>();
        stat = new();
    }

    public void Reset()
    {
        _rb.velocity = Vector3.zero;
        stat = new();
        transform.position = new Vector3(-11.7f, -8.5f, -7.92f) + transform.parent.position;
        hp = 5;
        mapManager.Init();
        game.Init();
        playerManager.Init();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Move(x, z);

        if (Input.GetKeyDown(KeyCode.Space))
            UseBomb();
        UpdateUI();
    }

    public void UpdateUI()
    {
        max.text = $"max : {stat.maxBomb}";
        cur.text = $"cur : {stat.maxBomb - stat.useBomb}";
        power.text = $"power : {stat.bombPower}";
        speed.text = $"speed : {stat.speed}";
        barrior.text = $"barrior : {stat.isBarrior}";
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
        // 마지막 입력 방향으로 이동
        Vector3 move = new Vector3(lastInputX, 0f, lastInputZ);
        _rb.velocity = move * stat.speed;
        lastInputX = lastInputZ = 0;
    }

    private void UseBomb()
    {

        if (stat.maxBomb > stat.useBomb)
        {
            stat.useBomb++;
            bombManager.OnUseBomb(transform.localPosition + transform.parent.position, stat);
        }
    }

    public void PoppingObj()
    {
        if (stat.isBarrior)
        {
            stat.isBarrior = false;
            return;
        }
        SceneManager.LoadScene("Intro");
    }

    public Vector2Int GetPositionIndex() => positionManager.GetPositionIndex(transform.localPosition) + new Vector2Int(7, 6);

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Ghost"))
        {
            LevelManager.instance.minGhost--;
            Destroy(collision.gameObject);
        }
    }
}