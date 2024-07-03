// https://stackoverflow.com/a/73162759/23660970

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class AdaptableSize 
    : LayoutElement, ILayoutElement 
{
#if UNITY_EDITOR
    [CustomEditor(typeof(AdaptableSize))]
    public class Drawer : Editor {
        public override void OnInspectorGUI() {

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));
            EditorGUI.EndDisabledGroup();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Preferred Height");
            var prop = serializedObject.FindProperty("usePreferredHeight");
            EditorGUILayout.PropertyField(prop, new GUIContent(""), GUILayout.Width(EditorGUIUtility.singleLineHeight));
            if(prop.boolValue) {
                var tmp = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 35;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("preferredHeightMax"), new GUIContent("Max"));
                EditorGUIUtility.labelWidth = tmp;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Preferred Width");
            prop = serializedObject.FindProperty("usePreferredWidth");
            EditorGUILayout.PropertyField(prop, new GUIContent(""), GUILayout.Width(EditorGUIUtility.singleLineHeight + 2));
            if(prop.boolValue) {
                var tmp = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 35;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("preferredWidthMax"), new GUIContent("Max"));
                EditorGUIUtility.labelWidth = tmp;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("contentToGrowWith"));
            if(EditorGUI.EndChangeCheck()) {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
#endif

    [SerializeField] private RectTransform contentToGrowWith;

    [SerializeField] private bool usePreferredHeight;
    [SerializeField] private float preferredHeightMax;
    [SerializeField] private bool usePreferredWidth;
    [SerializeField] private float preferredWidthMax;

    private float _preferredHeight;
    private float _preferredWidth;

    public override float preferredHeight => _preferredHeight;
    public override float preferredWidth => _preferredWidth;

    public override void CalculateLayoutInputVertical() 
    {
        if (contentToGrowWith == null) 
        {
            return;
        }
        
        if (usePreferredHeight) 
        {
            var height = LayoutUtility.GetPreferredHeight(contentToGrowWith);
            _preferredHeight = preferredHeightMax > height ? height : preferredHeightMax;
            
            var rect = ((RectTransform) transform);
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, _preferredHeight);
            SetDirty();
        } 
        else 
        {
            _preferredHeight = -1;
        }
    }

    public override void CalculateLayoutInputHorizontal() 
    {
        if (contentToGrowWith == null) 
        {
            return;
        }
        
        if (usePreferredWidth) 
        {
            var width = LayoutUtility.GetPreferredWidth(contentToGrowWith);
            _preferredWidth = preferredWidthMax > width ? width : preferredWidthMax;
            
            var rect = ((RectTransform) transform);
            rect.sizeDelta = new Vector2(_preferredWidth, rect.sizeDelta.y);
            SetDirty();
        } 
        else
        {
            _preferredWidth = -1;
        }
    }
}