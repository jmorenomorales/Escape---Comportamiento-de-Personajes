using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private GameObject jugador;

    private enum TEstado { ESPERANDO, GIRANDO, FIJADO_DISPARO };
    private TEstado estado = TEstado.ESPERANDO;
    private Vector3 posicionObjetivo = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
