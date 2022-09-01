using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _offSetX;
    [SerializeField] private float _offSetY;
    [SerializeField] private float _offSetZ;
    [SerializeField] private Save _save;
    void Start()
    {
        var totalX = 0f;
        var totalZ = 0f;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Hexagon");
        foreach (GameObject itObject in objects)
        {
            totalX += itObject.transform.position.x;
            totalZ += itObject.transform.position.z;
        }
        var centerX = totalX / objects.Length;
        var centerZ = totalZ / objects.Length;
        Camera.main.transform.position = new Vector3(centerX + _offSetX, centerX + centerZ + _offSetY, centerZ + _offSetZ);
    }
}
