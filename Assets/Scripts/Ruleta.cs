using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Ruleta : MonoBehaviour
{
    public List<GameObject> paquete1 = new List<GameObject>();
    public List<GameObject> paquete2 = new List<GameObject>();
    public List<GameObject> paquete3 = new List<GameObject>();
    private bool paquete1_activo;
    private bool paquete2_activo;
    private bool paquete3_activo;
    private int paquete1_indice;
    private int paquete2_indice;
    private int paquete3_indice;
    private Timer tiempo;
    private TimerIncremental tiempoGeneral;
    private Principal principal;
    private int veces = -1;
    public int nivel;
    private Text nivelTexto;
    public GameObject panelGanador;
    public InputField entrada;
    public Button _continuar;
    public int punteo;
    private Text punteoTexto;
    public GameObject boton;
    public TextMesh texto;
    private bool bandera = false;

    private void Awake(){
        tiempo = FindObjectOfType<Timer>();
        principal = FindObjectOfType<Principal>();
        tiempoGeneral = FindObjectOfType<TimerIncremental>();
        nivelTexto = GameObject.Find("Nivel").GetComponent<Text>();
        punteoTexto = GameObject.Find("Puntos").GetComponent<Text>();
        boton.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        boton.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        nivel = 1;
        punteo = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        ReseteoJuego();
        tiempoGeneral._tiempo = 0f;
        bandera = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(paquete1_activo && paquete2_activo && paquete3_activo){
            Girar(paquete1,1);
            Girar(paquete2,2);
            Girar(paquete3,3);
            if(tiempo._terminoTiempo){
                tiempo._terminoTiempo = false;
                AsignarTiempo();
            }
        } else if(!paquete1_activo && paquete2_activo && paquete3_activo){
            Girar(paquete2,2);
            Girar(paquete3,3);
            if(tiempo._terminoTiempo){
                tiempo._terminoTiempo = false;
                AsignarTiempo();
            }
        } else if(!paquete1_activo && !paquete2_activo && paquete3_activo){
            Girar(paquete3,3);
            if(tiempo._terminoTiempo){
                tiempo._terminoTiempo = false;
                AsignarTiempo();
            }
        }

        if(nivel==5){
            tiempoGeneral._ActivarTiempo = false;
            panelGanador.SetActive(true);
        }
    }

    private void Reseteo(List<GameObject> lista){
        for (int i = 0; i < lista.Count; i++)
        {
            GameObject actual = lista[i];
            if(i==0){
                actual.SetActive(true);
            } else {
                actual.SetActive(false);
            }
        }
    }

    private void Girar(List<GameObject> lista, int tipo){
        if(tiempo._terminoTiempo){
            if(tipo == 1){
                GameObject actual = lista[paquete1_indice];
                if(paquete1_indice + 1 == lista.Count){
                    paquete1_indice = -1;
                }
                GameObject actual_siguiente = lista[paquete1_indice+1];

                CambiarEstado(actual, false);
                CambiarEstado(actual_siguiente, true);
                paquete1_indice++;
            }else if(tipo == 2){
                GameObject actual = lista[paquete2_indice];
                if(paquete2_indice + 1 == lista.Count){
                    paquete2_indice = -1;
                }
                GameObject actual_siguiente = lista[paquete2_indice+1];

                CambiarEstado(actual, false);
                CambiarEstado(actual_siguiente, true);
                paquete2_indice++;                
            }else {
                GameObject actual = lista[paquete3_indice];
                if(paquete3_indice + 1 == lista.Count){
                    paquete3_indice = -1;
                }
                GameObject actual_siguiente = lista[paquete3_indice+1];

                CambiarEstado(actual, false);
                CambiarEstado(actual_siguiente, true);
                paquete3_indice++;    
            }
        }
    }

    private void CambiarEstado(GameObject actual, bool estado){
        actual.SetActive(estado);
    }

    public void Presionar(){
        if(veces == -1){
            AsignarTiempo();
            veces = 3;
            paquete1_activo = true;
            paquete2_activo = true;
            paquete3_activo = true;
            tiempo._ActivarTiempo = true;
            tiempoGeneral._ActivarTiempo = true;
        } else if(veces == 3){
            veces = 2;
            paquete1_activo = false;
            paquete2_activo = true;
            paquete3_activo = true;
        } else if(veces == 2) {
            veces = 1;
            paquete1_activo = false;
            paquete2_activo = false;
            paquete3_activo = true;
        } else {
            veces = 0;
            paquete1_activo = false;
            paquete2_activo = false;
            paquete3_activo = false;
            tiempo._ActivarTiempo = false;
            RevisarGano();
        }
    }

    private void AsignarTiempo(){
        //Nivel1->2  Nivel2->1.6f  Nivel3->1.45f  Nivel4->1.15f

        if(nivel==1){tiempo._tiempo = 2;}
        else if(nivel==2){tiempo._tiempo = 1.6f;}
        else if(nivel==3){tiempo._tiempo = 1.45f;}
        else {tiempo._tiempo = 1.35f;}

        nivelTexto.text = "Nivel: " + nivel;
    }

    private void RevisarGano(){
        if(paquete1_indice == paquete2_indice && paquete1_indice == paquete3_indice){
            //gano
            punteo += 10;
            punteoTexto.text = "Puntos: " + punteo;
            //Aumento nivel
            nivel++;
            //Reseteo juego
            ReseteoJuego();
        } else { //Perdio
            principal.GuardarNombre("", punteo, tiempoGeneral._tiempo, 1, 0);
            principal.CambioScena("Principal");
        }
    }

    private void ReseteoJuego(){
        Reseteo(paquete1);
        Reseteo(paquete2);
        Reseteo(paquete3);
        paquete1_activo = false;
        paquete2_activo = false;
        paquete3_activo = false;
        paquete1_indice = 0;
        paquete2_indice = 0;
        paquete3_indice = 0;
        veces = -1;
        AsignarTiempo(); //Nivel1->2  Nivel2->1.6f  Nivel3->1.45f  Nivel4->1.15f
        tiempo._ActivarTiempo = false;
    }

    public void CambiarColor(){
        if(entrada.text != ""){
            // Cambiar color boton
            string htmlValue = "#7FBB00";
            Color newCol;
            if (ColorUtility.TryParseHtmlString(htmlValue, out newCol))
            {
                _continuar.image.color = newCol;
            }
        } else {
            // Cambiar color boton
            string htmlValue = "#F8F5F5";
            Color newCol;
            if (ColorUtility.TryParseHtmlString(htmlValue, out newCol))
            {
                _continuar.image.color = newCol;
            }
        }
    }

    public void RegistrarUsuario(){
        if (entrada.text != ""){
            principal.GuardarNombre(entrada.text, punteo, tiempoGeneral._tiempo, 1, 0);
            panelGanador.SetActive(false);
            principal.CambioScena("Principal");
        }
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb){
        bandera = !bandera;
        texto.text = "" + bandera;
        Presionar();
        // Debug.Log("Boton presionado");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb){
        // Debug.Log("Deje de presionar");
    }
}
