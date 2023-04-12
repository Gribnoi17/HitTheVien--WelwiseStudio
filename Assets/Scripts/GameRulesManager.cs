using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRulesManager : MonoBehaviour
{
    private enum sceneType { LighterScene, SyringeScene }
    [SerializeField] private sceneType typeOfScene;

    [Header("Syringe Statements")]
    [SerializeField] private int pointsForHit = 25;
    [SerializeField] private int penaltyPointsForWrongHit = 20;
    [SerializeField] private int persentOfSliderFor1Hit = 10;
    [Tooltip("Часть в процентах, на которую заполняется полоска комбо за правильное попадание")]
    public AudioClip ComboFinishSound;


    [Header("Menu")]
    public int Stage1Points = 666;
    public int Stage2Points = 1223;
    public int Stage3Points = 4444;
    public int Stage4Points = 10105;
    public int Stage5Points = 50000;

    public int PointsForHit {get { return pointsForHit; } }
    public int PenaltyPointsForWrongHit { get { return penaltyPointsForWrongHit; } }
    public int PersentOfSliderFor1Hit { get { return persentOfSliderFor1Hit; } }

}
