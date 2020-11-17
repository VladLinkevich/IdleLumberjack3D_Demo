using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class StoreController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CoinManager.instance.SellTree();
    }
}
