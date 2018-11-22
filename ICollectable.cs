using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable {

    int Value { get; set; }
    void Collect();

}
