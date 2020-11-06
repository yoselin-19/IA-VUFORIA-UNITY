using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class Principal : MonoBehaviour
{
    private string nombre_archivo = "datosUsuarios.json";

    public void CambioScena(string nombre_escena){
        SceneManager.LoadScene(nombre_escena);
    }

    /*Usuario:
        public DateTime fecha;
        public int punteo;
        public float tiempo;
        public string nombre;
        public int tipo;
        public int cantidad_movimientos;
    */

    public void GuardarNombre(string nombre, int punteo, float tiempo, int tipo, int cantidad_movimientos){
        string filePath = Path.Combine(Application.persistentDataPath, nombre_archivo);
        if (File.Exists(filePath)){
            string json = File.ReadAllText(filePath);
            DatosAGuardar dag = JsonUtility.FromJson<DatosAGuardar>(json);

            Usuario user = new Usuario();
            user.fecha = DateTime.Now.ToString("dd/MM/yyyy");
            user.punteo = punteo;
            user.tiempo = tiempo;
            user.nombre = nombre;
            user.tipo = tipo;
            user.cantidad_movimientos = cantidad_movimientos;

            dag.usuarios.Add(user);
            
            //Guardando Datos
            string json2 = JsonUtility.ToJson(dag, true);
            string file = Path.Combine(Application.persistentDataPath, nombre_archivo);
            File.WriteAllText(file, json2);
            
        } else {
            // Seteando valores a guardar
            DatosAGuardar dag = new DatosAGuardar();
            dag.usuarios = new List<Usuario>();
            
            Usuario user = new Usuario();
            user.fecha = DateTime.Now.ToString("dd/MM/yyyy");
            user.punteo = punteo;
            user.tiempo = tiempo;
            user.nombre = nombre;
            user.tipo = tipo;
            user.cantidad_movimientos = cantidad_movimientos;

            dag.usuarios.Add(user);
            
            //Guardando Datos
            string json = JsonUtility.ToJson(dag, true);
            File.WriteAllText(filePath, json);
        }
        
    }
}
