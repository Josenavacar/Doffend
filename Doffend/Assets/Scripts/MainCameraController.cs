using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    private float movementSpeed = 0.03675f;
    public float maxXDistance = 4;
    public float maxYDistance = 0;
    [Range(2, 12)]
    public int zoomFactor = 6;
    public GameObject player;
    private Vector2 targetPosition;

    private Camera _cameraComponent;
    private int _actualZoomFactor;

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

        Vector2 newPos = Vector2.Lerp(transform.position, targetPosition, movementSpeed);
        // TODO: Fix the setPosition to work properly, for now we just smoothly interpolate.
        // setPosition(newPos);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        
        _actualZoomFactor = (int)Mathf.Lerp(_actualZoomFactor, zoomFactor, movementSpeed);

        _cameraComponent.orthographicSize = _calculateCameraSize();
    }

    private float _calculateCameraSize()
    {
        return 1.5f;
    }
}
