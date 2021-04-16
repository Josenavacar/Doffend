using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(BoxCollider2D))]
public class EntityController2D : MonoBehaviour
{
    public LayerMask collisionMask;

    public const float skinWidth = 0.015f;
    public int numHorizontalRays = 4;
    public int numVerticalRays = 4;

    private float horizontalRaySpacing;
    private float verticalRaySpacing;

    [SerializeField]
    private BoxCollider2D _collider;
    [SerializeField]
    private RaycastOrigins _raycastOrigins;
    [SerializeField]
    public CollisionDirection collisions;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _calculateRaySpacing();
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < numVerticalRays; ++i)
            Debug.DrawRay(_raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.red);
        

        for (int i = 0; i < numHorizontalRays; ++i)
            Debug.DrawRay(_raycastOrigins.topRight + Vector2.right * horizontalRaySpacing * i, Vector2.right * -2, Color.red);
    }

    public void move(Vector3 velocity)
    {
        // Update origins of rays.
        _updateRaycastOrigins();
        // Reset collision checks.
        collisions = 0;

        
        if (velocity.x != 0)
            _performHorizontalCollisions(ref velocity);

        if (velocity.y != 0)
            _performVerticalCollisions(ref velocity);

        _performMove(velocity);
    }

    private void _updateRaycastOrigins()
    {
        if (_collider == null)
        {
            Debug.LogWarning("Collider not set yet");
            return;
        }
        Bounds bounds = _collider.bounds;
        bounds.Expand(-2 * skinWidth);

        _raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        _raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        _raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        _raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
    }

    private void _calculateRaySpacing()
    {
        Bounds bounds = _collider.bounds;
        bounds.Expand(-2 * skinWidth);

        Assert.IsTrue(numHorizontalRays >= 2 && numVerticalRays >= 2);

        horizontalRaySpacing = bounds.size.y / (numHorizontalRays - 1);
        verticalRaySpacing = bounds.size.x / (numVerticalRays - 1);
    }

    private void _performHorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < numVerticalRays; ++i)
        {
            Vector2 rayOrigin = (directionX == -1) ? _raycastOrigins.bottomLeft : _raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                if (directionX == 1)
                    collisions |= CollisionDirection.cdRight;
                else
                    collisions |= CollisionDirection.cdLeft;
            }
        }
    }

    private void _performVerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < numVerticalRays; ++i)
        {
            Vector2 rayOrigin = (directionY == -1) ? _raycastOrigins.bottomLeft : _raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            if (hit)
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                if (directionY == 1)
                    collisions |= CollisionDirection.cdTop;
                else
                    collisions |= CollisionDirection.cdBottom;
            }
        }
    }

     

    protected virtual void _performMove(Vector3 velocity)
    {
        transform.Translate(velocity);
    }

    /// <summary>
    /// RaycastOrigins describes the four origin points we use to cast rays from.
    /// </summary>
    private struct RaycastOrigins
    {
        public Vector2 topLeft;
        public Vector2 topRight;
        public Vector2 bottomLeft;
        public Vector2 bottomRight;
    }

    [Flags]
    public enum CollisionDirection
    {
        cdLeft = 1 << 1,
        cdTop = 1 << 2,
        cdRight = 1 << 3,
        cdBottom = 1 << 4
    }
}
