#if UNITY_EDITOR

using _1.Scripts.Islands;
using UnityEditor;
using UnityEngine;

namespace _1.Scripts._Editor.Ilands
{
    [CustomEditor(typeof(IslandPart))]
    public class IslandColliders : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            var myScript = (IslandPart)this.target;
            if (GUILayout.Button("Check count")) //8
            {
                myScript.CheckCollidersCount();
            }
            
            if (GUILayout.Button("Min/Max")) //8
            {
                myScript.MinMax();
            }
            
            if (GUILayout.Button("Size Z")) //8
            {
                myScript.PrintSizes();
            }

            EditorGUILayout.Space(25f);
            
            if (GUILayout.Button("Delete")) //8
            {
                myScript.Colliders();
            }
            
            if (GUILayout.Button("Delete by size")) //8
            {
                myScript.DeleteSize();
            }

            EditorGUILayout.Space(25f);
            
            if (GUILayout.Button("Apply Zero")) //8
            {
                myScript.ApplyZero();
            }
        }
    }
}

#endif