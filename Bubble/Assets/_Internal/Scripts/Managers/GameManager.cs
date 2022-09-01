using UnityEngine;
using System.Threading.Tasks;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _platform;
    [SerializeField] private Vector3[][] _positions;
    [SerializeField] private int _numberHexagon = 2;
    [SerializeField] private Save _save;

    private Vector3 lastHexagon = Vector3.zero;

    public delegate void CheckIfWinHandler();
    public static CheckIfWinHandler CheckIfWinEvent;

    private void OnEnable()
    {
        CheckIfWinEvent += CheckIfWin;
    }
    private void OnDisable()
    {
        CheckIfWinEvent -= CheckIfWin;
    }
    private void Start()
    {
        _numberHexagon = 2 * _save.Level;
        UIManager.UpdateLevelNumberEvent?.Invoke(_save.Level);
        Create();
    }

    private void Create()
    {
        int linhas = 0;
        for (int t = 0; t < _numberHexagon;)
        {
            for (int i = 0; i < linhas + 1; i++)
            {
                if (i == 0)
                {
                    lastHexagon = new Vector3(0 + ((1.75f * linhas)), 0, 0);
                }
                else
                {
                    lastHexagon = new Vector3(lastHexagon.x - 0.878f, lastHexagon.y, 0 + (1.5f * i));
                }
                GameObject gm = Instantiate(_platform[0], lastHexagon, Quaternion.Euler(-90, 0, 0));
                lastHexagon = gm.transform.position;
                t++;
                if (t == _numberHexagon)
                    break;
            }
            linhas++;
            
            
        }
    }

    private async void CheckIfWin()
    {
        await Task.Delay(1000);
        if (GameObject.FindWithTag("Sphere") == null)
        {
            UIManager.WinPanelEvent?.Invoke();
            _save.Level++;
        }
    }
}
