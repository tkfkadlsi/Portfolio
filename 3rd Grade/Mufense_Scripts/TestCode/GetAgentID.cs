using UnityEngine;
using UnityEngine.AI;

public class GetAgentID : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"{GetComponent<NavMeshAgent>().agentTypeID}");
    }
}
