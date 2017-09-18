/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//nicht in Nutzung (der Damagekram ist jetzt hauptsächlcih Flos Sache); Ich wollte damit ursprünglich ein gut erweiterbares Schadenskonzept aufbauen.
public interface IDamageable<T>{
    void receiveDamage(T damageAmount);
}
