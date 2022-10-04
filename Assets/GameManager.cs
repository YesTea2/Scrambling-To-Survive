using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform spawnPosition;


    private void Start()
    {


        playerPrefab.transform.position = spawnPosition.position;




    }



}
