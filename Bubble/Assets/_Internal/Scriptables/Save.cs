using UnityEngine;
[CreateAssetMenu(fileName = "Scriptable", menuName = "Save")]
public class Save : ScriptableObject
{
    [SerializeField] private int _level = 1;
    public int Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }
}
