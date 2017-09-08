using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellHOT : MonoBehaviour {
	public float Radius = 12f;
	public int effectDuration = 30;

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
						Debug.Log("ebene hoch");
						if(Parent.tag == "Tank"){
							Debug.Log("gefunden");
							Parent.GetComponent<EffectScript> ().HoTCounter += effectDuration;
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
