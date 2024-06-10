using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject testMob;

    public void MobReset()
    {
        testMob.transform.position = Vector3.zero;
    }
}
