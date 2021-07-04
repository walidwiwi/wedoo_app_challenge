using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanable : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("work", .5f);
    }
    private void work() => appManager.panels_hanel.Add(gameObject);
}
