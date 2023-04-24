using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTargetEvent : MonoBehaviour
{
    public GameObject[] UIItems;
    public void TargetFound()
    {
        foreach (var item in UIItems)
        {
            StartCoroutine(item.GetComponent<ARUIContrl>().PlayUIAni());
        }
    }

    public void TargetLost()
    {
        foreach (var item in UIItems)
        {
            item.GetComponent<ARUIContrl>().InitUI();
        }
    }
}
