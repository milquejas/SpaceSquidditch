using UnityEngine;
using UnityEngine.InputSystem;

public class BikeInteraction : MonoBehaviour
{
    public PlayerController playerController;
    public BikeController bikeController;
    public Transform mountPoint; // Pelaajan sijainti moottoripyörän kyytiin nousemista varten
    public Transform mountSeatPoint; // Istumapaikka pelaajalle moottoripyörän kyytiin noustessa
    public float mountDistanceThreshold = 2f;
    public LayerMask playerLayer;



    private InputAction interact;

    private void Awake()
    {
        interact = new InputAction("interact", InputActionType.Button, "<Keyboard>/e");
        interact.performed += ctx => Interact();
    }

    private void OnEnable()
    {
        interact.Enable();
    }

    private void OnDisable()
    {
        interact.Disable();
    }

    private void Interact()
    {
        if (bikeController.PlayerOnBike())
        {
            // Jos pelaaja on jo moottoripyörän kyydissä, pyydä pelaajaa poistumaan kyydistä
            bikeController.DismountPlayer(playerController);
        }
        else
        {
            // Muuten pyydä pelaajaa nousemaan moottoripyörän kyytiin
            playerController.MountBike(bikeController);
        }
    }
}
