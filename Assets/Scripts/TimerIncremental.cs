using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerIncremental : MonoBehaviour
{
    public Text _tiempoText;
    public float _tiempo;
    public bool _ActivarTiempo;

    // Start is called before the first frame update
    void Start()
    {
        _ActivarTiempo = false;
        _tiempo = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_ActivarTiempo)
        {
            _tiempo += Time.deltaTime;
            _tiempoText.text = "Tiempo: " + _tiempo.ToString("f0");
        }        
    }
}
