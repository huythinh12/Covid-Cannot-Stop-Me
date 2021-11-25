using UnityEngine;

public class NPCDetectVirus : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Virus"))
        {
            var state = GetComponent<NPCState>();

            if (state.currentHealth > 0)
            {//check when npc have mask
                if (!state.isDef)
                {
                    if (state.countInject < 2)
                    {
                        if (state.countInject == 1)
                        {
                            var rdLucky = Random.Range(0, 3);
                            if (rdLucky == 2)
                            {
                                state.isVirusInside = true;
                                state.currentHealth--;
                            }
                        }
                        else 
                        {
                            state.currentHealth--;
                        }  
                    }
                }
                else
                {
                    state.isDef = false;
                }
            }
        }
    }
}