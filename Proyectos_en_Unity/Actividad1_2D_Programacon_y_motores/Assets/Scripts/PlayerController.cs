using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float velocidadX = 3.0f;
    [SerializeField] float velocidadY = 3.0f;
    [SerializeField] float movimieto = 0f;
    [SerializeField] float movimieto2 = 0f;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] TextMeshProUGUI panelTiempo;
    
    private Rigidbody2D rb;

    private bool tocaSuelo = false;
    private int puntuacion = 0;
    private float tiempoTotal = 0f;

    [SerializeField] int CampoDeVisionSerializado = 0; //Es como un campo Protected en C++. Convertir un objeto en una secuencia de bytes para guardarlo o transmitirlo.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Accede a x componente del objeto al que esta asociado el Script.
    }

    // Update is called once per frame
    void Update()
    {
        movimieto = Input.GetAxis("Horizontal");
        movimieto2 = Input.GetAxis("Vertical");
        rb.AddForce(new Vector2(movimieto * velocidadX, 0));  //Le aplicamos una fuerza al objeto
        if (!tocaSuelo)
        {
            movimieto2 = 0f;
        }
        rb.AddForce(new Vector2(0, movimieto2 * velocidadY * 7));
        //rb.AddForce(new Vector2(movimieto * velocidad, movimieto * velocidad));
        //Debug.Log(movimieto);

        // Controlo el tiempo que ha pasado
        tiempoTotal = tiempoTotal + Time.deltaTime; // Calcula el tiempo transcurrido del juego independientemente del equipo que lo ejecute.
        //Debug.Log("Tiempo transcurrido: " +  tiempoTotal);
        panelTiempo.text= $"Tiempo de juego: {tiempoTotal.ToString()}";

    }

    // ---------------------------------------- //
    // ---------- Clases Adicionales ---------- //
    // ---------------------------------------- //

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Una pelota ha colisionado con nuestro jugador");
        if (collision.gameObject.tag == "Bola")
        {
            collision.gameObject.SetActive(false); //Desactiva un objeto con el que colisione nuestro Player
            puntuacion++;
            textMeshProUGUI.text = "Puntuación: " + puntuacion.ToString();
            //Debug.Log("La puntuación actual es: " + puntuacion);
        }

        if (collision.gameObject.tag == "Suelo")
        {
            //Debug.Log("Estoy tocando el Suelo");
            tocaSuelo = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Suelo")
        {
            //Debug.Log("Dejo de tocar el Suelo");
            tocaSuelo=false;
        }
    }

    /*
    private void OnTriggerEnter2d(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Moneda")){
            Debug.Log("Detecta choque con trigger");
        } else if (collision.gameObject.CompareTag("ParedInvisible"))
        {
            Debug.Log("Detecta choque con pared Invisible");
        }
    }

    private void OnTriggerExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Moneda"))
        {
            Debug.Log("Detecta que dejo de tocar el trigger");

        }
    }

    private void OnTriggerStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Moneda"))
        {
            Debug.Log("Detecto que estoy tocando el trigger");
        }
    }*/
}
