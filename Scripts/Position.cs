using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{

    //gizmos are basically things you want to see while editing the game but not while you're playing it
    private void OnDrawGizmos()
    {
        //parameters for gizmos are gizmo's center and it's radius
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
