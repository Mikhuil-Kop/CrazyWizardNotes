using System;
using UNet.Core.Abstractions;
using UNet.Inputs.Abstractions;
using UnityEngine;

namespace UNet.Inputs.Realization
{
    public class CameraInputService : IPointerInputService
    {
        private ICameraService CameraService { get; }

        public CameraInputService(ICameraService cameraService)
        {
            CameraService = cameraService;
        }

        public Vector2D CameraPosition => CameraService.CameraPosition;

        public Vector2D Pointer => CameraService.Pointer;

        public Vector2D ScreenPointer => CameraService.ScreenPointer;

        public IActor[] PointedTargets => CameraService.PointedTargets;

        public IActor[] ScreenPointedTargets => CameraService.ScreenPointedTargets;

        public bool GetButtonDown(string buttonName)
        {
            if (buttonName == "mouse")
            {
                return Input.GetMouseButtonDown(0);
            }
            else
            {
                return Input.GetButtonDown(buttonName);
            }
        }

        public bool GetButton(string buttonName)
        {
            if (buttonName == "mouse")
            {
                return Input.GetMouseButton(0);
            }
            else
            {
                return Input.GetButton(buttonName);
            }
        }

        public bool GetButtonUp(string buttonName)
        {
            if (buttonName == "mouse")
            {
                return Input.GetMouseButtonUp(0);
            }
            else
            {
                return Input.GetButtonUp(buttonName);
            }
        }

        public float GetAxis(string axisName)
        {
            return Input.GetAxis(axisName);
        }

        public Vector2D GetVector(string axisName)
        {
            var x = Input.GetAxis(axisName + "_X");
            var y = Input.GetAxis(axisName + "_Y");

            var vec = new Vector2D(x, y);

            return vec / Math.Max(vec.Magnitude, 1f);
        }
    }
}