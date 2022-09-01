using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateHexagon : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private bool _canHit = true;
    [SerializeField] private Vector3 _targetPosition = Vector3.zero;
    [SerializeField] private LayerMask _layersTargets;
    [SerializeField] private List<Color32> _colors;

    public delegate void DisableHitHandler();
    public static DisableHitHandler DisableHitEvent;

    public delegate void EnableHitHandler();
    public static EnableHitHandler EnableHitEvent;

    public void OnHit(InputValue value)
    {
        HitInput(value.isPressed);
    }

    private void OnEnable()
    {
        DisableHitEvent += DisableHit;
        EnableHitEvent += EnableHit;
    }

    private void OnDisable()
    {
        DisableHitEvent -= DisableHit;
        EnableHitEvent -= EnableHit;
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        SetColorBubbles();
    }

    private void HitInput(bool HitState)
    {
        if (_canHit)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,_layersTargets) && hit.collider.gameObject == this.gameObject)
            {
                _targetPosition = transform.eulerAngles + new Vector3(0, 60, 0);
                StartCoroutine(Rotate());
                RotateHexagon.DisableHitEvent?.Invoke();
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

    IEnumerator Rotate()
    {
        transform.Rotate(0, 0, 1);
        yield return new WaitForSeconds(0);
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
            StartCoroutine(Rotate());
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
