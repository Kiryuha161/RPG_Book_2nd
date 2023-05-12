using System;
using UnityEngine;

using Gaskellgames;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames.CameraController3D
{
    public class CameraBrain : MonoBehaviour
    {
        #region Variables

        [SerializeField] private CameraRig activeCamera;
        [ReadOnly, SerializeField] private CameraRig previousCamera;
        [ReadOnly, SerializeField] private Transform follow;
        [ReadOnly, SerializeField] private Transform lookAt;
        [ReadOnly, SerializeField] private CameraLens lens;
        [ReadOnly, SerializeField] private CameraOrbits topRig;
        [ReadOnly, SerializeField] private CameraOrbits middleRig;
        [ReadOnly, SerializeField] private CameraOrbits bottomRig;
        private CameraRig activeCameraCheck;
        private Camera cam;

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Game Loop

        private void Reset()
        {
            if (gameObject.GetComponent<Camera>() != null)
            {
                cam = GetComponent<Camera>();
                activeCameraCheck = null;
            }
        }

        private void Start()
        {
            cam = GetComponent<Camera>();

            if (activeCamera != null)
            {
                previousCamera = activeCamera;
            }
        }

        private void LateUpdate()
        {
            UpdateCamera();
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Editor / Debug

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                UpdateCamera();
            }
        }

#endif

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Functions

        private void UpdateCamera()
        {
            if (activeCamera != null)
            {
                if (activeCameraCheck != activeCamera)
                {
                    if (cam == null)
                    {
                        cam = GetComponent<Camera>();
                    }
                    else
                    {
                        CameraLens tempLens = activeCamera.GetComponent<CameraRig>().GetCameraLens();
                        cam.fieldOfView = tempLens.verticalFOV;
                        cam.nearClipPlane = tempLens.nearClipPlane;
                        cam.farClipPlane = tempLens.farClipPlane;
                        cam.cullingMask = tempLens.cullingMask;

                        activeCameraCheck = activeCamera;
                    }
                }
                else
                {
                    transform.position = activeCamera.transform.position;
                    transform.rotation = activeCamera.transform.rotation;
                }
            }
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Getters / Setters

        public CameraRig GetActiveCamera() { return activeCamera; }

        public void SetActiveCamera(CameraRig newActiveCamera)
        {
            if (activeCamera != newActiveCamera)
            {
                previousCamera = activeCamera;
            }
            activeCamera = newActiveCamera;
        }

        public CameraRig GetPreviousCamera() { return previousCamera; }

        public void SetPreviousCamera() { activeCamera = previousCamera; }

        #endregion

    } // class end
}
