using UnityEngine;
using UnityEngine.InputSystem;

public class BikeInteraction : MonoBehaviour
{
    public PlayerController playerController;
    public BikeController bikeController;
    public Transform mountPoint; // Pelaajan sijainti moottoripy�r�n kyytiin nousemista varten
    public Transform mountSeatPoint; // Istumapaikka pelaajalle moottoripy�r�n kyytiin noustessa
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
            // Jos pelaaja on jo moottoripy�r�n kyydiss�, pyyd� pelaajaa poistumaan kyydist�
            bikeController.DismountPlayer(playerController);
        }
        else
        {
            // Muuten pyyd� pelaajaa nousemaan moottoripy�r�n kyytiin
            playerController.MountBike(bikeController);
        }
    }
}
