using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public bool isFiring = false;
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;

    public Transform raycastOrigin;
    public Transform raycastDestination;

    Ray ray;
    RaycastHit hitInfo;

    public void StartFiring()
    {
        isFiring = true;
        /*
        `foreach`-silmukan toiminta perustuu sen kohdeaineiston tyyppiin. Kun k‰yt‰t `foreach`-silmukkaa, 
        joka iteroi kokoelmaa, kuten taulukkoa tai listaobjektia, jokainen iteraatio palauttaa kokoelman 
        j‰senen tietyn tyyppisen‰. T‰ss‰ tapauksessa `ParticleSystem[] muzzleFlash` on taulukko `ParticleSystem`-olioista.

        Kun m‰‰rittelet silmukassa `var particle`, se viittaa jokaiseen iteraation aikana kokoelmaan sis‰ltyv‰‰n 
        `ParticleSystem`-olioon. `var`-avainsanan k‰yttˆ tarkoittaa sit‰, ett‰ C#-k‰‰nt‰j‰ p‰‰ttelee 
        automaattisesti muuttujan tyypin sen alustimen perusteella. Koska `muzzleFlash`-taulukko on m‰‰ritetty tyyppiin 
        `ParticleSystem[]`, `var`-avainsanan k‰yttˆ t‰ss‰ yhteydess‰ tarkoittaa, ett‰ `particle` on 
        `ParticleSystem`-tyyppinen muuttuja.

        Lyhyesti sanottuna `foreach`-silmukka tiet‰‰, ett‰ `particle` on `ParticleSystem`-tyyppinen, koska se iteroi 
        `muzzleFlash`-taulukkoa, joka on m‰‰ritelty sis‰lt‰m‰‰n `ParticleSystem`-olioita.
         */
        foreach (var particle in muzzleFlash) particle.Emit(1);

        ray.origin = raycastOrigin.position;
        ray.direction = raycastDestination.position - raycastOrigin.position;
        /*Koodinp‰tk‰ss‰ Instantiate-funktio luo uuden peliobjektin tracerEffect-pohjalta annettuun sijaintiin ja 
         kiertoarvoon. Koska tracerEffect on tyyppi‰ TrailRenderer, uusi instanssi saa myˆs TrailRenderer-komponentin.
         Sen j‰lkeen tracer-muuttujaan tallennetaan viite t‰h‰n uuteen instanssiin. Koska tracer-muuttujan tyyppi‰ ei ole 
         m‰‰ritetty suoraan, vaan k‰ytet‰‰n var-avainsanaa, C# p‰‰tt‰‰ muuttujan tyypin automaattisesti sen perusteella,
         mit‰ Instantiate-funktio palauttaa. Koska Instantiate-funktio palauttaa uuden instanssin TrailRenderer-komponentista, 
         tracer-muuttuja on myˆs TrailRenderer-tyyppinen.
         Sitten AddPosition(ray.origin)-kutsulla lis‰t‰‰n alkupiste (ray.origin) uuden tracer-instanssin polkuun. 
         T‰m‰ asettaa alkupisteen ensimm‰iseksi paikaksi polun piirt‰misess‰.*/
        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitInfo))
        {
            //UnityEngine.Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);

            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            tracer.transform.position = hitInfo.point;
        }
    }

    public void StopFiring()
    {
        isFiring = false;

    }
}
