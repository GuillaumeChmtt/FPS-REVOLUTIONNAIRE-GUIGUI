using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float normalSensitivityX = 10F;
    public float normalSensitivityY = 10F;
    public float zoomedSensitivityMultiplier = 0.3f; // 30% de sensibilité en visée

    public float sensitivityX = 10F;
    public float sensitivityY = 10F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;

    void Start()
    {
        // Initialise les sensibilités
        sensitivityX = normalSensitivityX;
        sensitivityY = normalSensitivityY;
    }

    void Update()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }

    // Fonction appelée par SniperZoom pour changer la sensibilité
    public void SetZoomed(bool zoomed)
    {
        if (zoomed)
        {
            sensitivityX = normalSensitivityX * zoomedSensitivityMultiplier;
            sensitivityY = normalSensitivityY * zoomedSensitivityMultiplier;
        }
        else
        {
            sensitivityX = normalSensitivityX;
            sensitivityY = normalSensitivityY;
        }
    }
}