using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [Header("Prefabs")]
    [SerializeField] private GameObject _cube;

    [Header("Transforms")]
    [SerializeField] private Transform _spawn;

    private int _speed;
    private int _distance;
    private int _pauseTime;

    private GameObject _currentCube;
    private Vector3 _direction;

    public void StartSimulate()
    {
        _speed = UIManager.Instance.Speed;
        _distance = UIManager.Instance.Distance;
        _pauseTime = UIManager.Instance.PauseTime;
        _direction = new Vector3(1, 0, 0);

        UIManager.Instance.CloseStartPanel();

        CreateCube(_speed, _distance, _pauseTime);
    }

    public void Reload()
    {
        StopAllCoroutines();
        Destroy(_currentCube?.gameObject);
        UIManager.Instance.OpenStartPanel();
    }

    private void CreateCube(int speed, int distance, int pauseTime)
    {

        StartCoroutine(Simulate(speed, distance, pauseTime));
    }

    private IEnumerator Simulate(int speed, int distance, int pauseTime)
    {
        _currentCube = Instantiate(_cube, _spawn);
        var currentDistance = 0;

        while (currentDistance < distance)
        {
            _currentCube.transform.Translate(_direction * speed * Time.deltaTime);
            currentDistance = (int)Vector3.Distance(_spawn.position, _currentCube.transform.position);
            yield return null;
        }

        Destroy(_currentCube);
        StartCoroutine(Pause(speed, distance, pauseTime));
    }

    private IEnumerator Pause(int speed, int distance, int pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
        CreateCube(speed, distance, pauseTime);
    }
}
