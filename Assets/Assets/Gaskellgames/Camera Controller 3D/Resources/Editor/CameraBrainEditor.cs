#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

using Gaskellgames;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames.CameraController3D
{
    [CustomEditor(typeof(CameraBrain))] [CanEditMultipleObjects]
    public class CameraBrainEditor : Editor
    {
        #region Serialized Properties / OnEnable

        SerializedProperty activeCamera;
        SerializedProperty previousCamera;
        SerializedProperty follow;
        SerializedProperty lookAt;
        SerializedProperty lens;
        SerializedProperty topRig;
        SerializedProperty middleRig;
        SerializedProperty bottomRig;

        bool InfoGroup = true;
        bool OrbitsGroup = true;

        private void OnEnable()
        {
            activeCamera = serializedObject.FindProperty("activeCamera");
            previousCamera = serializedObject.FindProperty("previousCamera");
            follow = serializedObject.FindProperty("follow");
            lookAt = serializedObject.FindProperty("lookAt");
            lens = serializedObject.FindProperty("lens");
            topRig = serializedObject.FindProperty("topRig");
            middleRig = serializedObject.FindProperty("middleRig");
            bottomRig = serializedObject.FindProperty("bottomRig");
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region OnInspectorGUI

        public override void OnInspectorGUI()
        {
            // get & update references
            CameraBrain cameraBrain = (CameraBrain)target;
            serializedObject.Update();

            /*
            // draw default inspector
            base.OnInspectorGUI();
            */

            // banner
            Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Gaskellgames/Camera Controller 3D/Resources/Icons/inspectorBanner_CameraController3D.png", typeof(Texture));
            GUILayout.Box(banner, GUILayout.ExpandWidth(true), GUILayout.Height(Screen.width / 7.5f));

            // custom inspector
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(activeCamera);
            EditorGUILayout.PropertyField(previousCamera);

            EditorGUILayout.Space();
            GUI.contentColor = Color.gray;
            InfoGroup = EditorGUILayout.BeginFoldoutHeaderGroup(InfoGroup, "Active CameraRig");
            if (InfoGroup)
            {
                GUI.contentColor = Color.white;
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(follow);
                EditorGUILayout.PropertyField(lookAt);
                EditorGUILayout.PropertyField(lens);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
            if (cameraBrain.GetActiveCamera().GetFreelookRig() != null)
            {
                GUI.contentColor = Color.gray;
                OrbitsGroup = EditorGUILayout.BeginFoldoutHeaderGroup(OrbitsGroup, "Camera Orbits");
                if (OrbitsGroup)
                {
                    GUI.contentColor = Color.white;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(topRig);
                    EditorGUILayout.PropertyField(middleRig);
                    EditorGUILayout.PropertyField(bottomRig);
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }



            // apply reference changes
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}

#endif
