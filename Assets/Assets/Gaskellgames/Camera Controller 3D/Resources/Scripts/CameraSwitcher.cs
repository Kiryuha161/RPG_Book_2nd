using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using Gaskellgames;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames.CameraController3D
{
    public class CameraSwitcher : MonoBehaviour
    {
        #region Variables

        private InputActionsGaskellgames inputActionsGaskellgames;
        
        [Space] [SerializeField] private CameraBrain cameraBrain;
        [Tooltip("Use the global camera list")]
        [SerializeField] private bool useRegisteredList = true;
        [Space] [SerializeField] private List<CameraRig> customCameraRigsList;

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Reset / Debug [Editor]

#if UNITY_EDITOR

        private void Reset()
        {
            if (GetComponent<CameraBrain>() != null)
            {
                cameraBrain = GetComponent<CameraBrain>();
            }
        }

#endif

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Game loop

        private void Awake()
        {
            inputActionsGaskellgames = new InputActionsGaskellgames();
        }

        private void OnEnable()
        {
            inputActionsGaskellgames.Enable();
        }

        private void OnDisable()
        {
            inputActionsGaskellgames.Disable();
        }

        private void Update()
        {
            if (inputActionsGaskellgames.Universal.InputButton3.WasPerformedThisFrame())
            {
                SwitchToNextCamera();
            }
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Private Functions

        private void SwitchToNextCamera()
        {
            if(cameraBrain != null)
            {
                List<CameraRig> registeredCameras = CameraList.GetCameraRigList();

                if (useRegisteredList && registeredCameras.Count >= 2)
                {
                    SelectNextCamera(registeredCameras);
                }
                else if(customCameraRigsList.Count >= 2)
                {
                    SelectNextCamera(customCameraRigsList);
                }
            }
        }

        private void SelectNextCamera(List<CameraRig> cameraList)
        {
            CameraRig active = cameraBrain.GetActiveCamera();
            int activeIndex = -1;

            for (int i = 0; i < cameraList.Count; i++)
            {
                if (cameraList[i] == active)
                {
                    activeIndex = i;
                }
            }

            if (activeIndex != -1)
            {
                if (activeIndex == cameraList.Count - 1)
                {
                    activeIndex = 0;
                }
                else
                {
                    activeIndex++;
                }
                cameraBrain.SetActiveCamera(cameraList[activeIndex]);
            }
            else
            {
                cameraBrain.SetActiveCamera(cameraList[0]);
            }
        }

        #endregion


    } // class end
}


