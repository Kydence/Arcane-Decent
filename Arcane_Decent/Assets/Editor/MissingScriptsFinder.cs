#if UNITY_EDITOR
using Codice.Client.Common;
using UnityEditor;
using UnityEngine;

public static class MissingScriptsFinder
{
    [MenuItem("Tools/Find Missing Scripts in Scene")]
    public static void Find()
    {
        int count = 0;
        foreach (GameObject go in Object.FindObjectsOfType<GameObject>())
        {
            Component[] components = go.GetComponents<Component>();

            foreach (Component c in components)
            {
                if (c == null)
                {
                    Debug.LogWarning($"Missing script on: {GetPath(go)}");
                    count++;
                }
            }
        }

        Debug.Log($"Search complete. Missing scripts found: {count}");
    }

    [MenuItem("Tools/Find Missing Scripts in Prefabs")]
    public static void FindInPrefabs()
    {
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        int count = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;

            Component[] components = prefab.GetComponentsInChildren<Component>(true);

            foreach (Component c in components)
            {
                if (c == null)
                {
                    Debug.LogWarning($"[Prefab] Missing script in: {path}", prefab);
                    count++;
                }
            }
        }

        Debug.Log($"[Prefabs] Search complete. Missing scripts found: {count}");
    }

    private static string GetPath(GameObject go)
    {
        return go.transform.parent == null ? go.name : GetPath(go.transform.parent.gameObject) + "/" + go.name;
    }
}
#endif