namespace UNet.Inputs.Abstractions
{
    public interface IInputService
    {
        bool GetButtonDown(string buttonName);
        bool GetButton(string buttonName);
        bool GetButtonUp(string buttonName);
        float GetAxis(string axisName);
        Vector2D GetVector(string axisName);
    }
}