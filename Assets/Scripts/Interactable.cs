using System.Runtime.CompilerServices;
using Controllers;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected PlayerInvController inv;
    void Start()
    {
        inv = GameObject.Find("GameC").GetComponent<PlayerInvController>();
    }
    public virtual void Interact()
    {
        //overwrite in child classes
    }

}

