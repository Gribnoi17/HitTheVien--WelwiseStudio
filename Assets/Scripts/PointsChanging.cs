using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsChanging : MonoBehaviour
{
    [SerializeField] private List<GameObject> redPoints = new List<GameObject>();
    [SerializeField] private List<GameObject> greenPoints = new List<GameObject>();
    [SerializeField] private List<GameObject> bluePoints = new List<GameObject>();
    [SerializeField] private float minTimeBtwRounds;
    [SerializeField] private float maxTimeBtwRounds;
    [SerializeField] private float timeBtwRoundsRamp;
    [SerializeField] private float injectionTime = 1f;
    [Tooltip("Время анимации укола шприца и возварщение его на начальную позицию")]

    private bool isHitAvailable = true;
    private float currentTimeBtwRounds;
    private GameObject currentRedPoint;
    private GameObject currentGreenPoint;
    private GameObject currentBluePoint;
    private UI uiScript;
    
    private void Start()
    {
        uiScript = FindObjectOfType<UI>();
        currentTimeBtwRounds = maxTimeBtwRounds;
        Initialize();
        StartCoroutine(TurnOnPoints());
    }

    public void RightHit_WaitAnimTime()
    {
        if (isHitAvailable)
        {
            isHitAvailable = false;
            StopAllCoroutines();
            uiScript.AddPoints();
            StartCoroutine(HitAction());
        }
    }

    private void Initialize()
    {
        foreach(GameObject point in redPoints)
        {
            point.SetActive(false);
        }

        foreach (GameObject point in greenPoints)
        {
            point.SetActive(false);
        }

        foreach (GameObject point in bluePoints)
        {
            point.SetActive(false);
        }
    }

    private IEnumerator TurnOnPoints()
    {
        currentRedPoint = redPoints[Random.Range(0, redPoints.Count)];
        currentGreenPoint = greenPoints[Random.Range(0, greenPoints.Count)];
        currentBluePoint = bluePoints[Random.Range(0, bluePoints.Count)];

        currentBluePoint.SetActive(true);
        currentGreenPoint.SetActive(true);
        currentRedPoint.SetActive(true);

        isHitAvailable = true;
        yield return new WaitForSeconds(currentTimeBtwRounds);
        WrongHitAction();
        TurnOFFcurrentPoints();

        if (currentTimeBtwRounds > minTimeBtwRounds)
            currentTimeBtwRounds -= timeBtwRoundsRamp;
        StartCoroutine(TurnOnPoints());
    }

    private void TurnOFFcurrentPoints()
    {
        currentBluePoint.SetActive(false);
        currentGreenPoint.SetActive(false);
        currentRedPoint.SetActive(false);
    }

    private IEnumerator HitAction()
    {
        yield return new WaitForSeconds(injectionTime);
        TurnOFFcurrentPoints();
        StartCoroutine(TurnOnPoints());
    }

    public void WrongHitAction()
    {
        if (isHitAvailable)
        {
            isHitAvailable = false;
            uiScript.WrongTouch();
            StopAllCoroutines();
            StartCoroutine(HitAction());
        }      
    }


}
