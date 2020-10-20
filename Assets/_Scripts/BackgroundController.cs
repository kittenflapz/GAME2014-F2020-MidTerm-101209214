/*Source file name: BackgroundController.cs
 * Student Name: Catt Symonds
 * Student ID: 101209214
 * Date Last Modified: 20/10/2020
 * Description: Controls the background scrolling and resetting for infinite background scroll effect
 * Revision history:
 * 20/10/2020: made it scroll sideways
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // CHANGED to horizontal values
    public float horizontalSpeed;
    public float horizontalBoundary;

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _Reset()
    {
        transform.position = new Vector3(horizontalBoundary, 0.0f);
    }

    private void _Move()
    {
        transform.position -= new Vector3(horizontalSpeed, 0.0f) * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        // CHANGED if the background is further left than the left of the screen then reset
        if (transform.position.x <= -horizontalBoundary)
        {
            _Reset();
        }
    }
}
