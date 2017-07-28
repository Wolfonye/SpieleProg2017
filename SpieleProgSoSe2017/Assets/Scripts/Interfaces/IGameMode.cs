﻿/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Soll sicherstellen, dass ein GameModus bestimmte Funktionen zur Verfügung stellt wie z.B. mitzuteilen, ob er sich im RundenCooldown
 * befindet. Das ist vorerst der Hauptgrund. Hintergrund ist, dass ich eine losere Kopplung zB mit dem Cam-Movement haben möchte.
 * Ich habe keine Lust immer den CamMovement-Code anzupassen, wenn ich einen neuen SPielmodus hinzufüge. Vielleicht kann man
 * da an ein paar anderen Stellen noch weiter abstrahieren.
 */
public interface IGameMode {
	bool isInCoolDown ();
}
