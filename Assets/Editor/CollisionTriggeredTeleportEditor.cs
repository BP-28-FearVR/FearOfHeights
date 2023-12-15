using UnityEditor;
using UnityEngine;

// Changes the Inspector Editor of the CollisionTriggeredTeleport Class
[CustomEditor(typeof(CollisionTriggeredTeleport))]
public class CollisionTriggeredTeleportEditor : Editor
{
    SerializedProperty detectUsing;

    SerializedProperty _collidingTag;

    SerializedProperty _collidingLayer;

    SerializedProperty _transformParent;

    SerializedProperty _vectorType;

    SerializedProperty _teleportVector;

    // OnEnable is called when the GameObject is loaded
    private void OnEnable()
    {
        // Find serialized Properties and store them
        detectUsing = serializedObject.FindProperty("detectUsing");
        _collidingTag = serializedObject.FindProperty("_collidingTag");
        _collidingLayer = serializedObject.FindProperty("_collidingLayer");
        _transformParent = serializedObject.FindProperty("_transformParent");
        _vectorType = serializedObject.FindProperty("_vectorType");
        _teleportVector = serializedObject.FindProperty("_teleportVector");
    }

    // OnInspectorGUI specifies the way the Inspector Editor should be drawn
    public override void OnInspectorGUI()
    {
        // Get the current instance of CollisionTriggeredTeleport (target only exists in this context)
        CollisionTriggeredTeleport collisionTriggeredTeleport = (CollisionTriggeredTeleport)target;

        serializedObject.Update();

        // Generate the non-editable script reference a normal script component has
        using (new EditorGUI.DisabledScope(true)) EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), GetType(), false);

        // Generate the following fields in the Inspector Editor
        EditorGUILayout.PropertyField(detectUsing);

        // Show either Tag or Layer depending on the choosen Detection type
        if (collisionTriggeredTeleport.detectUsing == CollisionTriggeredTeleport.DetectUsing.Tag)
        {
            EditorGUILayout.PropertyField(_collidingTag);
        }
        else
        {
            EditorGUILayout.PropertyField(_collidingLayer);
        }
        EditorGUILayout.PropertyField(_transformParent);
        EditorGUILayout.PropertyField(_vectorType);
        EditorGUILayout.PropertyField(_teleportVector);
        
        serializedObject.ApplyModifiedProperties();
    }
}