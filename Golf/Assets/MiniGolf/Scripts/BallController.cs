using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public static BallController instance;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject areaAffector;
    [SerializeField] private float maxForce, forceModifier;
    [SerializeField] private LayerMask rayLayer;
    private float force;
    private Rigidbody rigidBody;

    private Vector3 startPos, endPos;
    private bool canShoot = false;
    private Vector3 direction;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        rigidBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !canShoot)
        {
            startPos = ClickedPoint();
            lineRenderer.gameObject.SetActive(true);
            lineRenderer.SetPosition(0, lineRenderer.transform.localPosition);
        }

        if(Input.GetMouseButton(0))
        {
            endPos = ClickedPoint();
            force = Mathf.Clamp(Vector3.Distance(endPos, startPos) * forceModifier, 0, maxForce);
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(endPos));
        }

        if(Input.GetMouseButtonUp(0))
        {
            canShoot = true;
            lineRenderer.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate() 
    {
        if(canShoot)
        {
            canShoot = false;
            direction = startPos - endPos;
            rigidBody.AddForce(direction * force, ForceMode.Impulse);
            areaAffector.SetActive(false);
            force = 0;
            startPos = endPos = Vector3.zero;
        }    
    }
    Vector3 ClickedPoint()
    {
        Vector3 position = Vector3.zero;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, rayLayer))
        {
            position = hit.point;
        }
        return position;
    }
}
