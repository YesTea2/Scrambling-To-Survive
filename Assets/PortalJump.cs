using UnityEngine;

public class PortalJump : MonoBehaviour
{


    [SerializeField] Transform[] spawnAreas;


    [SerializeField] bool isLeftPortal;
    [SerializeField] bool isRightPortal;
    private void Start()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isLeftPortal)
            {
                
                collision.gameObject.transform.position = spawnAreas[1].transform.position;
            }
            if (isRightPortal)
            {

                collision.gameObject.transform.position = spawnAreas[0].transform.position;
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isLeftPortal)
            {

                collision.gameObject.transform.position = spawnAreas[1].transform.position;
            }
            if (isRightPortal)
            {

                collision.gameObject.transform.position = spawnAreas[0].transform.position;
            }
        }
    }



    
}
