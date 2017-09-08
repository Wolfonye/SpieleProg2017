using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSpeedDown : MonoBehaviour {
	public float Radius = 12f;
	public int effectDuration = 31;

	private void OnCollisionEnter(Collision collision)
	{
		ExplosionDamage(Radius);
	}

	void ExplosionDamage(float start){
		Collider[] hitColliders;
		Vector3 center = transform.position;
		float radius = 0f;
		int i = 0;
		for(int j = 0; j < 1; j++){
			//Debug.Log (j);
			radius = radius + start;	
			hitColliders = Physics.OverlapSphere(center, radius);
			while (i < hitColliders.Length)
			{
				//Debug.Log (hitColliders [i].tag);
				if (hitColliders[i].tag == "Base"){
					var Parent = hitColliders[i].transform.parent;
					while(Parent != null){
						if(Parent.tag == "Tank"){
							Parent.GetComponent<EffectScript> ().SlowCounter += effectDuration;
						}
						Parent = Parent.transform.parent;
					}
				}
				i++;
			}
			i = 0;
		}
	}
}
