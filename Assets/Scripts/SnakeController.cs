using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeController : MonoBehaviour
{
    [SerializeField] private GameObject TailPrefab;

    public float Speed = 0.5f;
    public float StepSize = 1.0f;
    public int Length = 2;
    
    private int _tailLength = 0;
    private Rigidbody2D _rigidbody;
    private MovingPart _tail;
    
    private Vector2 _movementDirection;
    private float _nextStepTime;
    private float _stepTime = 0;

    private void Start()
    {
        _movementDirection =  Vector2.up;
        BounceTime();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void BounceTime()
    {
        _stepTime -= _nextStepTime;
        _nextStepTime = 1/Speed;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovementDirection();

        _stepTime += Time.deltaTime;
        if (_stepTime < _nextStepTime)
        {
            return;
        }
        MoveSnake();
        BounceTime();
    }
    
    private void MoveSnake()
    {
        var position = transform.position;
        var positionIncrement = _movementDirection * StepSize;
        var newPosition = new Vector2(position.x, position.y) + positionIncrement;
      
        _rigidbody.transform.position = newPosition; 
        transform.eulerAngles = new Vector3(0, 0, Vector2.Angle(Vector2.up, _movementDirection));
        if (Length > _tailLength + 1)
        {
            IncrementLength(position);
        }
        else
        {
            _tail.Move(position, transform.rotation);
        }
    }

    private void UpdateMovementDirection()
    {
        var horDirection = Input.GetAxis("Horizontal");
        var vertDirection = Input.GetAxis("Vertical");
        
        var currentMovementDirection = transform.rotation * Vector2.up;
        if (Mathf.Abs(horDirection) > 0.1f && Mathf.Abs(currentMovementDirection.x) <  0.1)
        {
            _movementDirection = horDirection > 0 ? Vector2.right : Vector2.left;
        }
        else if (Mathf.Abs(vertDirection) > 0.1f &&  Mathf.Abs(currentMovementDirection.y) < 0.1)
        {
            _movementDirection = vertDirection > 0 ? Vector2.up : Vector2.down;
        }
    }

    private void IncrementLength(Vector3 position)
    {
        var obj = Instantiate(TailPrefab, position, Quaternion.identity);
        var newSegment = obj.GetComponent<MovingPart>();
        newSegment._child = _tail;
        _tail = newSegment;
        _tailLength++;
    }
}
