using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        var snake = col.gameObject.GetComponent<SnakeController>();
        if (snake != null)
        {
            snake.Length++;
        }
        Destroy(gameObject);
    }
}