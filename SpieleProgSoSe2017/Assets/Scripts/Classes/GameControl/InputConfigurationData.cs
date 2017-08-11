using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Serialisierbares Datenobjekt zur Persistierung von Controls; komplementiert InputConfiguration
//Ich bin so frech und lasse die ganzen DInger public; Hintergrund ist folgender: wesentlich weniger
//Code; weiterhin habe ich nciht vor die Objekte lange rumfliegen zu lassen. Und schlussendlich
//scheint sich die halbe Unity-Architektur darum nicht sonderlich zu kümmern, was irgendwie verständlich
//ist, da der ANwendungskontext sehr spezifisch ist und die wenigsten Sachen vermutlich wiederverwendet
//werden. Weiterhin überwiegt hier teils vermutlich das Argument der Code-Lesbarkeit.
[System.Serializable]
public class InputConfigurationData {
	public string fireKey;
	public string leftJumpKey;
	public string rightJumpKey;
	public string leftJumpKeyAlt;
	public string rightJumpKeyAlt;
	public string overviewKey;
	public string camModeKey;
	public string pauseMenuKey;

	//das sollte später mit der Maus passieren!!!
	public string barrelUpKey;
	public string barrelDownKey;
}
