using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public SpaceShipController shipController;

    public float currentTime = 0;
    
    
    public int asteroidClickCounter;
    
    public TMP_Text playerPos_Text;
    public TMP_Text playerSpeed_Text;
    public TMP_Text currentTime_Text;
    public TMP_Text distTraveled_Text;
    public TMP_Text distToNextChkPnt_Text;
    public TMP_Text timeToNextChkPnt_Text;


    //to be replaced with checkPoint SO
    public long mercuryPos  = 5790;      //*10^4
    public long venusPos    = 10820;     //*10^4
    public long earthPos    = 14960;     //*10^4
    public long marsPos     = 22800;     //*10^4
    public long jupiterPos  = 77850;     //*10^4
    public long saturnPos   = 143200;    //*10^4
    public long uranusPos   = 286700;    //*10^4
    public long neptunePos  = 451500;    //*10^4
    public long plutoPos    = 590640;    //*10^4

    bool testBool = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        currentTime_Text.text = "Elapsed Time: " + currentTime.ToString("f2");
        
        playerSpeed_Text.text = shipController.speedKmps.ToString("f0") + " km/hr";
        
        playerPos_Text.text = (shipController.playerPosition).ToString("f0") + " km from the Sun"; // + "*10^4 km";
               
        distTraveled_Text.text = "Distance Traveled: " + "\n" + (shipController.playerPosition - shipController.playerStartPosition).ToString("f0") + " km";

        if (shipController.playerPosition < (marsPos * 10000))
        {
            distToNextChkPnt_Text.text = ((long)(marsPos * 10000) - shipController.playerPosition).ToString("f0") + "\n" + " Remaining distance to Mars";
        }
        else if (shipController.playerPosition < (jupiterPos * 10000))
        {
            distToNextChkPnt_Text.text = ((long)(jupiterPos * 10000) - shipController.playerPosition).ToString("f0") + "\n" + " Remaining distance to Jupiter";
        }
        else if (shipController.playerPosition < (saturnPos * 10000))
        {
            distToNextChkPnt_Text.text = ((long)(saturnPos * 10000) - shipController.playerPosition).ToString("f0") + "\n" + " Remaining distance to Saturn";
        }
        else if (shipController.playerPosition < (uranusPos * 10000))
        {
            distToNextChkPnt_Text.text = ((long)(uranusPos * 10000) - shipController.playerPosition).ToString("f0") + "\n" + " Remaining distance to Uranus";
        }
        else if (shipController.playerPosition < (neptunePos * 10000))
        {
            distToNextChkPnt_Text.text = ((long)(neptunePos * 10000) - shipController.playerPosition).ToString("f0") + "\n" + " Remaining distance to neptune";
        }
        else
        {
            distToNextChkPnt_Text.text = ((long)(plutoPos * 10000) - shipController.playerPosition).ToString("f0") + "\n" + " Remaining distance to pluto";
        }

        long timeToNextCheckpoint = ((((long)(plutoPos * 10000) - shipController.playerPosition) / shipController.speedKmps));
        float timeToNextCheckpointFloat = 0f;
        if (timeToNextCheckpoint <= 0)
        {
            timeToNextCheckpointFloat = (((float)(plutoPos) - shipController.playerPosition / 10000) / (shipController.speedKmps / 10000));
        }


            //Shows time in years
            if (timeToNextCheckpoint / 24 / 365 > 0)
        {

            timeToNextChkPnt_Text.text = ((((long)(plutoPos * 10000) - shipController.playerPosition) / shipController.speedKmps) / 24 / 365).ToString() + "\n" + "years remaining to pluto";
        }
        //shows time in days
        else if (timeToNextCheckpoint / 24 > 0)
        {
            timeToNextChkPnt_Text.text = ((((long)(plutoPos * 10000) - shipController.playerPosition) / shipController.speedKmps) / 24).ToString() + "\n" + "days remaining to pluto";
        }
        else if (timeToNextCheckpoint > 0)
        {
            timeToNextChkPnt_Text.text = ((((long)(plutoPos * 10000) - shipController.playerPosition) / shipController.speedKmps)).ToString() + "\n" + "hours remaining to pluto";
        }
        else if (timeToNextCheckpoint == 0 && timeToNextCheckpointFloat * 60 > 0)
        {
            timeToNextChkPnt_Text.text = (timeToNextCheckpointFloat * 60).ToString() + "\n" + "minutes remaining to pluto";
        }
        else
        {
            timeToNextChkPnt_Text.text = (timeToNextCheckpointFloat * 60 * 60).ToString() + "\n" + "seconds remaining to pluto";
        }


        if (testBool)
        {
            test();
            testBool = false;
        }
    }

    private void test()
    {
        Debug.Log(((long)(plutoPos * 10000)).ToString());
        Debug.Log(((long)(plutoPos * 10000) - shipController.playerPosition).ToString());
        Debug.Log((((long)(plutoPos * 10000) - shipController.playerPosition) /shipController.speedKmps).ToString());
        Debug.Log(((((long)(plutoPos * 10000) - shipController.playerPosition) /shipController.speedKmps)/24).ToString());
        Debug.Log(((((long)(plutoPos * 10000) - shipController.playerPosition) /shipController.speedKmps)/24/365).ToString());
    }
}
