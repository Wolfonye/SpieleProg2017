using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellDOT : MonoBehaviour {
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
		int dmg = 20;
		for(int j = 0; j < 1; j++){
			//Debug.Log (j);
			radius = radius + start;	
			hitColliders = Physics.OverlapSphere(center, radius);
			while (i < hitColliders.Length)
			{
				//Debug.Log (hitColliders [i].tag);
				if (hitColliders[i].tag == "Base"){
					//Debug.Log("base gefunden");
					var Parent = hitColliders[i].transform.parent;
					//Debug.Log("show me the parent" + Parent);
					//Debug.Log(null);
					while(Parent != null){
						//Debug.Log("ebene hoch");
						if(Parent.tag == "Tank"){
							//Debug.Log("gefunden");
							Parent.GetComponent<EffectScript> ().DoTCounter += effectDuration;
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
