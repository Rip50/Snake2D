using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Obstacle : MonoBehaviour
{
    private SceneController _controller;

    private void Awake()
    {
        _controller = FindObjectOfType<SceneController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var snake = col.gameObject.GetComponent<SnakeController>();
        if (snake != null)
        {
            _controller.LooseTheGame();
        }
    }
}