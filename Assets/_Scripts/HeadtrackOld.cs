using UnityEngine;
using System.Collections;
using System.IO;
using Kinect = Windows.Kinect;
using System.Collections.Generic;
public class HeadtrackOld : MonoBehaviour
{
     public Transform head;

    public float OffsetX;
    public float OffsetY;
    public float OffsetZ;
    public float MultiplyX = 1f;
    public float MultiplyY = 1f;
    public float MultiplyZ = 1f;

    void Update()
    {

        if(head != null){
            Vector3 jointPos = head.position;
            float NewOffsetX = jointPos.x * MultiplyX + OffsetX;
            float NewOffsetY = jointPos.y * MultiplyY + OffsetY;
            float NewOffsetZ = jointPos.z*MultiplyZ + OffsetZ;
            //float NewOffsetZ = OffsetZ;  // no tracking for Z
            //Debug.Log(jointPos);
            //transform.position = new Vector3(NewOffsetX, NewOffsetY, NewOffsetZ);
            transform.position = new Vector3(NewOffsetX, NewOffsetY, transform.position.z);
        }

    }

    // public KinectInterop.JointType joint = KinectInterop.JointType.Head;
    // public float OffsetX;
    // public float OffsetY;
    // public float OffsetZ;
    // public float MultiplyX = 1f;
    // public float MultiplyY = 1f;
    // public float MultiplyZ = 1f;

    // void Update()
    // {

    //     KinectManager manager = KinectManager.Instance;

    //     if (manager && manager.IsInitialized())
    //     {
    //         if (manager.IsUserDetected())
    //         {
    //             long userId = manager.GetPrimaryUserID();

    //             if (manager.IsJointTracked(userId, (int)joint))
    //             {
                    
    //                 Vector3 jointPos = manager.GetJointPosition(userId, (int)joint);
    //                 float NewOffsetX = jointPos.x * MultiplyX + OffsetX;
    //                 float NewOffsetY = jointPos.y * MultiplyY + OffsetY;
    //                 float NewOffsetZ = jointPos.z*MultiplyZ + OffsetZ;
    //                 //float NewOffsetZ = OffsetZ;  // no tracking for Z
    //                 //Debug.Log(jointPos);
    //                 //transform.position = new Vector3(NewOffsetX, NewOffsetY, NewOffsetZ);
    //                 transform.position = new Vector3(NewOffsetX, NewOffsetY, transform.position.z);
    //             }
    //         }
    //     }
    // }
}
