using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumelonMng : MonoBehaviour
{
    private Numelon num;
    // Start is called before the first frame update
    void Start()
    {
        num = new Numelon();
        Debug.Log(num.GetAns());
        Debug.Log(num.CheckAns(123));
        Debug.Log(num.CheckAns(456));
        Debug.Log(num.CheckAns(789));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
