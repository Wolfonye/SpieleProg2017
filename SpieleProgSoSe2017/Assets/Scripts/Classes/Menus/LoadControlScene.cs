/*
 *Author: Katya Engelmann
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadControlScene : MonoBehaviour {

	public void LoadByIndex (int sceneIndex) {
		/* Debug-Notiz (Philipp an Käthe)
		 * War so frech diese sceneIndex-Abfrage in dein Skript zu packen; scheinbar funktioniert im UNity-Editor
		 * das laut Doc eigentlich stattindende Beenden von COroutinen beim Szenenwechsel nicht perfekt,
		 * sodass ich da öfter Sachen referenziere, die nicht mehr existieren, weil die Coroutinen wohl
		 * zu spät beendet werden. Keines der entsprechenden Probleme trat im Build auf, da scheint es zu arbeiten
		 * wie die Doc es vermuten lässt. Leider konnte ich nicht alles über meinen Code regeln; das hier ist
		 * so ziemlich die einzige Stelle, wo ich sicherstellen kann, dass die Coroutine bzgl Gamemode ordentlich
		 * "gecancelt" werden; scheint jedenfalls zu funktionieren. Editor spinnt jetzt nciht mehr rum.
		 * So richtig logisch ist das alles nicht. Mag aber damit zusammenhängen, dass im Editor einige Sachen
		 * nebenher passieren, die beim "reinen" Spiel nicht stattfinden
		 */
		if(sceneIndex == 0){
			ActiveObjects.setActiveGameMode(null);
		}
		SceneManager.LoadScene (sceneIndex);
	}
}
