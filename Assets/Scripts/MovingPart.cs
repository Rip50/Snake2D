using UnityEngine;

public class MovingPart : MonoBehaviour
{
    public MovingPart _child = null;
    private Rigidbody2D _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 position, Quaternion rotation, float speed)
    {
        var currentPosition = transform.position;
        var currentRotation = transform.rotation;
        
        if (_child != null)
        {
            _child.Move(currentPosition, currentRotation, speed);
        }
        _rigidbody.MovePosition(position);
        _rigidbody.MoveRotation(rotation);
    }
    
    public void CreateChildPart(GameObject prefab, Vector3 positionOffset)
    {
        if (_child == null)
        {
            var obj = Instantiate(prefab, transform);
            obj.transform.localPosition = positionOffset;
            var childNode = obj.GetComponent<MovingPart>();
            _child = childNode;
        }
        else
        {
            _child.CreateChildPart(prefab, positionOffset);
        }
    }
}
