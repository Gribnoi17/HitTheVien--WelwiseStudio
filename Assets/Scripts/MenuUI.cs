using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


//Sava's code
//Sava's code
//Sava's code
public class MenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private List<GameObject> blockPanels = new List<GameObject>();
    [SerializeField] private GameObject warningPanel;

    private GameRulesManager gameRulesManager;
    private int bestScore;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("SawWarning"))
        {
            PlayerPrefs.SetString("SawWarning", "No");
            warningPanel.SetActive(true);
        }

        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }

        PlayerPrefs.SetInt("PassedStages", 0);

        recordText.text = PlayerPrefs.GetInt("BestScore").ToString();
        bestScore = PlayerPrefs.GetInt("BestScore");
        gameRulesManager = FindObjectOfType<GameRulesManager>();

        foreach(GameObject panel in blockPanels)
        {
            panel.SetActive(false);
        }

        if(bestScore < gameRulesManager.Stage1Points)
        {
            blockPanels[0].SetActive(true);            
        }
        else
        {
            PlayerPrefs.SetInt("PassedStages", 1);
        }

        if (bestScore < gameRulesManager.Stage2Points)
        {
            blockPanels[1].SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("PassedStages", 2);
        }

        if (bestScore < gameRulesManager.Stage3Points)
        {
            blockPanels[2].SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("PassedStages", 3);
        }

        if (bestScore < gameRulesManager.Stage4Points)
        {
            blockPanels[3].SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("PassedStages", 4);
        }

        if (bestScore < gameRulesManager.Stage5Points)
        {
            blockPanels[4].SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("PassedStages", 5);
        }
    }



}
