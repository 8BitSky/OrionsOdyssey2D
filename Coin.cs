using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable, ICollectable{


    [SerializeField] private int value = 1;
    [SerializeField] private float valueModifier = 1.2f;

    [SerializeField] public int Value { get; set; }

    public override void Init()
    {
        base.Init();
        Value = value;
        if (value != 1)
        {
            Vector3 transformScale = transform.localScale;
            transformScale.y *= valueModifier;
            transformScale.x *= valueModifier;
            transform.localScale = transformScale;
        }
    }

    public void Collect()
    {
        audioManager.Play("CoinPickup");
        animator.SetBool("collected", true);
        UIManager.Instance.UpdateScore(Value);
    }

}
