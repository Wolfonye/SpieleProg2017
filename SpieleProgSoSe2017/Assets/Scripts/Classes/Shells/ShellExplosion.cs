using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellExplosion : MonoBehaviour {
	public float Radius = 2.4f;

	private void OnCollisionEnter(Collision collision)
	{
		ExplosionDamage(Radius);
	}

	void ExplosionDamage(float start){
		Collider[] hitColliders;
		Vector3 center = transform.position;
		float radius = 0f;
		int i = 0;
		int dmg = 20;
		for(int j = 0; j < 5; j++){
			radius = radius + start;	
			hitColliders = Physics.OverlapSphere(center, radius);
			while (i < hitColliders.Length)
			{
				Debug.Log (hitColliders [i].gameObject.transform.root.tag);
				if (hitColliders[i].gameObject.transform.root.tag == "Tank"){
					hitColliders [i].gameObject.transform.root.GetComponent<TakeDamage> ().TakeExplosion (dmg);
				}
				i++;
			}
		}
	}
}
