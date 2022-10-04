using UnityEngine;

public class LimitFramerate : MonoBehaviour
{
    [SerializeField] private int framerate = 60;

    public static LimitFramerate instance;
    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);



    }
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = framerate;
    }


}
