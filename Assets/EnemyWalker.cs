using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    float dirNumber;
    
    public bool isMoving;

    Rigidbody2D _rig;

    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
    }

  

    private void FixedUpdate()
    {
        if (isMoving)
        {
            _rig.velocity = new Vector3(dirNumber, 0, 0);
        }
    }

    public void DirectionToSend(int wayToSend, bool isMove)
    {
        if (isMove)
        {
            dirNumber = wayToSend;
            isMoving = isMove;
        }
    }

}
