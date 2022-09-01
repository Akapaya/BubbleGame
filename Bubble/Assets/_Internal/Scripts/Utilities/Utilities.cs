using UnityEngine;
public static class Utilities
{
    public static bool CheckingApproximately(this Vector3 objA, Vector3 objB, float maxDiference)
    {
        if (objA.y > objB.y - maxDiference && objA.y < objB.y + maxDiference)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
