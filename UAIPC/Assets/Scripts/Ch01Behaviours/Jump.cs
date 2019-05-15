using UnityEngine;
using System.Collections.Generic;

public class Jump : VelocityMatch
{
    public JumpPoint jumpPoint;

    //Keeps track of whether the jump is achievable
    bool canAchieve = false;

    //Holds the maximum vertical jump velocity
    public float maxYVelocity;

    public Vector3 gravity = new Vector3(0, -9.8f, 0);

    private Projectile projectile;

    private List<AgentBehaviour> behaviours;


    public void Isolate(bool state)
    {
        foreach (AgentBehaviour b in behaviours)
            b.enabled = !state;
        this.enabled = state;
    }

    public void DoJump()
    {
        projectile.enabled = true;
        Vector3 direction;
        direction = Projectile.GetFireDirection(jumpPoint.jumpLocation, jumpPoint.landingLocation, agent.maxSpeed);
        projectile.Set(jumpPoint.jumpLocation, direction, agent.maxSpeed, false);
    }

    public override void Awake()
    {
        base.Awake();
        this.enabled = false;
        projectile = gameObject.AddComponent<Projectile>();
        behaviours = new List<AgentBehaviour>();
        AgentBehaviour[] abs;
        abs = gameObject.GetComponents<AgentBehaviour>();
        foreach (AgentBehaviour b in abs)
        {
            if (b == this)
                continue;
            behaviours.Add(b);
        }
    }


    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        // Check if we have a trajectory, and create one if not.
        if (jumpPoint != null && target == null)
        {
            CalculateTarget();
        }

        //Check if the trajectory is zero. If not, we have no acceleration.
        if (!canAchieve)
        {
            return steering;
        }

        //Check if we’ve hit the jump point
        if (Mathf.Approximately((transform.position - target.transform.position).magnitude, 0f) &&
            Mathf.Approximately((agent.velocity - target.GetComponent<Agent>().velocity).magnitude, 0f))
        {
            DoJump();
            return steering;
        }

        return base.GetSteering();
    }

    protected void CalculateTarget()
    {
        target = new GameObject();
        target.AddComponent<Agent>();

        //Calculate the first jump time
        float sqrtTerm = Mathf.Sqrt(2f * gravity.y * jumpPoint.deltaPosition.y + maxYVelocity * agent.maxSpeed);

        float time = (maxYVelocity - sqrtTerm) / gravity.y;

        //Check if we can use it, otherwise try the other time
        if (!CheckJumpTime(time))
        {
            time = (maxYVelocity + sqrtTerm) / gravity.y;
        }
    }

    //Private helper method for the CalculateTarget function
    private bool CheckJumpTime(float time)
    {
        //Calculate the planar speed
        float vx = jumpPoint.deltaPosition.x / time;
        float vz = jumpPoint.deltaPosition.z / time;

        float speedSq = vx * vx + vz * vz;

        //Check it to see if we have a valid solution
        if (speedSq < agent.maxSpeed * agent.maxSpeed)
        {
            target.GetComponent<Agent>().velocity = new Vector3(vx, 0f, vz);
            canAchieve = true;
            return true;
        }

        return false;
    }
}
