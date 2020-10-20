/*Source file name: PlayerController.cs
 * Student Name: Catt Symonds
 * Student ID: 101209214
 * Date Last Modified: 20/10/2020
 * Description: Controls player
 * Revision history:
 * 20/10/2020: made player move up and down instead of left and right
 */

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BulletManager bulletManager;

    // CHANGED to vertical boundary
    [Header("Boundary Check")]
    public float verticalBoundary;

    // CHANGED to vertical values
    [Header("Player Speed")]
    public float verticalSpeed;
    public float maxSpeed;
    public float verticalTValue;

    [Header("Bullet Firing")]
    public float fireDelay;

    // Private variables
    private Rigidbody2D m_rigidBody;
    private Vector3 m_touchesEnded;

    // Start is called before the first frame update
    void Start()
    {
        m_touchesEnded = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
        _FireBullet();
    }

     private void _FireBullet()
    {
        // delay bullet firing 
        if(Time.frameCount % 60 == 0 && bulletManager.HasBullets())
        {
            bulletManager.GetBullet(transform.position);
        }
    }

    private void _Move()
    {
        float direction = 0.0f;

        // touch input support 
        // CHANGED to detect touch ABOVE or BELOW instead of EITHER SIDE of the player sprite
        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            if (worldTouch.y > transform.position.y)
            {
                // direction is positive
                direction = 1.0f;
            }

            if (worldTouch.y < transform.position.y)
            {
                // direction is negative
                direction = -1.0f;
            }

            m_touchesEnded = worldTouch;

        }

        
        // keyboard support
        // CHANGED to detect vertical axis
        if (Input.GetAxis("Vertical") >= 0.1f) 
        {
            // direction is positive
            direction = 1.0f;
        }

        if (Input.GetAxis("Vertical") <= -0.1f)
        {
            // direction is negative
            direction = -1.0f;
        }

        if (m_touchesEnded.x != 0.0f)
        {
            // CHANGED to move on y axis instead of x
           transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, m_touchesEnded.y, verticalTValue));
        }
        else
        {
            Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * verticalSpeed, 0.0f);
            m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
            m_rigidBody.velocity *= 0.99f;
        }
    }

    // CHANGED to check y position instead of x position
    private void _CheckBounds()
    {
     
        // check right bounds
        if (transform.position.y >= verticalBoundary)
        {
            transform.position = new Vector3(transform.position.x, verticalBoundary, 0.0f);
        }

        // check left bounds
        if (transform.position.y <= -verticalBoundary)
        {
            transform.position = new Vector3(transform.position.x, -verticalBoundary, 0.0f);
        }

    }
}
