using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using JetBrains.Annotations;

public class Move : MonoBehaviour
{
    //Скрипт для передвижения шприца

    [Header("Speed for move")]
    [SerializeField] float moveSpeedHorizontal = 2;
    [SerializeField] float moveSpeedVertical = 2;

    [Header("Coordinates for blocking move")]
    [SerializeField] float lockForX1;
    [SerializeField] float lockForX2;
    [SerializeField] float lockForZ1;
    [SerializeField] float lockForZ2;

    Animator anim;
    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if(!anim.GetBool("Inject"))
        {
            float horizontalInput = Input.GetAxis("Mouse X");
            float verticalInput = Input.GetAxis("Mouse Y");
            if (verticalInput != 0 || horizontalInput != 0)
            {
                Vector3 newPos = new Vector3(-(horizontalInput) * moveSpeedHorizontal, 0, -(verticalInput) * moveSpeedVertical) * Time.deltaTime;
                Vector3 newPosForObject = new Vector3(transform.position.x + newPos.x, 0, transform.position.z + newPos.z);

                //проверка - не выйдет ли шприц за опред. область
                if (newPosForObject.x > lockForX1 || newPosForObject.x < lockForX2  || newPosForObject.z > lockForZ1 || newPosForObject.z < lockForZ2)
                {
                    return;
                }

                transform.Translate(new Vector3(-(horizontalInput) * moveSpeedHorizontal, 0, -(verticalInput) * moveSpeedVertical) * Time.deltaTime);
            }
        }
    }
}
