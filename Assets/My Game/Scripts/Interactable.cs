using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    // Permite arrastar funções do GameManager ou WorkStation direto no Inspector
    public UnityEvent onInteract;

    public void Interact()
    {
        Debug.Log("Interagiu com: " + gameObject.name);
        onInteract.Invoke();
    }
}