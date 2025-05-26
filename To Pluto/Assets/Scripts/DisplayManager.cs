using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayManager : MonoBehaviour
{

    SpaceShipController shipController;

    [Header("GameManager")]
    public TMP_Text currentTime_Text;
    public TMP_Text distTraveled_Text;
    public TMP_Text distToNextChkPnt_Text;
    public TMP_Text timeToNextChkPnt_Text;
    public TMP_Text timeToPluto_Text;

    [Header("SpaceShipController")]
    public TMP_Text playerPos_Text;
    public TMP_Text playerSpeed_Text;
    public TMP_Text fuelLevel_Text;
    public TMP_Text resourceCount_Text;

    [Header("BuildEngine")]
    public TMP_Text engineCountText;
    public TMP_Text tractorBeamCountText;
    public TMP_Text detectorLevelText;
    public TMP_Text engineCostText, detectorCostText, tractorBeamCostText, researchCostText;
    public TMP_Text engineUpgradeCostText, engineUpgradeLevel;

    

    [Header("Images")]
    public Image fuelImage;
    public Image resourceImage;
    public Image iconFillImage;
    public Image BuildEngineFillImage;
    public Image upgradeIconFillImage;
    public Image upgradeButtonFillImage;
    public Image detectorFillImage;
    public Image tractorBeamFillImage;

    [Header("Upgrade FillImages")]
    public TMP_Text[] engineUpgradeTexts;
    public TMP_Text[] thrusterUpgradeTexts;
    public Image[] engineResearchFillImages;
    public Image[] thrusterResearchFillImages;

    [Header("ResearchButtons")]
    public Button[] researchButtons;

    [Header("Menus")]
    [SerializeField] GameObject researchMenu;


    [Header("Prefabs")]
    public GameObject floatingTextGO, floatingTextSpawnLocation;

    // [Header("Menus")]
    // public GameObject upgradeMenu;

    private void Start()
    {
        shipController = GameManager.instance.shipController;
    }

    private void Update()
    {
        UpdateTexts();
        UpdateImages();
    }

    void UpdateTexts()
    {
        currentTime_Text.text = "Elapsed Time: " + GameManager.instance.currentTime.ToString("f2");

        playerSpeed_Text.text = shipController.speedKmps.ToString("f0") + " km/hr";
        playerPos_Text.text = (shipController.playerPosition).ToString("f0") + " km from the Sun";

        distTraveled_Text.text = "Space Traveled: " + (shipController.playerPosition - shipController.playerStartPosition).ToString("f0") + " km";

        fuelLevel_Text.text = shipController.fuel + "/" + shipController.maxFuel;
        resourceCount_Text.text = (float)(shipController.resourceCount) + "/" + (float)(shipController.maxResourceCount);
    }

    void UpdateImages()
    {
        fuelImage.fillAmount = shipController.fuel / shipController.maxFuel;
        resourceImage.fillAmount = (float)(shipController.resourceCount) / (float)(shipController.maxResourceCount);
    }

    public void FillImagePercent(Image image, float current, float max)
    {
        image.fillAmount = current / max;
    }


    public void FillImagePercent(Image image, int current, int max)
    {
        image.fillAmount = current / max;
    }

    public void ToggleMenu(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    //Updates the part level and cost of next build
    public void UpdatePartLevel()
    {
        Debug.Log("we triggered the update of the part level it should be saying: " + GameManager.instance.researchManager.GetPartCost("Engine").ToString());
        engineCountText.text = GameManager.instance.shipController.enginePartSOList.Count.ToString();
        engineCostText.text = GameManager.instance.researchManager.GetPartCost("Engine").ToString();

        //, detectorLevelText, tractorBeamLevelText, researchLevelText;
    }
    public void CreateFloatingText(Transform parentTransform, string floatingTextString)
    {
        Vector3 offset = new Vector3(0, 3, 0);
        //Quaternion rot = 
        GameObject newGO = Instantiate(floatingTextGO, floatingTextSpawnLocation.transform.position, floatingTextSpawnLocation.transform.rotation,parentTransform);
        Debug.Log(newGO + " floatingTextString");
        newGO.GetComponentInChildren<TextMesh>().text = floatingTextString;

    }
}
