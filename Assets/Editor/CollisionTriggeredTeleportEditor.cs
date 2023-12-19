using UnityEditor;
using UnityEngine;

// Changes the Inspector Editor of the CollisionTriggeredTeleport Class
[CustomEditor(typeof(CollisionTriggeredTeleport))]
public class CollisionTriggeredTeleportEditor : Editor
{
    SerializedProperty detectUsing;

    SerializedProperty collidingTag;

    SerializedProperty collidingLayer;

    SerializedProperty transformParent;

    SerializedProperty vectorType;

    SerializedProperty teleportVector;

    // OnEnable is called when the GameObject is loaded
    private void OnEnable()
    {
        // Find serialized Properties and store them
        detectUsing = serializedObject.FindProperty("detectUsing");
        collidingTag = serializedObject.FindProperty("collidingTag");
        collidingLayer = serializedObject.FindProperty("collidingLayer");
        transformParent = serializedObject.FindProperty("transformParent");
        vectorType = serializedObject.FindProperty("vectorType");
        teleportVector = serializedObject.FindProperty("teleportVector");
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
            EditorGUILayout.PropertyField(collidingTag);
        }
        else
        {
            EditorGUILayout.PropertyField(collidingLayer);
        }
        EditorGUILayout.PropertyField(transformParent);
        EditorGUILayout.PropertyField(vectorType);
        EditorGUILayout.PropertyField(teleportVector);
        
        serializedObject.ApplyModifiedProperties();
    }
}