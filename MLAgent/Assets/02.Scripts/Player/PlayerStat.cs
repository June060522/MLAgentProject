using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    private int _maxBomb = 1;
    private int _bombPower = 1;
    private float _speed = 5.0f;
    private bool _isBarrior = false;
    private bool _canPush = false;

    public int maxBomb => _maxBomb;
    public int bombPower => _bombPower;
    public float speed => _speed;
    public bool isBarrior => _isBarrior;
    public bool canPush => _canPush;
}