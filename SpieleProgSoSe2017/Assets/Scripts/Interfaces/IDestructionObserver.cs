using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructionObserver {
	void destructionObserved (GameObject destructedObject);
}
