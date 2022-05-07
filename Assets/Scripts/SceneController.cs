using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _applePrefab;
    [SerializeField] public BoxCollider2D _field;
    public SnakeController _snakeController;
    private Bounds _bounds;
    
    private GameObject _apple = null;

    private void Awake()
    {
        _bounds = _field.bounds;
    }

    private void Update()
    {
        if (_apple != null) return;
        
        var x = Random.Range(_bounds.min.x, _bounds.max.x);
        var y = Random.Range(_bounds.min.y, _bounds.max.y);

        x = Mathf.Floor(x) + 0.5f;
        y = Mathf.Floor(y) + 0.5f;
            
        _apple = Instantiate(_applePrefab, new Vector3(x, y, -1.0f), Quaternion.identity);
    }

    public void LooseTheGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
