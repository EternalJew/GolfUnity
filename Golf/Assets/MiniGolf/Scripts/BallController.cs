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
    private bool canShoot = false, isBallStatic = true;
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
    void Start()
    {
        CameraFollow.instance.SetTarget(gameObject);
    }
    public void MouseDownMethod()
    {
        startPos = ClickedPoint();
        lineRenderer.gameObject.SetActive(true);
        lineRenderer.SetPosition(0, lineRenderer.transform.localPosition);
    }

    public void MouseNormalMethod()
    {
        endPos = ClickedPoint();
        endPos.y = lineRenderer.transform.position.y;
        force = Mathf.Clamp(Vector3.Distance(endPos, startPos) * forceModifier, 0, maxForce);
        lineRenderer.SetPosition(1, transform.InverseTransformPoint(endPos));
    }

    public void MouseUpMethod()
    {
        canShoot = true;
        lineRenderer.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(rigidBody.velocity == Vector3.zero && !isBallStatic)
        {
            isBallStatic = true;
            rigidBody.angularVelocity = Vector3.zero;
            areaAffector.SetActive(true);
        }
    }

    private void FixedUpdate() 
    {
        if(canShoot)
        {
            canShoot = false;
            isBallStatic = false;
            direction = startPos - endPos;
            rigidBody.AddForce(direction * force, ForceMode.Impulse);
            //Fix this. Area affector hide when we make shot
            areaAffector.SetActive(false);
            force = 0;
            startPos = endPos = Vector3.zero;
            isBallStatic = false;
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
