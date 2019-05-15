using UnityEngine;

public class JumpLocation : MonoBehaviour
{
    public LandingLocation landingLocation;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Agent"))
            return;
        Agent agent = other.GetComponent<Agent>();
        Jump jump = other.GetComponent<Jump>();
        if (agent == null || jump == null)
            return;
        Vector3 originPos = transform.position;
        Vector3 targetPos = landingLocation.transform.position;
        jump.Isolate(true);
        jump.jumpPoint = new JumpPoint(originPos, targetPos);
        jump.DoJump();
    }
}
