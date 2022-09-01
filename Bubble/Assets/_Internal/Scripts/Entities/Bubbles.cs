using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bubbles : MonoBehaviour
{
    [SerializeField] private LayerMask _layersTargets;
    [SerializeField] private Color32 _color;
    
    public void CheckIfCombine()
    {
        Vector3 dir = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, dir * 50, Color.green);
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.8f, _layersTargets))
        {
            if (hit.collider.gameObject.TryGetComponent(out MeshRenderer meshRenderer))
            {
                if (meshRenderer.material.color == _color)
                {
                    hit.collider.gameObject.GetComponent<Animator>().Play("AutoDestroy");
                    this.gameObject.GetComponent<Animator>().Play("AutoDestroy");
                }
            }
        }
    }

    public void Destroy()
    {
        GameManager.CheckIfWinEvent?.Invoke();
        Destroy(this.gameObject);
    }

    public void SetColor(Color32 color)
    {
        if (TryGetComponent(out MeshRenderer meshRenderer))
        {
            meshRenderer.materials[0].color = color;
            _color = color;
        }
    }
}
