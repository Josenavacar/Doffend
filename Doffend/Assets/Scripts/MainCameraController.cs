using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    private float movementSpeed = 0.03675f;
    public int zoomFactor = 6;

    public GameObject player;
    private Vector2 targetPosition;

    private Camera _cameraComponent;
    private int _actualZoomFactor;

    //enable and set the max y value
    public bool YMaxEnabled = false;
    public float YMaxValue = 0;
    //enable and set the min y value
    public bool YMinEnabled = false;
    public float YMinValue = 0;
    //enable and set the max X value
    public bool XMaxEnabled = false;
    public float XMaxValue = 0;
    //enable and set the min X value
    public bool XMinEnabled = false;
    public float XMinValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        _actualZoomFactor = zoomFactor;
        targetPosition = transform.position;
        _cameraComponent = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        targetPosition = player.transform.position;

        //vertical clamp
        if (YMinEnabled && YMaxEnabled)
            targetPosition.y = Mathf.Clamp(targetPosition.y, YMinValue, YMaxValue);

        else if (YMinEnabled)
            targetPosition.y = Mathf.Clamp(targetPosition.y, YMinValue, targetPosition.y);

        else if (YMaxEnabled)
            targetPosition.y = Mathf.Clamp(targetPosition.y, targetPosition.y, YMaxValue);

        //horizontal clamp
        if (XMinEnabled && XMaxEnabled)
            targetPosition.x = Mathf.Clamp(targetPosition.x, XMinValue, XMaxValue);

        else if (YMinEnabled)
            targetPosition.x = Mathf.Clamp(targetPosition.x, XMinValue, targetPosition.x);

        else if (YMaxEnabled)
            targetPosition.x = Mathf.Clamp(targetPosition.x, targetPosition.x, XMaxValue);

        Vector2 newPos = Vector2.Lerp(transform.position, targetPosition, movementSpeed);
        // TODO: Fix the setPosition to work properly, for now we just smoothly interpolate.
        // setPosition(newPos);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        
        _actualZoomFactor = (int)Mathf.Lerp(_actualZoomFactor, zoomFactor, movementSpeed);

        _cameraComponent.orthographicSize = _calculateCameraSize();
    }

    private float _calculateCameraSize()
    {
        return 2f;
    }
}
