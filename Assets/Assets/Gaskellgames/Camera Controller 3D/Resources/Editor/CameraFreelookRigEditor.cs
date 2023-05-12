#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

using Gaskellgames;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames.CameraController3D
{
    [CustomEditor(typeof(CameraFreelookRig))] [CanEditMultipleObjects]
    public class CameraFreelookRigEditor : Editor
    {
        #region Serialized Properties / OnEnable

        SerializedProperty GizmosOnSelected;

        SerializedProperty follow;
        SerializedProperty topRig;
        SerializedProperty middleRig;
        SerializedProperty bottomRig;

        SerializedProperty cameraCollisions;
        SerializedProperty lockCursorOnLoad;
        SerializedProperty xSensitivity;
        SerializedProperty ySensitivity;
        SerializedProperty rotationOffset;
        SerializedProperty collisionOffset;

        bool collisionGroup = false;

        private void OnEnable()
        {
            GizmosOnSelected = serializedObject.FindProperty("GizmosOnSelected");

            follow = serializedObject.FindProperty("follow");
            topRig = serializedObject.FindProperty("topRig");
            middleRig = serializedObject.FindProperty("middleRig");
            bottomRig = serializedObject.FindProperty("bottomRig");

            cameraCollisions = serializedObject.FindProperty("cameraCollisions");
            lockCursorOnLoad = serializedObject.FindProperty("lockCursorOnLoad");
            xSensitivity = serializedObject.FindProperty("xSensitivity");
            ySensitivity = serializedObject.FindProperty("ySensitivity");
            rotationOffset = serializedObject.FindProperty("rotationOffset");
            collisionOffset = serializedObject.FindProperty("collisionOffset");
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region OnInspectorGUI

        public override void OnInspectorGUI()
        {
            // get & update references
            CameraFreelookRig cameraFreelookRig = (CameraFreelookRig)target;
            serializedObject.Update();

            /*
            // draw default inspector
            base.OnInspectorGUI();
            */

            // banner
            Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Gaskellgames/Camera Controller 3D/Resources/Icons/inspectorBanner_CameraController3D.png", typeof(Texture));
            GUILayout.Box(banner, GUILayout.ExpandWidth(true), GUILayout.Height(Screen.width / 7.5f));

            // custom inspector
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(GizmosOnSelected);
            EditorGUILayout.PropertyField(lockCursorOnLoad);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(cameraCollisions);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(follow);
            EditorGUILayout.PropertyField(topRig);
            EditorGUILayout.PropertyField(middleRig);
            EditorGUILayout.PropertyField(bottomRig);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(xSensitivity);
            EditorGUILayout.PropertyField(ySensitivity);
            EditorGUILayout.PropertyField(rotationOffset);

            collisionGroup = cameraCollisions.boolValue;
            if(collisionGroup)
            {
                EditorGUILayout.PropertyField(collisionOffset);
            }

            // apply reference changes
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}

#endif
