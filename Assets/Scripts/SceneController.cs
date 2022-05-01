using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _applePrefab;
    private GameObject _apple;
    private void Update()
    {
        if (_apple == null)
        {
            //Instantiate(_applePrefab);
        }
    }
}
