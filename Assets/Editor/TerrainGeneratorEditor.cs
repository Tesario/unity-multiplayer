using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var terrainGenerator = (TerrainGenerator)target;

        if (GUILayout.Button("Generate"))
        {
            terrainGenerator.GenerateTerrain();
        }
    }
}
