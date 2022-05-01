using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeController : MonoBehaviour
{
    [FormerlySerializedAs("Tail")] [SerializeField] private GameObject TailPrefab;
    
    public float Speed = 0.5f;
    public float StepSize = 1.0f;
    public int Length = 2;

    private Rigidbody2D _rigidbody;
    private MovingPart _tail;
    
    private Vector2 _movementDirection;
    private float _nextStepTime;
    private float _stepTime = 0;

    private void Start()
    {
        _movementDirection =  Vector2.up;
        _nextStepTime = 1/Speed;
        _rigidbody = GetComponent<Rigidbody2D>();
        CreateTail();
    }

    private void CreateTail()
    {
        var currentPosition = transform.position;
        currentPosition.z += 1;
        var positionIncrement = _movementDirection * StepSize;
        var x = - positionIncrement.x;
        var y = - positionIncrement.y;
        var childPosition = new Vector3(x, y, currentPosition.z);
        
        // First node
        var obj = Instantiate(TailPrefab, currentPosition, transform.rotation);
        _tail = obj.GetComponent<MovingPart>();
        
        // Rest nodes
        for (var i = 1; i < Length; i++)
        {
            _tail.CreateChildPart(TailPrefab, childPosition);
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

        var newPosition = MoveHead();
        _tail.Move(newPosition, transform.rotation, Speed);
    }
    
    private Vector2 MoveHead()
    {
        var positionIncrement = _movementDirection * StepSize;
        var newPosition = new Vector2(transform.position.x, transform.position.y) + positionIncrement;
        _rigidbody.MovePosition(newPosition);
        return newPosition;
    }

    private void UpdateMovementDirection()
    {
        var horDirection = Input.GetAxis("Horizontal");
        var vertDirection = Input.GetAxis("Vertical");
        if (Mathf.Abs(horDirection) > 0.1f)
        {
            _movementDirection = horDirection > 0 ? Vector2.right : Vector2.left;
        }
        else if (Mathf.Abs(vertDirection) > 0.1f)
        {
            _movementDirection = vertDirection > 0 ? Vector2.up : Vector2.down;
        }

        //var product = Vector2.Dot(direction, _movementDirection); //0 for 1/2Pi
    }
}
