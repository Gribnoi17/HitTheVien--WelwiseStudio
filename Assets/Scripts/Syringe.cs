using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Syringe : MonoBehaviour
{
    [Header("Other")]
    [SerializeField] private float timeBetPrick;
    [SerializeField] private GameObject syringe;
    [SerializeField] private Camera cam;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private AudioClip armHitSound;

    [Header("Coordinates for blocking move")]
    [SerializeField] private float lockForX1;
    [SerializeField] private float lockForX2;
    [SerializeField] private float lockForZ1;
    [SerializeField] private float lockForZ2;

    [SerializeField] private float distToZ;
   

    private bool canStartCoroutine;
    private Animator anim;
    private bool canMove;
    private Vector3 tarPos;
    private AudioSource soundSource;

    void Start()
    {
        soundSource = GameObject.FindGameObjectWithTag("SoundSource").GetComponent<AudioSource>();
        anim = syringe.GetComponent<Animator>();
        canStartCoroutine = true;
    }
    void Update()
    {
        Touch();
        MouseDown();

        if(canMove)
        {
            Move();
        }
    }

    void CheckCorotine()
    {
        if (canStartCoroutine && !anim.GetBool("Inject"))
        {
            canStartCoroutine = false;
            StartCoroutine("Prick");
        }
    }
    IEnumerator Prick()
    {
        anim.SetBool("Inject", true);
        soundSource.PlayOneShot(armHitSound);
        yield return new WaitForSeconds(timeBetPrick);
        canStartCoroutine = true;
        anim.SetBool("Inject", false);
        StopAllCoroutines();
    }

    void Touch()
    {
        if (Input.touchCount > 0 && !anim.GetBool("Inject"))
        {
            Touch touch = Input.GetTouch(Input.touchCount - 1);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y , distance));
            touchPos.z += distToZ;
            touchPos.y = transform.position.y;
            if (CanMove(touchPos))
            {
                tarPos = touchPos;
                canMove = true;
            }
        }
    }

    private void MouseDown()
    {
        if(Input.GetButtonDown("Fire1") && !anim.GetBool("Inject"))
        {
            Vector3 mouse = Input.mousePosition;
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, distance));
            touchPos.z += distToZ;
            touchPos.y = transform.position.y;
            if (CanMove(touchPos))
            {
                tarPos = touchPos;
                canMove= true;
            }
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, tarPos, Time.deltaTime * speed);
        if(transform.position == tarPos)
        {
            CheckCorotine();
            canMove = false;
        }
    }

    private bool CanMove(Vector3 newPos)
    {
        if (newPos.x < lockForX1 || newPos.x > lockForX2 || newPos.z > lockForZ1 || newPos.z < lockForZ2)
        {
            return false;
        }

        return true;
    }
}
