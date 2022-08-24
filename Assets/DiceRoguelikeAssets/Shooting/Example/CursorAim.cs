using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;

public class CursorAim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
