using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreBombPowerUp : PowerUp
{
    // public new PowersEnum powerUpEnum = PowersEnum.ExplosionForce;
    // Start is called before the first frame update
    void Start()
    {
        this.powerUpEnum = PowersEnum.MoreBombPowerUp;
    }

}
