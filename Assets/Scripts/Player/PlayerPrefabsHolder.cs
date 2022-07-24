using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabsHolder : MonoBehaviour
{
    [Header("Shift Element Effect")]
    public GameObject shiftIgnisFX;
    public GameObject shiftVentusFX;
    public GameObject shiftTerraFX;
    
    [Header("Melee Attack Effect <Ignis>")]
    public GameObject ignisMelee1FX;
    public GameObject ignisMelee2FX;
    public GameObject ignisMelee3FX;
    public GameObject ignisHitFX;
    [Header("Melee Attack Effect <Terra>")]
    public GameObject terraMelee1FX;
    public GameObject terraMelee2FX;
    public GameObject terraMelee3FX;
    public GameObject terraHitFX;
    [Header("Melee Attack Effect <Ventus>")]
    public GameObject ventusMelee1FX;
    public GameObject ventusMelee2FX;
    public GameObject ventusMelee3FX;
    public GameObject ventusHitFX;

    [Header("Ventus DoubleJump Effect")]
    public GameObject ventusDoubleJumpFX;
    [Header("Ventus Dash Effect")]
    public GameObject ventusDashFX;
}
