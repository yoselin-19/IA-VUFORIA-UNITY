using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    private string nombre_archivo = "datosUsuarios.json";

    private Transform entryContainerRompecabezas;
    private Transform entryTemplateRompecabezas;
    private List<Transform> highscoreEntryTransformListRompecabezas;

    private void Awake() {
        Transform _ruleta = transform.Find("Ruleta");
        entryContainer = _ruleta.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        Transform _rompecabezas = transform.Find("Rompecabezas");
        entryContainerRompecabezas = _rompecabezas.Find("highscoreEntryContainerRompecabezas");
        entryTemplateRompecabezas = entryContainerRompecabezas.Find("highscoreEntryTemplateRompecabezas");
        entryTemplateRompecabezas.gameObject.SetActive(false);        

        string filePath = Path.Combine(Application.persistentDataPath, nombre_archivo);
        if (File.Exists(filePath)){
            string json = File.ReadAllText(filePath);
            DatosAGuardar dag = JsonUtility.FromJson<DatosAGuardar>(json);

            // Separar usuarios ruleta y rompecabezas
            List<Usuario> usuarios_ruleta = new List<Usuario>();
            List<Usuario> usuarios_rompecabezas = new List<Usuario>();

            for (int i = 0; i < dag.usuarios.Count; i++)
            {
                Usuario actual = dag.usuarios[i];
                if(actual.tipo==1){
                    usuarios_ruleta.Add(actual);
                } else {
                    usuarios_rompecabezas.Add(actual);
                }
            }

            // Sort entry list by Score Ruleta
            for (int i = 0; i < usuarios_ruleta.Count; i++)
            {
                for (int j = i+1; j < usuarios_ruleta.Count; j++)
                {
                    if(usuarios_ruleta[j].punteo >= usuarios_ruleta[i].punteo){
                        Usuario tmp = usuarios_ruleta[i];
                        usuarios_ruleta[i] = usuarios_ruleta[j];
                        usuarios_ruleta[j] = tmp;
                    }
                }
            }

            highscoreEntryTransformList = new List<Transform>();
            int contador_ruleta = 0;
            foreach (Usuario highcoreEntry in usuarios_ruleta)
            {
                if(contador_ruleta < 5){
                    CreateHigscoreEntryTransform(highcoreEntry, entryContainer, highscoreEntryTransformList);
                    contador_ruleta++;
                }
            }

            // Sort entry list by Score Ruleta
            for (int i = 0; i < usuarios_rompecabezas.Count; i++)
            {
                for (int j = i+1; j < usuarios_rompecabezas.Count; j++)
                {
                    if(usuarios_rompecabezas[j].cantidad_movimientos >= usuarios_rompecabezas[i].cantidad_movimientos){
                        Usuario tmp = usuarios_rompecabezas[i];
                        usuarios_rompecabezas[i] = usuarios_rompecabezas[j];
                        usuarios_rompecabezas[j] = tmp;
                    }
                }
            }

            highscoreEntryTransformListRompecabezas = new List<Transform>();
            int contador_rompecabezas = 0;
            foreach (Usuario highcoreEntry in usuarios_rompecabezas)
            {
                if(contador_rompecabezas < 5){
                    CreateHigscoreEntryTransformRompecabezas(highcoreEntry, entryContainerRompecabezas, highscoreEntryTransformListRompecabezas);
                    contador_rompecabezas++;
                }
            }            
        }
    }

    private void CreateHigscoreEntryTransform(Usuario highscoreEntry, Transform container, List<Transform> transformList){
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0,-templateHeight*transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch(rank){
            default:
                rankString=rank + "TH"; break;
            case 1: rankString="1ST"; break;
            case 2: rankString="2ND"; break;
            case 3: rankString="3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        string fecha = highscoreEntry.fecha;
        entryTransform.Find("fechaText").GetComponent<Text>().text = fecha;

        int score = highscoreEntry.punteo;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        float tiempo = highscoreEntry.tiempo;
        entryTransform.Find("tiempoText").GetComponent<Text>().text = tiempo.ToString();

        string name = highscoreEntry.nombre;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);
    }

    private void CreateHigscoreEntryTransformRompecabezas(Usuario highscoreEntry, Transform container, List<Transform> transformList){
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplateRompecabezas, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0,-templateHeight*transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch(rank){
            default:
                rankString=rank + "TH"; break;
            case 1: rankString="1ST"; break;
            case 2: rankString="2ND"; break;
            case 3: rankString="3RD"; break;
        }

        entryTransform.Find("posTextRompecabezas").GetComponent<Text>().text = rankString;

        string fecha = highscoreEntry.fecha;
        entryTransform.Find("fechaTextRompecabezas").GetComponent<Text>().text = fecha;

        int movimientos = highscoreEntry.cantidad_movimientos;
        entryTransform.Find("movimientosTextRompecabezas").GetComponent<Text>().text = movimientos.ToString();

        float tiempo = highscoreEntry.tiempo;
        entryTransform.Find("tiempoTextRompecabezas").GetComponent<Text>().text = tiempo.ToString();

        string name = highscoreEntry.nombre;
        entryTransform.Find("nameTextRompecabezas").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);
    }    
}
