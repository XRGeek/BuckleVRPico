using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerRotation : MonoBehaviour
{
    private InputDevice rightController;

    [SerializeField] private float damping = 5f;

    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        if (devices.Count > 0)
        {
            rightController = devices[0];
        }
    }

    void Update()
    {
        if (rightController.isValid && rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation))
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, damping * Time.deltaTime);
        }
    }
}
