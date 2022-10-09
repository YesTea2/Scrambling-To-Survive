using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxhealth;
    [SerializeField] float forceAmount;
    bool isApplyingForce;
    int direction;
    Rigidbody2D _rig;
    SpriteRenderer _render;
    Color storedColor;


    private void Start()
    {
        health = maxhealth;
        _rig = GetComponent<Rigidbody2D>();

        _render = transform.GetChild(0).GetComponent<SpriteRenderer>();
        storedColor = _render.color;
    }

    private void FixedUpdate()
    {
        if (isApplyingForce)
        {
            isApplyingForce = false;
            if (direction < 0)
            {
                
                _rig.AddForce(new Vector2(-1, .1f) * forceAmount, ForceMode2D.Impulse);
                StartCoroutine(BlinkMe());
               
                

            }
            if (direction > 0)
            {
                _rig.AddForce(new Vector2(1, .1f) * forceAmount, ForceMode2D.Impulse);
                StartCoroutine(BlinkMe());




            }
        }
    }


    public void TakeDamage(int amount, int dir)
    {
        direction = dir;
        health -= amount;
        isApplyingForce = true;
       

    }

    IEnumerator BlinkMe()
    {
        
        _render.color = Color.red;
        yield return new WaitForSeconds(.1f);
        _render.color = storedColor;

    }
}
