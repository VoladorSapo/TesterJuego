using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ImageDictionary))]
public class DictionaryScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (((ImageDictionary)target).modifyvalues)
        {
            if (GUILayout.Button("Save changes"))
            {
                ((ImageDictionary)target).DeserializeDictionary();
            }

        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        if (GUILayout.Button("Print Dictionary"))
        {
            ((ImageDictionary)target).PrintDictionary();
        }
    }
}
