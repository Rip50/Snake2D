using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _applePrefab;
    [SerializeField] public BoxCollider2D _field;
    
    private Bounds _bounds;
    
    private GameObject _apple = null;

    private void Awake()
    {
        _bounds = _field.bounds;
    }

    private void Update()
    {
        if (_apple != null) return;

        Vector2 position;
        RaycastHit2D hit;
        do
        {
            position = GetRandomPositionOnTheField();
            hit = Physics2D.Raycast(position, Vector2.up);
            if (hit && hit.collider != _field)
                Debug.Log($"Object found: {hit.transform.gameObject.name}");
        } while (hit && hit.collider != _field);

        _apple = Instantiate(_applePrefab, new Vector3(position.x, position.y, -1.0f), Quaternion.identity);
        
    }

    private Vector2 GetRandomPositionOnTheField()
    {
        var x = Random.Range(_bounds.min.x, _bounds.max.x);
        var y = Random.Range(_bounds.min.y, _bounds.max.y);

        x = Mathf.Floor(x) + 0.5f;
        y = Mathf.Floor(y) + 0.5f;

        return new Vector2(x, y);
    }

    public void LooseTheGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
