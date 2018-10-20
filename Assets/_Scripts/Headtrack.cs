using System.Collections;
using System.IO;
using UnityEngine;
using Kinect = Windows.Kinect;
using System.Collections.Generic;
public class Headtrack : MonoBehaviour {

    public CalebBodySourceView bodySourceView;

    public Transform[] HandsAndFeet;


    public float OffsetX;
    public float OffsetY;
    public float OffsetZ;
    public float MultiplyX = 1f;
    public float MultiplyY = 1f;
    public float MultiplyZ = 1f;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject> ();
    private BodySourceManager _BodyManager;

    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType> () { { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft }, { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft }, { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft }, { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },

        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight }, { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight }, { Kinect.JointType.KneeRight, Kinect.JointType.HipRight }, { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },

        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft }, { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft }, { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft }, { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft }, { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft }, { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },

        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight }, { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight }, { Kinect.JointType.HandRight, Kinect.JointType.WristRight }, { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight }, { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight }, { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },

        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid }, { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder }, { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck }, { Kinect.JointType.Neck, Kinect.JointType.Head },
    };

    void Update () {
        // if (BodySourceManager == null) {
        //     return;
        // }

        // _BodyManager = BodySourceManager.GetComponent<BodySourceManager> ();
        // if (_BodyManager == null) {
        //     return;
        // }

        // Kinect.Body[] data = _BodyManager.GetData ();
        // if (data == null) {
        //     return;
        // }

        // List<ulong> trackedIds = new List<ulong> ();
        // foreach (var body in data) {
        //     if (body == null) {
        //         continue;
        //     }

        //     if (body.IsTracked) {
        //         trackedIds.Add (body.TrackingId);
        //     }
        // }

        // List<ulong> knownIds = new List<ulong> (_Bodies.Keys);

        // // First delete untracked bodies
        // foreach (ulong trackingId in knownIds) {
        //     if (!trackedIds.Contains (trackingId)) {
        //         Destroy (_Bodies[trackingId]);
        //         _Bodies.Remove (trackingId);
        //     }
        // }

        for(int i = 1; i<5; i++){
            HandsAndFeet[i-1].position = bodySourceView.bodyParts[i];
        }

        Vector3 jointPos = bodySourceView.bodyParts[0];
        float NewOffsetX = jointPos.x * MultiplyX + OffsetX;
        float NewOffsetY = jointPos.y * MultiplyY + OffsetY;
        float NewOffsetZ = jointPos.z*MultiplyZ + OffsetZ;
        //float NewOffsetZ = OffsetZ;  // no tracking for Z
        //Debug.Log(jointPos);
        //transform.position = new Vector3(NewOffsetX, NewOffsetY, NewOffsetZ);
        transform.position = new Vector3(NewOffsetX, NewOffsetY, transform.position.z);




    }


    // public Vector3 getHeadPosition(){

    //     Kinect.Body[] data = _BodyManager.GetData ();
    //     if (data == null) {
    //         return Vector3.zero;
    //     }

    //     foreach (var body in data) {
    //         if (body == null) {
    //             continue;
    //         }

    //         if (body.IsTracked) {
    //             //Debug.Log(GetVector3FromJoint(body.Joints[Kinect.JointType.Head]));
    //             return GetVector3FromJoint(body.Joints[Kinect.JointType.Head]);
    //             // if (!_Bodies.ContainsKey (body.TrackingId)) {
    //             //     _Bodies[body.TrackingId] = CreateBodyObject (body.TrackingId);
    //             // }

    //             // RefreshBodyObject (body, _Bodies[body.TrackingId]);
    //         }
    //     }
    //     return Vector3.zero;
    // }


    private static Vector3 GetVector3FromJoint (Kinect.Joint joint) {
        return new Vector3 (joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
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