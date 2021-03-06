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
 * Ein Gamemode ist insbesondere ein DestructionObserver, zumindest im Moment.
 * Ich glaube das kann und werde ich später rausrefactorn TODO
 */
public interface IGameMode:IDestructionObserver {
	//gibt true zurueck, wenn der angewählte Gamemode sich gerade im Rundencooldown befindet
	bool isInCoolDown ();
	//gibt die MODE_ID des jeweiligen Gamemode zurück
	string getModeID ();
	//gibt true zurueck, wenn der angesprochene Modus aktiv ist
	bool isModeEnabled();
	//tauscht zwischen enabled und nicht enabled eines Modes
	void toggleEnabled();
	//man soll dem Mode mitteilen können, wann der letzte Schuss gerade fliegt
	void setLastShotInTheAir(bool isActive);
	//der Mode soll reagieren, wenn der letzte Schuss zerstört wurde
	void lastShotExploded();
	//um von aussen ein Rundenende herbeizuführen unabhängig vom spezieleln Modus
	void initiateRoundEnd();
}
