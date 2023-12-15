using UnityEditor;
using UnityEngine;

// Changes the Inspector Editor of the CollisionTriggeredEvent Class
[CustomEditor(typeof(CollisionTriggeredEvent))]
public class CollisionTriggeredEventEditor : Editor
{
    SerializedProperty detectUsing;

    SerializedProperty _collidingTag;

    SerializedProperty _collidingLayer;

    SerializedProperty _event;

    // OnEnable is called when the GameObject is loaded
    private void OnEnable()
    {
        // Find serialized Properties and store them
        detectUsing = serializedObject.FindProperty("detectUsing");
        _collidingTag = serializedObject.FindProperty("_collidingTag");
        _collidingLayer = serializedObject.FindProperty("_collidingLayer");
        _event = serializedObject.FindProperty("_event");
    }

    // OnInspectorGUI specifies the way the Inspector Editor should be drawn
    public override void OnInspectorGUI()
    {
        // Get the current instance of CollisionTriggeredEvent (target only exists in this context)
        CollisionTriggeredEvent collisionTriggeredEvent = (CollisionTriggeredEvent)target;

        serializedObject.Update();

        // Generate the non-editable script reference a normal script component has
        using (new EditorGUI.DisabledScope(true)) EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), GetType(), false);

        // Generate the following fields in the Inspector Editor
        EditorGUILayout.PropertyField(detectUsing);

        // Show either Tag or Layer depending on the choosen Detection type
        if (collisionTriggeredEvent.detectUsing == CollisionTriggeredEvent.DetectUsing.Tag)
        {
            EditorGUILayout.PropertyField(_collidingTag);
        }
        else
        {
            EditorGUILayout.PropertyField(_collidingLayer);
        }
        EditorGUILayout.PropertyField(_event);

        serializedObject.ApplyModifiedProperties();
    }
}