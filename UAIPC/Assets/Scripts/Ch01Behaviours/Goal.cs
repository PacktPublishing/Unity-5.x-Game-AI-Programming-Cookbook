using UnityEngine;
using System.Collections;

public class Goal {

    public bool isPosition;
    public bool isOrientation;
    public bool isVelocity;
    public bool isRotation;

    public float orientation;
    public float rotation;
    public Vector3 position;
    public Vector3 velocity;

    public Goal() {
        isPosition = false;
        isOrientation = false;
        isVelocity = false;
        isRotation = false;
        orientation = 0.0f;
        rotation = 0.0f;
        position = Vector3.zero;
        velocity = Vector3.zero;
    }

    public void UpdateChannels (Goal o) {
        if (o.isOrientation)
            orientation = o.orientation;
        if (o.isRotation)
            rotation = o.rotation;
        if (o.isPosition)
            position = o.position;
        if (o.isVelocity)
            velocity = o.velocity;
    }
}
