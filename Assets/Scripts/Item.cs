using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        var snake = col.GetComponent<SnakeController>();
        if (snake == null) return;
        
        snake.IncrementLength();
        snake.Speed += 0.1f;
        
        Destroy(this.gameObject);
    }
}