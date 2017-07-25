using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellDestruction : MonoBehaviour {

	private List<IDestructionObserver> destructionObservers;

	//Ich muss mir irgendwie am Anfang die Spielelemente ziehen, die gerne notified würden, wenn die Shell explodiert wird
	//bissl unschön noch...
	void Start(){
		destructionObservers = new List<IDestructionObserver>();
		GameObject gameMaster2000 = GameObject.FindWithTag ("Gamemaster2000");
		destructionObservers.Add((TempRoundTimer)gameMaster2000.GetComponent<TempRoundTimer>());
	}

    //Joa...die Shell muss weg...
    private void OnCollisionEnter(Collision collision)
    {
		foreach (IDestructionObserver destructionObserver in destructionObservers) {
			destructionObserver.destructionObserved (gameObject);
		}
        Destroy(gameObject);
    }
		
	public void registerDestructionObserver(IDestructionObserver observer){
		destructionObservers.Add (observer);
	}
}
