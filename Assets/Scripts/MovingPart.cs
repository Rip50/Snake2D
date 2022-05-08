using System;
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

    public void Move(Vector2 position, Quaternion rotation)
    {
        var currentPosition = transform.position;
        var currentRotation = transform.rotation;
        
        if (_child != null)
        {
            _child.Move(currentPosition, currentRotation);
        }
        _rigidbody.MovePosition(position);
        _rigidbody.MoveRotation(rotation);
    }
}
