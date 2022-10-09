using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEffect : MonoBehaviour
{
    [SerializeField] float remainTime;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(remainTime);
        Destroy(gameObject);
    }
}
