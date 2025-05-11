using Unity.VisualScripting;
using UnityEngine;

public class GateButton : MonoBehaviour, Interactable
{
    public string InteractMessage => objectInteractMessage;

    [SerializeField]
    string objectInteractMessage;

    [SerializeField] 
    GameObject gate;

    [SerializeField] 
    AudioSource gateSource;

    [SerializeField]
    AudioClip gateClip;
    void Open()
    {
        gateSource.PlayOneShot(gateClip);
        gate.GetComponent<Animator>().Play("GateSwing");
    }

    public void Interact()
    {
        Open();
    }
}
