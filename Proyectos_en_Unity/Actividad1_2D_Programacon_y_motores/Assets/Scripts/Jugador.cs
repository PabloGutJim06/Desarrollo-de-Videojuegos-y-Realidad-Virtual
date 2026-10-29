using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    #region Variables Serializadas
    [SerializeField] float velocidadX = 3.0f;
    [SerializeField] float velocidadY = 3.0f;
    [SerializeField] float movimientoX = 0f;
    [SerializeField] float movimientoY = 0f;
    [SerializeField] TextMeshProUGUI textoMonedas;
    [SerializeField] TextMeshProUGUI textoVidas;
    [SerializeField] TextMeshProUGUI textoMonedasTotales;
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] GameObject puerta;
    [SerializeField] GameObject TextoVictoria;
    [SerializeField] GameObject Abismo;
    [SerializeField] GameObject Enemigo;
    [SerializeField] Vector3 VolverTrasMuerte = new Vector3(0.37f, -8.77f, 0f);
    

    #endregion

    #region Variables Privadas
    // Muerte
    private int salud = 2;
    private bool invulnerabilidad = false;
    bool muerto = false;

    //Vidas
    private int vidasActuales = 5;

    //Saltar
    bool tocaSuelo = false;
    private int cantidadSaltosJugador = 2;
    private float tiempoEntreSaltos = 0f;
    private float delay = 0.3f; 

    //Monedas
    bool tocaMoneda = false; 
    int contadorMonedas = 0;

    //Llave
    bool tienellave = false;
    private Rigidbody2D rb;
    
    

    //Colision

    #endregion

    #region Start
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Accede a x componente del objeto al que esta asociado el Script.
    }
    #endregion

    #region Update
    // Update is called once per frame
    void Update()
    {
        if(!tocaSuelo)
        {
            movimientoY = 0f;
        }

        if (muerto){
            explosionFX.transform.position = transform.position;
            explosionFX.Play();
            //transform.position = new Vector3(VolverTrasMuerte.x, VolverTrasMuerte.y, transform.position.z);
            rb.transform.position = VolverTrasMuerte;
            rb.transform.rotation = Quaternion.Euler(0,0,0);
            rb.angularVelocity = 0f;
            rb.linearVelocity = Vector2.zero;
            muerto = false;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && cantidadSaltosJugador > 0 && tiempoEntreSaltos <= 0)
        {
            saltar();
        }
        movimientoX = Input.GetAxis("Horizontal");
        
        if(tiempoEntreSaltos > 0)
        {
            tiempoEntreSaltos = tiempoEntreSaltos - Time.deltaTime;
        }

        rb.AddForce(new Vector2(movimientoX * velocidadX, 0));
    }
    #endregion

    #region Metodos Colisiones

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo") // Solo nos interesa si lo que chocó es un enemigo.
        {
            // GetContact(0) = primer punto de contacto del choque.
            // .normal = vector de longitud 1 que indica hacia dónde "mira" la superficie de choque.
            Vector2 normal = collision.GetContact(0).normal;

            // Si la normal es más horizontal que vertical, el choque fue por un lado.
            // Mathf.Abs quita el signo (izquierda/derecha, arriba/abajo), así no importa de qué lado vengas.
            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                DaniarPlayer(); // Choque lateral: pierdes vida.
            } else
            {
                Destroy(collision.gameObject);
            }
            // Si no, fue vertical (pisaste al enemigo): no pasa nada.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Suelo")
        {
            tocaSuelo = true;
            cantidadSaltosJugador = 2;
        }

        if(collision.gameObject.tag == "Muerte")
        {
            morir();
        }
        
        if(collision.gameObject.tag == "Moneda")
        {
            tocaMoneda = true;
            Destroy(collision.gameObject);
            contadorMonedas++;
            textoMonedas.text = $"Monedas: {contadorMonedas}";
            //textoMonedas.text = (int.Parse(textoMonedas.text) + 1).ToString();
        }

        if(collision.gameObject.tag == "Llave")
        {
            tienellave = true;
            Destroy(collision.gameObject);
            puerta.SetActive(false);
        }

        if(collision.gameObject.tag == "Victoria")
        {
            TextoVictoria.SetActive(true);
            textoMonedasTotales.text = $"Total Monedas: {contadorMonedas}";
            Time.timeScale = 0f;
        }

        if(collision.gameObject.tag == "Abismo")
        {
            Abismo.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Abismo")
        {
            Abismo.SetActive(false);
        }
    }

    #endregion

    #region Otros Metodos

    private void saltar()
    {
        tiempoEntreSaltos = delay;
        rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        tocaSuelo = false;
        cantidadSaltosJugador--;

    }
    private void DaniarPlayer()
    {
        salud--;
        Debug.Log("El jugador ha Perdido 1 de vida por el enemigo");
        if(salud <= 0)
        {
            morir();
            
        }
    }

    private void morir()
    {
        muerto = true;
        vidasActuales--;
        textoVidas.text = $"Vidas: {vidasActuales}";
        if (vidasActuales <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }


    #endregion


    /*
    + Para cuando queramos pausar el juego completo: 
    + Time.timeScale = 0f;
    */
}
