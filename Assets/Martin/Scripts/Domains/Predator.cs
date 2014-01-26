using UnityEngine;
using System.Collections;

public class Predator : Domain {

	public override DomainEnum EDomain{get{return DomainEnum.BodyAttachment;}}

	// Use this for initialization
	void Start () {
		foreach (Transform trans in GetComponentsInChildren<Transform>()) {
			trans.tag = "Predator";
		}
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision coll) {
		if(coll.gameObject.tag != "Predator" && 
		   coll.gameObject.tag != "Ground" &&
		   coll.rigidbody != null &&
		   !coll.rigidbody.isKinematic){
			StartCoroutine(KillResizeRelocate(coll.gameObject));
			
		}
	
	}

	private IEnumerator KillResizeRelocate(GameObject obj){
		yield return new WaitForEndOfFrame();
		//find a body
		while(obj.GetComponent<Body>() == null){
			if(obj.transform.parent == null)
				return false;
			obj = obj.transform.parent.gameObject;
		}
		//save size
		obj.transform.parent = null; //place in worldspace
		Vector3 startSize = obj.transform.localScale;
		float startTime = Time.time;
		float duration = 0.5f;
		
		while(obj.transform.localScale.magnitude>0.1f){
			yield return new WaitForEndOfFrame();
			obj.transform.localScale = Vector3.Lerp(startSize,Vector3.zero,(Time.time-startTime)/duration);
		}
		obj.transform.position = obj.transform.position + Vector3.up*30 + Vector3.right *100;
		yield return new WaitForEndOfFrame();
		obj.transform.localScale = startSize;
	}
}
