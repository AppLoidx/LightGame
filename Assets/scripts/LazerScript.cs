using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    public float _laserLength;
    public LayerMask _border;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Vector3 endPosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y) * _laserLength;

        RaycastHit2D[] hits = new RaycastHit2D[1];

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Enemy"));
        filter.useTriggers = true;

        // the colliders hit will be stored in hits
        int collidersHit = Physics2D.Raycast(transform.position, transform.right, filter, hits);
        if (collidersHit > 0)
        {
            _lineRenderer.SetPositions(new Vector3[] { transform.position, transform.up });
        }
        else
        {
            _lineRenderer.SetPositions(new Vector3[] { transform.position, endPosition });
        }
    }
}
