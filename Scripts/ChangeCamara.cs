using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamara : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera_1;
    public CinemachineVirtualCamera VirtualCamera_2;

    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            VirtualCamera_1.Priority = 5;
            VirtualCamera_2.Priority = 10;
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            VirtualCamera_1.Priority = 10;
            VirtualCamera_2.Priority = 5;
        }
    }
}
