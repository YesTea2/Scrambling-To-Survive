using UnityEngine;

public class PortalJump : MonoBehaviour
{

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform[] spawnAreas;

    [SerializeField] bool isLeftPortal;
    [SerializeField] bool isRightPortal;
    private void Start()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isLeftPortal)
            {
                Teleport(2);
            }
            if (isRightPortal)
            {
                Teleport(1);
            }
        }
    }



    public void Teleport(int number)
    {
        if (number == 1)
        {
            playerPrefab.transform.position = spawnAreas[0].transform.position;
        }
        else if (number == 2)
        {
            playerPrefab.transform.position = spawnAreas[1].transform.position;
        }
    }
}
