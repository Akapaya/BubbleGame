using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private GameObject _winPanel;

    public delegate void UpdateLevelNumberHandler(int value);
    public static UpdateLevelNumberHandler UpdateLevelNumberEvent;

    public delegate void WinPanelHandler();
    public static WinPanelHandler WinPanelEvent;

    private void OnEnable()
    {
        WinPanelEvent += WinPanel;
        UpdateLevelNumberEvent += UpdateLevelNumber;
    }
    private void OnDisable()
    {
        WinPanelEvent -= WinPanel;
        UpdateLevelNumberEvent -= UpdateLevelNumber;
    }
    private void UpdateLevelNumber(int value)
    {
        _levelText.text = "LEVEL: " + value.ToString("00");
    }
    private void WinPanel()
    {
        _winPanel.SetActive(true);
    }
}
