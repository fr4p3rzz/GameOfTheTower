using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// This script is only used in order to generate maps that can be manually edited

[CustomEditor(typeof(APlaygroundGenerator), true)]

public class RandomLayoutGeneratorEditor : Editor
{
    APlaygroundGenerator generator;

    public void Awake()
    {
        generator = (APlaygroundGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Generate map layout"))
        {
            generator.GenerateLayout();
        }
    }


}
