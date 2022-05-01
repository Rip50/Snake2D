using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeController : MonoBehaviour
{
    [SerializeField] private GameObject Tail;
    
    public float Speed = 0.5f;
    public float StepSize = 1.0f;
    public int Length = 2;

    private Rigidbody2D _rigidbody;
    private TailSegment _tail;
    private Vector2 _movementDirection;
    private Vector2 _desiredMovementDirection;
    private float _nextStepTime;
    private float _stepTime = 0;

    private void Start()
    {
        _desiredMovementDirection = Vector2.up;
        _movementDirection = _desiredMovementDirection;
        _nextStepTime = 1/Speed;
        _rigidbody = GetComponent<Rigidbody2D>();
        CreateTail();
    }

    private void CreateTail()
    {
        var currentPosition = transform.position;
        TailSegment lastTailSegment = null;
        for (var i = 1; i < Length; i++)
        {
            var positionIncrement = _movementDirection * StepSize * i;
            var x = currentPosition.x - positionIncrement.x;
            var y = currentPosition.y - positionIncrement.y;
            var tailSegmentPosition = new Vector3(x, y, currentPosition.z);
            var tailObject = Instantiate(Tail, tailSegmentPosition, Quaternion.identity);
            var tail = tailObject.GetComponent<TailSegment>();
            if (_tail == null)
            {
                _tail = tail;
            }
            else
            {
                lastTailSegment.NextTailSegment = tail;
            }
            lastTailSegment = tail;
        }
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
        _stepTime -= _nextStepTime;
        
        CreateTailSegment();
        var length = CalculateSnakeLength();
        if (length >= Length)
        {
            TrimTail();
        }

        MoveHead();
    }

    private int CalculateSnakeLength()
    {
        var length = 1;
        var currentSegment = _tail;
        do
        {
            length++;
            currentSegment = currentSegment.NextTailSegment;
        } while (currentSegment.NextTailSegment != null);

        return length;
    }

    private void TrimTail()
    {
        var lastSegment = _tail;
        while (lastSegment.NextTailSegment != null)
        {
            lastSegment = lastSegment.NextTailSegment;
        }
        lastSegment.DestroySegment();
    }

    private void MoveHead()
    {
        var positionIncrement = _desiredMovementDirection * StepSize;
        _rigidbody.MovePosition(new Vector2(transform.position.x, transform.position.y) + positionIncrement);
        _movementDirection = _desiredMovementDirection;
    }

    private void UpdateMovementDirection()
    {
        var horDirection = Input.GetAxis("Horizontal");
        var vertDirection = Input.GetAxis("Vertical");
        var direction = _movementDirection;
        if (Mathf.Abs(horDirection) > 0.1f)
        {
            direction = horDirection > 0 ? Vector2.right : Vector2.left;
        }
        else if (Mathf.Abs(vertDirection) > 0.1f)
        {
            direction = vertDirection > 0 ? Vector2.up : Vector2.down;
        }

        var product = Vector2.Dot(direction, _movementDirection);
        if (product == 0)
        {
            _desiredMovementDirection = direction;
        }
    }

    private void CreateTailSegment()
    {
        var tailObject = Instantiate(Tail, transform.position, Quaternion.identity);
        var tail = tailObject.GetComponent<TailSegment>();
        tail.NextTailSegment = _tail;
        _tail = tail;
    }
}
