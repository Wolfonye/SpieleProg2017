using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetKeyButtonTexts : MonoBehaviour {
	public Text	moveLeft;
	public Text moveRight;
	public Text jumpUp;
	public Text jumpDown;
	public Text fire;
	public Text barrelUp;
	public Text barrelDown;
	public Text slowBarrelMovement;
	public Text cycleCamMode;
	public Text overview;
	public Text pauseScreen;

	public void setKeyTexts(){
		moveLeft.text = InputConfiguration.driveLeftKey;
		moveRight.text = InputConfiguration.driveRightKey;
		jumpUp.text = InputConfiguration.leftJumpKey;
		jumpDown.text = InputConfiguration.rightJumpKey;
		fire.text = InputConfiguration.fireKey;
		barrelUp.text = InputConfiguration.barrelUpKey;
		barrelDown.text = InputConfiguration.barrelDownKey;
		slowBarrelMovement.text = InputConfiguration.slowBarrelMovementKey;
		cycleCamMode.text = InputConfiguration.camModeKey;
		overview.text = InputConfiguration.overviewKey;
		pauseScreen.text = InputConfiguration.pauseMenuKey;
	}
}
