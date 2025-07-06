using System;
using UnityEditor;
using UnityEngine;

namespace KalyoncuBusiness.ScriptableObjects
{
    public class ScriptableObjectIdAttribute : PropertyAttribute
    {
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ScriptableObjectIdAttribute))]
    public class ScriptableObjectIdDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;

            float widthSize = position.width / 4;
            float offsetSize = 2;

            Rect pos1 = new Rect(position.x, position.y, widthSize * 3, position.height);
            Rect pos2 = new Rect(position.x + widthSize * 3 + offsetSize, position.y, widthSize, position.height);

            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = Guid.NewGuid().ToString();
            }
            EditorGUI.PropertyField(pos1, property, label, true);
            GUI.enabled = true;
            if (GUI.Button(pos2, "Copy Id"))
            {
                TextEditor te = new TextEditor();
                te.text = property.stringValue;
                te.SelectAll();
                te.Copy();
            }
        }
    }
#endif

    [System.Serializable]
    public class BaseScriptableObject : ScriptableObject
    {
        [ScriptableObjectId]
        public string id;
    }

}