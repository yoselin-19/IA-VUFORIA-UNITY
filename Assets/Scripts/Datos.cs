using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
    Serializacion y elaboracion de datos para el json <<Datos>>
*/
[Serializable]
public class DatosAGuardar
{
    public List<Usuario> usuarios;
}

[Serializable]
public class Usuario
{
    public string fecha;
    public int punteo;
    public float tiempo;
    public string nombre;
    public int tipo;
    public int cantidad_movimientos;
}