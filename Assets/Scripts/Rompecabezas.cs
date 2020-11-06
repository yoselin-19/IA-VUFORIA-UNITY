using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Rompecabezas : MonoBehaviour
{
    public VariableJoystick control_movimiento;
    int direccion = 0;
    bool bandera = false;
 
    int pos_vacio = 9;
    int contador = 0;
    public GameObject[] tablero;
    int [] matrizjuego;
    int [] matrizcorrecta;
    
    public GameObject panelGanador;
    public InputField entrada;
    public Button _continuar;
    private TimerIncremental tiempoGeneral;
    private Principal principal;

    private void Awake(){
        principal = FindObjectOfType<Principal>();
        tiempoGeneral = FindObjectOfType<TimerIncremental>();
    }

    void Start(){
        if(contador==0){
            matrizjuego = new int[9]{2,3,5,4,6,7,8,1,9};
            matrizcorrecta = new int[9]{1,2,3,4,5,6,7,8,9};
            tiempoGeneral._tiempo = 0f;
        }
    }

    void Update(){
        double posy = Math.Round(control_movimiento.Vertical, 0, MidpointRounding.ToEven);
    	double posx = Math.Round(control_movimiento.Horizontal, 0, MidpointRounding.ToEven);
        tiempoGeneral._ActivarTiempo = true;

        //indicando direccion
        if(posx==1 &&posy==0){
            direccion = 1;
            bandera = true;
        } else if(posx==-1 &&posy==0){
            direccion = 2;
            bandera = true;
        } else if(posx==0 &&posy==1){
            direccion = 3;
            bandera = true;
        } else if(posx==0 &&posy==-1){
            direccion = 4;
            bandera = true;
        }

        //Indicando movimientos
        if(posx == 0 && posy == 0 && bandera == true){
            bandera = false;
            contador++;
            if(direccion==1){ //Derecha
                mover(direccion);
            } else if(direccion==2){ //Izquierda
                mover(direccion);
            } else if(direccion==3){ //Arriba
                mover(direccion);
            } else if(direccion==4){ //Abajo
                mover(direccion);
            }
        }

        //Revisar si gano
        bool gano = false;
        for(int i = 0; i < 9; i++){
            int valor = (int)matrizjuego.GetValue(i);
            int valor2 = (int)matrizcorrecta.GetValue(i);
            if(valor == valor2){
                gano = true;
            } else {
                gano = false;
                break;
            }
        }

        if(gano){
            tiempoGeneral._ActivarTiempo = false;
            panelGanador.SetActive(true);
        }
    }

    void mover(int direccion){
        if(pos_vacio == 9){
            if(direccion == 1){}
            else if(direccion==2){
                CambioPosicion(8, 7, 8);
            } 
            else if(direccion==3){
                CambioPosicion(8, 5, 6);
            } 
            else if(direccion == 4){}
        } 
        else if(pos_vacio == 8){
            if(direccion == 1){
                CambioPosicion(7, 8, 9);
            }
            else if(direccion==2){
                CambioPosicion(7, 6, 7);
            } 
            else if(direccion==3){
                CambioPosicion(7, 4, 5);
            } 
            else if(direccion == 4){}
        }
        else if(pos_vacio == 7){
            if(direccion == 1){
                CambioPosicion(6, 7, 8);
            }
            else if(direccion==2){} 
            else if(direccion==3){
                CambioPosicion(6, 3, 4);
            } 
            else if(direccion == 4){}
        }
        else if(pos_vacio == 6){
            if(direccion == 1){}
            else if(direccion==2){
                CambioPosicion(5, 4, 5);
            } 
            else if(direccion==3){
                CambioPosicion(5, 2, 3);
            } 
            else if(direccion == 4){
                CambioPosicion(5, 8, 9);
            }
        }
        else if(pos_vacio == 5){
            if(direccion == 1){
                CambioPosicion(4, 5, 6);
            }
            else if(direccion==2){
                CambioPosicion(4, 3, 4);
            } 
            else if(direccion==3){
                CambioPosicion(4, 1, 2);
            } 
            else if(direccion == 4){
                CambioPosicion(4, 7, 8);
            }
        }       
        else if(pos_vacio == 4){
            if(direccion == 1){
                CambioPosicion(3, 4, 5);
            }
            else if(direccion==2){} 
            else if(direccion==3){
                CambioPosicion(3, 0, 1);
            } 
            else if(direccion == 4){
                CambioPosicion(3, 6, 7);
            }
        }
        else if(pos_vacio == 3){
            if(direccion == 1){}
            else if(direccion==2){
                CambioPosicion(2, 1, 2);
            } 
            else if(direccion==3){} 
            else if(direccion == 4){
                CambioPosicion(2, 5, 6);
            }
        }  
        else if(pos_vacio == 2){
            if(direccion == 1){
                CambioPosicion(1, 2, 3);
            }
            else if(direccion==2){
                CambioPosicion(1, 0, 1);
            } 
            else if(direccion==3){} 
            else if(direccion == 4){
                CambioPosicion(1, 4, 5);
            }
        }
        else if(pos_vacio == 1){
            if(direccion == 1){
                CambioPosicion(0, 1, 2);
            }
            else if(direccion==2){} 
            else if(direccion==3){} 
            else if(direccion == 4){
                CambioPosicion(0, 3, 4);
            }
        }                         
    }

    private void CambioPosicion(int pos_tablero_origen, int pos_tablero_destino, int nuevo_pos){
        GameObject temp = (GameObject)tablero.GetValue(pos_tablero_origen); //panel vacio
        float x = temp.transform.position.x;
        float y = temp.transform.position.y;
        float z = temp.transform.position.z;

        GameObject temp2 = (GameObject)tablero.GetValue(pos_tablero_destino); //panel izquierdo
        float x2 = temp2.transform.position.x;
        float y2 = temp2.transform.position.y;
        float z2 = temp2.transform.position.z;

        temp.transform.position = new Vector3(x2, y2, z2);
        temp2.transform.position = new Vector3(x, y ,z);

        tablero.SetValue(temp2, pos_tablero_origen);
        tablero.SetValue(temp, pos_tablero_destino);
        
        pos_vacio = nuevo_pos;
        
        int temporalposicion = (int)matrizjuego.GetValue(pos_tablero_destino);
        int temporalposicion2 = (int)matrizjuego.GetValue(pos_tablero_origen);

        matrizjuego.SetValue(temporalposicion2, pos_tablero_destino);
        matrizjuego.SetValue(temporalposicion, pos_tablero_origen);
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
            principal.GuardarNombre(entrada.text, 0, tiempoGeneral._tiempo, 2, contador);
            contador=0;
            panelGanador.SetActive(false);
            principal.CambioScena("Principal");
        }
    }
}
