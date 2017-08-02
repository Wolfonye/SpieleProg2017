/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Eine Shell oder irgendeine andere Art von "Schuss" soll mit mitteilen können, wenn er zerstört wurde, wobei
 * man hier vielleicht allgemiener hätte sagen können, wenn "die Phase des Absetzens" abgeschlossen ist.
 * Eine Mine hat ja nciht wirklich eine Flugzeit, ein Schuss schon, eine Mine könnte das also direkt mitteilen
 * eine Shell sollte das wirklich erst, nachdem sie explodiert ist.
 * Aber das war am Anfang noch nicht so geplant, daher die etwas enge Namensgebung.
 * Ich muss mal schauen, obs Tools gibt, die das schnell refactorn können. Visual Studio köntne es vermutlich
 * aaaber meine Version ist abgelaufen und ich gurke mit Monodevelop rum -.-
 * */
public class ShellDestruction : MonoBehaviour {

	private List<IDestructionObserver> destructionObservers;

	// wichtig, dass das im Awake steht, sonst hagelts NullPointerExceptions :D
	void Awake(){
		destructionObservers = new List<IDestructionObserver>();
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
