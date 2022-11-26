using UnityEditor;
using UnityEngine;

namespace EditorScripts.Inspector
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class SerializeReadOnlyAttribute : PropertyAttribute
    {
 
    }
 
    [CustomPropertyDrawer(typeof(SerializeReadOnlyAttribute))]
    public class SerializeReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
            GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
 
        public override void OnGUI(Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}