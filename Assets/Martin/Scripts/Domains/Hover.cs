using UnityEngine;
using System.Collections;

public class Hover : Domain {
	
	public override DomainEnum EDomain{get{return DomainEnum.BodyAttachmentSpecialMovement;}}

//	private LayerMask mask;

	private float minHeight;
	private float maxHeight;
	private float frequency;

	void Start(){
//		mask = ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
		minHeight = Random.Range(0,BodyPart.FindOffset(gameObject,Vector3.down,null,null).y);
		maxHeight = Random.Range(minHeight+0.4f,minHeight+8.4f);
		frequency = Random.Range(1f,5.0f);
//		foreach (Transform trans in gameObject.GetComponentsInChildren<Transform>()) {
//			trans.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
//		}
	}

	void FixedUpdate(){
		Vector3 target = Vector3.Lerp(Vector3.up*minHeight,Vector3.up*maxHeight,(Time.time%(2*frequency)<frequency?(Time.time%frequency)/frequency:1-(Time.time%frequency)/frequency));
		rigidbody.AddForce(0,(target.y - transform.position.y)-rigidbody.velocity.y,0);
	}

}
