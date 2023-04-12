using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider comboSlider;
    [SerializeField] private TextMeshProUGUI ratioText;
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private GameObject notificationPanel;
    [SerializeField] private GameObject instuctionImg;

    private int currentScore = 0;
    private float currentComboPersent = 1;
    private GameRulesManager gameRulesManager;
    private AudioSource soundSource;
    private bool isComboFinishSoundAvailable;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2 && !PlayerPrefs.HasKey("SawRules"))
        {
            instuctionImg.SetActive(true);
            PlayerPrefs.SetString("SawRules", "Yes");
        }

        isComboFinishSoundAvailable = true;
        gameRulesManager = FindObjectOfType<GameRulesManager>();
        WrongTouch();
        soundSource = GameObject.FindGameObjectWithTag("SoundSource").GetComponent<AudioSource>();
    }

    private IEnumerator TurnOnNotificationPanel(string stage)
    {
        notificationPanel.SetActive(true);
        notificationText.text = $"Вы достигли звания '{stage}'";
        yield return new WaitForSeconds(2.16f);
        notificationPanel.SetActive(false);
    }

    private void SendNotification()
    {
        if (PlayerPrefs.GetInt("BestScore") >= gameRulesManager.Stage1Points && PlayerPrefs.GetInt("PassedStages") < 1)
        {
            StartCoroutine(TurnOnNotificationPanel("Дозатор"));
            PlayerPrefs.SetInt("PassedStages", 1);
        }
        if (PlayerPrefs.GetInt("BestScore") >= gameRulesManager.Stage2Points && PlayerPrefs.GetInt("PassedStages") < 2)
        {
            StartCoroutine(TurnOnNotificationPanel("Приколист"));
            PlayerPrefs.SetInt("PassedStages", 2);
        }
        if (PlayerPrefs.GetInt("BestScore") >= gameRulesManager.Stage3Points && PlayerPrefs.GetInt("PassedStages") < 3)
        {
            StartCoroutine(TurnOnNotificationPanel("Снайпер"));
            PlayerPrefs.SetInt("PassedStages", 3);
        }
        if (PlayerPrefs.GetInt("BestScore") >= gameRulesManager.Stage4Points && PlayerPrefs.GetInt("PassedStages") < 4)
        {
            StartCoroutine(TurnOnNotificationPanel("Подъездный доктор"));
            PlayerPrefs.SetInt("PassedStages", 4);
        }
        if (PlayerPrefs.GetInt("BestScore") >= gameRulesManager.Stage5Points && PlayerPrefs.GetInt("PassedStages") < 5)
        {
            StartCoroutine(TurnOnNotificationPanel("Без шансов"));
            PlayerPrefs.SetInt("PassedStages", 5);
        }
    }

    public void AddPersents2Combo()
    {
        if(comboSlider.value < 1)
        {
            currentComboPersent += gameRulesManager.PersentOfSliderFor1Hit;
            comboSlider.value = currentComboPersent / 100; //Делим на 10 чтобы перевести в вид для слайдера
            isComboFinishSoundAvailable = true;

            if (comboSlider.value == 0.3f || comboSlider.value == 0.5f || comboSlider.value == 0.7f || comboSlider.value == 1f)
            {
                ratioText.gameObject.SetActive(false);
                ratioText.gameObject.SetActive(true);
                ratioText.text = (comboSlider.value * 10).ToString() + "X";
            }
            else
            {
                ratioText.gameObject.SetActive(false);
            }

        }

        if (comboSlider.value >= 1)
        {
            if (isComboFinishSoundAvailable)
            {
                soundSource.PlayOneShot(gameRulesManager.ComboFinishSound);
                isComboFinishSoundAvailable = false;
            }
        }
   
    }

    public void AddPoints()
    {
        if (currentScore < gameRulesManager.PointsForHit)
            currentScore += gameRulesManager.PointsForHit;
        else
            currentScore += Mathf.RoundToInt(Mathf.Abs(gameRulesManager.PointsForHit) * (currentComboPersent/10)); //Умножаем на 10 чтобы привести в вид коэффициента
        scoreText.text = currentScore.ToString();
        SendNotification();
        AddPersents2Combo();
        if(currentScore > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
        }
    }

    public void ReducePoints()
    {
        if(currentScore <= gameRulesManager.PenaltyPointsForWrongHit)
        {
            currentScore = 0;
        }
        else
        {
            currentScore -= gameRulesManager.PenaltyPointsForWrongHit;
        }
        scoreText.text = currentScore.ToString();
        ratioText.gameObject.SetActive(false);
    }

    public void WrongTouch()
    {
        ReducePoints();
        currentComboPersent = 0;
        comboSlider.value = 0;
    }
}
