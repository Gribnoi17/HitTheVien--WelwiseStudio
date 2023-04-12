using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : MonoBehaviour
{
    void OnCollisionEnter(Collision myCollision)
    {
            if (myCollision.gameObject.CompareTag("Point"))
            {
                Debug.Log("Ты попал");
            }
    }
}
