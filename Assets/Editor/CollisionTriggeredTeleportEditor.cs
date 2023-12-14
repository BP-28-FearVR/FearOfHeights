using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CollisionTriggeredTeleport))]
public class CollisionTriggeredTeleportEditor : Editor
{
    SerializedProperty _transformParent;

    SerializedProperty _vectorType;

    SerializedProperty _teleportVector;

    SerializedProperty detectUsing;

    SerializedProperty _collidingTag;
    
    SerializedProperty _collidingLayer;

    private void OnEnable()
    {
        _transformParent = serializedObject.FindProperty("_transformParent");
        _vectorType = serializedObject.FindProperty("_vectorType");
        _teleportVector = serializedObject.FindProperty("_teleportVector");
        detectUsing = serializedObject.FindProperty("detectUsing");
        _collidingTag = serializedObject.FindProperty("_collidingTag");
        _collidingLayer = serializedObject.FindProperty("_collidingLayer");
    }

    public override void OnInspectorGUI()
    {
        CollisionTriggeredTeleport collisionTriggeredTeleport = (CollisionTriggeredTeleport)target;

        serializedObject.Update();

        using (new EditorGUI.DisabledScope(true)) EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), GetType(), false);

        EditorGUILayout.PropertyField(_transformParent);
        EditorGUILayout.PropertyField(_vectorType);
        EditorGUILayout.PropertyField(_teleportVector);
        EditorGUILayout.PropertyField(detectUsing);

        if (collisionTriggeredTeleport.detectUsing == CollisionTriggeredTeleport.DetectUsing.Tag)
        {
            EditorGUILayout.PropertyField(_collidingTag);
        }
        else
        {
            EditorGUILayout.PropertyField(_collidingLayer);
        }

        serializedObject.ApplyModifiedProperties();
    }
}