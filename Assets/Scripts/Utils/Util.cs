using UnityEngine;

public static class Util
{
    public static T FindChild<T>(GameObject go, string name) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        foreach (T child in go.GetComponentsInChildren<T>())
        {
            if (child.name == name)
                return child;
        }

        return null;
    }
}
