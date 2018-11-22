using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

    int StartingHealth{get; set;}
    void Damage();
}
