using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ExplosionForcePowerUp : PowerUp
{
    // public new PowersEnum powerUpEnum = PowersEnum.ExplosionForce;
    // Start is called before the first frame update
    void Start()
    {
        this.powerUpEnum = PowersEnum.ExplosionForce;
    }

   
}
