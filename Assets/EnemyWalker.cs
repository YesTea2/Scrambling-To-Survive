using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    public int dirNumber;
    
    public bool isMoving;

    Rigidbody2D _rig;

    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1, dirNumber);
        }
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
