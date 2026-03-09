using UnityEngine;

public class SniperZoom : MonoBehaviour
{
    public Camera mainCamera;
    public float normalFOV = 60f;
    public float zoomedFOV = 15f;
    public float zoomSpeed = 10f;

    public GameObject crosshair;
    public GameObject scopeOverlay;

    private bool isZoomed = false;
    private float targetFOV;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        targetFOV = normalFOV;
        mainCamera.fieldOfView = normalFOV;

        if (scopeOverlay != null)
        {
            scopeOverlay.SetActive(false);
        }
    }

    void Update()
    {
        // Clic droit pour viser
        if (Input.GetButtonDown("Fire2"))
        {
            isZoomed = !isZoomed;

            if (isZoomed)
            {
                targetFOV = zoomedFOV;
                if (crosshair != null) crosshair.SetActive(false);
                if (scopeOverlay != null) scopeOverlay.SetActive(true);
            }
            else
            {
                targetFOV = normalFOV;
                if (crosshair != null) crosshair.SetActive(true);
                if (scopeOverlay != null) scopeOverlay.SetActive(false);
            }
        }

        // Zoom
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
    }
}