using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OnTouch : MonoBehaviour
{
    [SerializeField] private typeOfObject objectType;
    private enum typeOfObject { arm, rightPoint, wrongPoint }
    private PointsChanging pointsChanging;

    private void Start()
    {
        pointsChanging = FindObjectOfType<PointsChanging>();
    }

    private void OnMouseDown()
    {
        Hit();
    }

    private void Hit()
    {
        if(objectType == typeOfObject.rightPoint)
        {
            pointsChanging.RightHit_WaitAnimTime();
        }
        else
        {
            pointsChanging.WrongHitAction();
        }
    }
}
