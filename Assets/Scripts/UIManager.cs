using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

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

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField _speed;
    [SerializeField] private TMP_InputField _distance;
    [SerializeField] private TMP_InputField _pauseTime;

    [Header("Panels")]
    [SerializeField] private GameObject _startPanel;

    public int Speed => int.Parse(_speed.text.Length != 0 ? _speed.text : "0");
    public int Distance => int.Parse(_distance.text.Length != 0 ? _distance.text : "0");
    public int PauseTime => int.Parse(_pauseTime.text.Length != 0 ? _pauseTime.text : "0");


    public void CloseStartPanel()
    {
        StartPanelActive(false);
    }

    public void OpenStartPanel()
    {
        StartPanelActive(true);
    }

    private void StartPanelActive(bool active)
    {
        if (_startPanel.activeSelf == active)
        {
            return;
        }

        _startPanel.SetActive(active);
    }
}
