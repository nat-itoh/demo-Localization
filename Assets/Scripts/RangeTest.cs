using UnityEngine;
using System;

public class RangeTest : MonoBehaviour {
    
    void Start() {

        var array = new string[] { "a", "b", "c", "d", "e", "f"};


        Index index1 = 1;
        Index index2 = ^1;

        Debug.Log($"array[1] = {array[index1]}");
        Debug.Log($"array[^1] = {array[index2]}");
    }

}
