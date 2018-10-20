using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class CalebBodySourceView : MonoBehaviour {

    public Vector3 offset;
    public Vector3 multipliers;

    public bool render;

    public Vector3[] bodyParts = new Vector3[5];
    public Quaternion[] handRots = new Quaternion[2];

    public Material BoneMaterial;
    public GameObject BodySourceManager;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject> ();
    private BodySourceManager _BodyManager;

    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType> () { { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft }, { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft }, { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft }, { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },

        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight }, { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight }, { Kinect.JointType.KneeRight, Kinect.JointType.HipRight }, { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },

        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft }, { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft }, { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft }, { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft }, { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft }, { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },

        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight }, { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight }, { Kinect.JointType.HandRight, Kinect.JointType.WristRight }, { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight }, { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight }, { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },

        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid }, { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder }, { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck }, { Kinect.JointType.Neck, Kinect.JointType.Head },
    };

    void Update () {
        if (BodySourceManager == null) {
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<BodySourceManager> ();
        if (_BodyManager == null) {
            return;
        }

        Kinect.Body[] data = _BodyManager.GetData ();
        if (data == null) {
            return;
        }

        List<ulong> trackedIds = new List<ulong> ();
        foreach (var body in data) {
            bodyParts[0] = new Vector3(0,0,0);
            bodyParts[1] = new Vector3(0,0,0);
            bodyParts[2] = new Vector3(0,0,0);
            bodyParts[3] = new Vector3(0,0,0);
            bodyParts[4] = new Vector3(0,0,0);
            if (body == null) {
                continue;
            }

            if (body.IsTracked) {
                trackedIds.Add (body.TrackingId);
            }
        }

        List<ulong> knownIds = new List<ulong> (_Bodies.Keys);

        // First delete untracked bodies
        foreach (ulong trackingId in knownIds) {
            if (!trackedIds.Contains (trackingId)) {
                Destroy (_Bodies[trackingId]);
                _Bodies.Remove (trackingId);
            }
        }

        bool primaryFound = false;

        foreach (var body in data) {
            if (body == null) {
                continue;
            }

            if (body.IsTracked) {

                if (!primaryFound) {
                    bodyParts[0] = Vector3.Scale (GetVector3FromJoint (body.Joints[Kinect.JointType.Head]), multipliers) + offset ;
                    bodyParts[1] = transform.position + Vector3.Scale (GetVector3FromJoint (body.Joints[Kinect.JointType.HandLeft]), multipliers) + offset ;
                    bodyParts[2] = transform.position + Vector3.Scale (GetVector3FromJoint (body.Joints[Kinect.JointType.HandRight]), multipliers) + offset ;
                    bodyParts[3] = transform.position + Vector3.Scale (GetVector3FromJoint (body.Joints[Kinect.JointType.FootLeft]), multipliers) + offset;
                    bodyParts[4] = transform.position + Vector3.Scale (GetVector3FromJoint (body.Joints[Kinect.JointType.FootRight]), multipliers) + offset;
                    primaryFound = true;
                    Vector3 right = bodyParts[2]-(Vector3.Scale (GetVector3FromJoint (body.Joints[Kinect.JointType.ElbowRight]), multipliers) + offset);
                    handRots[1] =  Quaternion.LookRotation(right,Vector3.up);
                    Vector3 left = bodyParts[1]-(Vector3.Scale (GetVector3FromJoint (body.Joints[Kinect.JointType.ElbowLeft]), multipliers) + offset);
                    handRots[0] =  Quaternion.LookRotation(left,Vector3.up);

                }

                if (!_Bodies.ContainsKey (body.TrackingId)) {
                    _Bodies[body.TrackingId] = CreateBodyObject (body.TrackingId);
                }

                RefreshBodyObject (body, _Bodies[body.TrackingId]);
            }
        }
        if (!primaryFound) {
            bodyParts[0] = new Vector3(0,0,0);
            bodyParts[1] = new Vector3(0,0,0);
            bodyParts[2] = new Vector3(0,0,0);
            bodyParts[3] = new Vector3(0,0,0);
            bodyParts[4] = new Vector3(0,0,0);

        }
    }

    private GameObject CreateBodyObject (ulong id) {
        GameObject body = new GameObject ("Body:" + id);

        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++) {
            GameObject jointObj = GameObject.CreatePrimitive (PrimitiveType.Cube);

            LineRenderer lr = jointObj.AddComponent<LineRenderer> ();
            lr.SetVertexCount (2);
            lr.material = BoneMaterial;
            lr.SetWidth (0.05f, 0.05f);

            jointObj.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
            jointObj.name = jt.ToString ();
            jointObj.transform.parent = body.transform;
        }

        return body;
    }

    private void RefreshBodyObject (Kinect.Body body, GameObject bodyObject) {
        if(render)
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++) {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;

            if (_BoneMap.ContainsKey (jt)) {
                targetJoint = body.Joints[_BoneMap[jt]];
            }

            Transform jointObj = bodyObject.transform.Find (jt.ToString ());
            //Caleb Fix
            jointObj.localPosition = Vector3.Scale (GetVector3FromJoint (sourceJoint), multipliers) + offset+transform.position;

            LineRenderer lr = jointObj.GetComponent<LineRenderer> ();
            if (targetJoint.HasValue) {
                lr.SetPosition (0, jointObj.localPosition);
                lr.SetPosition (1, Vector3.Scale (GetVector3FromJoint (targetJoint.Value), multipliers) + offset);
                lr.SetColors (GetColorForState (sourceJoint.TrackingState), GetColorForState (targetJoint.Value.TrackingState));
            } else {
                lr.enabled = false;
            }
        }
    }

    private static Color GetColorForState (Kinect.TrackingState state) {
        switch (state) {
            case Kinect.TrackingState.Tracked:
                return Color.green;

            case Kinect.TrackingState.Inferred:
                return Color.red;

            default:
                return Color.black;
        }
    }

    private static Vector3 GetVector3FromJoint (Kinect.Joint joint) {
        return new Vector3 (joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}