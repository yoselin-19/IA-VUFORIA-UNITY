using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // public Text _tiempoText;
    public float _tiempo = 1000.0f;
    public int _tiempoActual = 1000;
    public bool _ActivarTiempo = false;

    public bool _terminoTiempo = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_ActivarTiempo)
        {
            _tiempo -= Time.deltaTime;
            _tiempoActual = (int)_tiempo ;
            // _tiempoText.text = "Tiempo: " + _tiempo.ToString("f0");

            if (_tiempoActual == 0)
            {
                _terminoTiempo = true;
            }
        }
    }
}
