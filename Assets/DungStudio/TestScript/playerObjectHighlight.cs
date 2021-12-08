using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class playerObjectHighlight : MonoBehaviour
{
	//tag of object
	public string selectObject = "selectObject";

	//mainSelection: select the objectToDestination
	//tempSelection: for the raycast
	public Transform mainSelection, tempSelection, pullObjectDestiantion;
	//Choose only the layer point at and ignore everything else
	public LayerMask layerMask;
	public Camera camera;
	public RaycastHit hit;
	//highlightMat: when we hit raycast the object it will highlight
	//defautMat: change it back the material when not touch it
	public Color highlightMat, defautMat;

	//make sure the select is color
	public Renderer selectionRender;
	// switch to touch the object
	public bool pullObjectReady, lockRayCast, throwPowerActive;
	//raycastRange: range of raycast
	//powerThrow: throw power
	public float raycastRange, powerThrow, speedOfDrag;

	public Animator selectAnimation;

	public Rigidbody rigidbodyMain;

	public Transform[] objects;

	void Update()
	{
		//start of Update
		//not select not highlight
		if (mainSelection = null)
		{
			mainSelection = null;
		}

		// Raycast Highlight System
		// first create boolean to select highlight
		if (lockRayCast == false)
		{
			//Draw the raycast to the camera looking and always at forward
			//range of raycast
			//select certain layer
			if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, raycastRange, layerMask))
			{
				//See the raycast point at
				Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
				//Get the object we hit
				tempSelection = hit.collider.transform;
				selectionRender = tempSelection.GetComponent<Renderer>();
				rigidbodyMain = tempSelection.GetComponent<Rigidbody>();
				//find the tag of object raycast found
				if (tempSelection.tag == selectObject)
				{
					//find the render object will be highlight
					if (selectionRender != null)
					{
						selectionRender.material.color = highlightMat;
					}
					//all condition find it will be select
					mainSelection = tempSelection;
				}
			}
			//change it back to defaut Material
			if (mainSelection == null)
			{
				tempSelection = null;
				selectionRender.material.color = defautMat;
			}
		}// end of Raycast

		// select object and move
		if (Input.GetKeyDown(KeyCode.Mouse0) && tempSelection.tag == selectObject)
		{
			playerPickUpObject();
		}
		// drop object
		if (Input.GetKeyUp(KeyCode.Mouse0) && tempSelection != null && tempSelection.tag == selectObject)
		{
			playerLetGoObject();
		}
		// add force to object
		if (tempSelection.tag == selectObject && lockRayCast == false && throwPowerActive == true)
		{
			objectAddForce();
		}
		//pull object to destination
		if (pullObjectReady == true)
		{
			objectToDestination();
		}
	} // end of update

	public void playerPickUpObject()
	{

		pullObjectReady = true;
		lockRayCast = true;
		//Get Object Set Object
		tempSelection.transform.SetParent(pullObjectDestiantion);
		rigidbodyMain.useGravity = false;
		throwPowerActive = false;
	}

	public void playerLetGoObject()
	{
		rigidbodyMain.useGravity = true;
		pullObjectDestiantion.transform.DetachChildren();
		lockRayCast = false;
		throwPowerActive = true;
		pullObjectReady = false;
	}
	public void objectAddForce()
	{
		Vector3 pushDirection = tempSelection.transform.position - camera.transform.position;
		rigidbodyMain.AddForceAtPosition(pushDirection.normalized * powerThrow, pushDirection, ForceMode.Impulse);
		throwPowerActive = false;
	}
	public void objectToDestination()
	{
		tempSelection.localPosition = Vector3.Lerp(tempSelection.localPosition, pullObjectDestiantion.transform.localPosition, speedOfDrag);
	}
}