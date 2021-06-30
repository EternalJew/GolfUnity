using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
   [SerializeField] private float rotationSpeed = .2f;
   public static CameraRotation instance;
   private void Update()
   {
       
       RotateCamera(Input.GetAxis("Mouse X"));
   }
   public void RotateCamera(float xAxisRotation)
   {
       transform.Rotate(Vector3.down, -xAxisRotation * rotationSpeed);
   }
}
