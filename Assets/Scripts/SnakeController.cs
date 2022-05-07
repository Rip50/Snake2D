using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeController : MonoBehaviour
{
    [SerializeField] private GameObject TailPrefab;

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
        var obj = Instantiate(TailPrefab, currentPosition + childPosition, transform.rotation);
        _tail = obj.GetComponent<MovingPart>();
        
        // Rest nodes
        for (var i = 2; i < Length; i++)
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

        var position = transform.position;
        MoveHead();
        _tail.Move(position, transform.rotation);
    }
    
    private void MoveHead()
    {
        var positionIncrement = _movementDirection * StepSize;
        var newPosition = new Vector2(transform.position.x, transform.position.y) + positionIncrement;
      
        _rigidbody.MovePosition(newPosition);
        transform.eulerAngles = new Vector3(0, 0, Vector2.Angle(Vector2.up, _movementDirection));
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

    public void IncrementLength()
    {
        var currentPosition = transform.position;
        currentPosition.z = 0;
        var positionIncrement = _movementDirection * StepSize;
        var x = - positionIncrement.x;
        var y = - positionIncrement.y;
        var childPosition = new Vector3(x, y, currentPosition.z);
        
        _tail.CreateChildPart(TailPrefab, childPosition);
    }
}
