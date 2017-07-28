using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//nicht in Nutzung (der Damagekram ist jetzt hauptsächlcih Flos Sache)
public interface IDamageable<T>{
    void receiveDamage(T damageAmount);
}
