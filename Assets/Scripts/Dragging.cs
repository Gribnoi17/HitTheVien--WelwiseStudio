using UnityEngine;

public class Dragging : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float moveSpeed;
    private AudioLighter _audioLighter;

    private float _xLeftLimit = 12.6f;
    private float _xRightLimit = 2.78f;
    private float _yTopLimit = 4.5f;
    private float _yBottomLimit = 0f;

    private bool _canMove;
    private Vector3 _tarPos;
    private bool _tarRay;
    private bool _isDragging = false;


    public bool GetIsDragging()
    {
        return _isDragging;
    }


    private void Start()
    {
        _audioLighter = FindObjectOfType<AudioLighter>();
    }


    private void Update()
    {
        OnTouch();

        if (!_isDragging)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
        }


        if (_canMove)
            Move();
    }

    private void OnMouseDrag()
    {
        _isDragging= true;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
        //_audioLighter.PlaySparkSound();
        _audioLighter.PlayBoilingSound();
    }

    private void OnMouseDown()
    {
        _audioLighter.PlaySparkSound();
    }

    

    private void OnTouch()
    {
        if (Input.touchCount > 0)
        {
            Ray ray;
            if (Input.touches[Input.touchCount - 1].phase == TouchPhase.Began)
            {
                ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.GetComponent<Dragging>())
                        _tarRay = true;
                }
            }
            else if (Input.touches[Input.touchCount - 1].phase == TouchPhase.Ended)
            {
                _tarRay = false;
                _isDragging= false;
                _audioLighter.StopSound();
            }

            if (_tarRay)
            {
                Touch touch = Input.GetTouch(Input.touchCount - 1);
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                touchPos.z = transform.position.z;
                _tarPos = touchPos;
                if (CanMove(touchPos))
                {
                    _canMove = true;
                }
            }
        }
    }

    private bool CanMove(Vector3 newPos)
    {
        if (newPos.x > _xLeftLimit || newPos.x < _xRightLimit || newPos.y > _yTopLimit || newPos.y < _yBottomLimit)
        {
            return false;
        }

        return true;
    }

    private void Move()
    {
        _isDragging = true;
        //_audioLighter.PlaySparkSound();
        _audioLighter.PlayBoilingSound();
        transform.position = _tarPos;
        if (transform.position == _tarPos)
        {
            _canMove = false;
        }
    }

}
