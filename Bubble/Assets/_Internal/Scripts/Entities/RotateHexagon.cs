using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class RotateHexagon : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private bool _canHit = true;
    [SerializeField] private Vector3 _targetPosition = Vector3.zero;
    [SerializeField] private LayerMask _layersTargets;
    [SerializeField] private List<Color32> _colors;
    [SerializeField] private Inputs _inputs;


    private void Awake()
    {
        _inputs = new Inputs();
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _inputs.Touch.TouchHit.performed += ctx => HitInput(ctx);
        SetColorBubbles();
    }

    private void HitInput(InputAction.CallbackContext context)
    {
        if (_canHit)
        {
            Ray ray = _mainCamera.ScreenPointToRay(_inputs.Touch.TouchPosition.ReadValue<Vector2>());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _layersTargets) && hit.collider.gameObject == this.gameObject)
            {
                _targetPosition = transform.eulerAngles + new Vector3(0, 60, 0);
                GameObject[] hexagonsObjects = GameObject.FindGameObjectsWithTag("Hexagon");
                foreach (var item in hexagonsObjects)
                {
                    item.GetComponent<RotateHexagon>().DisableHit();
                }
                Rotate();

            }
        }
    }


    public void SetColorBubbles()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if(this.transform.GetChild(i).TryGetComponent(out Bubbles bubbles))
            {
                int r = Random.Range(0, _colors.Count-1);
                bubbles.SetColor(_colors[r]);
                _colors.RemoveAt(r);
            }
        }
    }

    private async void Rotate()
    {
        transform.Rotate(0, 0, 1 * (Time.deltaTime * 150));
        await Task.Delay(10);
        if (Utilities.CheckingApproximately(transform.eulerAngles, _targetPosition, 5))
        {
            transform.eulerAngles = _targetPosition;
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).TryGetComponent(out Bubbles bubbles))
                {
                    bubbles.CheckIfCombine();
                }
            }
        }
        else
        {
            Rotate();
            return;
        }
        await Task.Delay(700);
        GameObject[] hexagonsObjects = GameObject.FindGameObjectsWithTag("Hexagon");
        foreach (var item in hexagonsObjects)
        {
            item.GetComponent<RotateHexagon>().EnableHit();
        }
    }

    public void EnableHit()
    {
        _canHit = true;
    }

    public void DisableHit()
    {
        _canHit = false;
    }
}
