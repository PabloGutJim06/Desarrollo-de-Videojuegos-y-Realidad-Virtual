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
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] GameObject puerta;
    [SerializeField] Vector3 VolverTrasMuerte = new Vector3(0.37f, -8.77f, 0f);

    #endregion

    #region Variables Privadas
    // Muerte
    bool muerto = false;
    //Saltar
    bool tocaSuelo = false;
    //Monedas
    bool tocaMoneda = false; 
    int contadorMonedas = 0;
    //Llave
    bool tienellave = false;
    private Rigidbody2D rb;
    private int vidasActuales = 5;

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
            Debug.Log("El Personaje ha muerto");
            explosionFX.transform.position = transform.position;
            explosionFX.Play();
            //transform.position = new Vector3(VolverTrasMuerte.x, VolverTrasMuerte.y, transform.position.z);
            rb.transform.position = VolverTrasMuerte;
            muerto = false;
        }

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && tocaSuelo)
        {
            rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            tocaSuelo = false;
        }
        movimientoX = Input.GetAxis("Horizontal");

        rb.AddForce(new Vector2(movimientoX * velocidadX, 0));
    }
    #endregion

    #region Metodos Colisiones
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Suelo")
        {
            Debug.Log("He detectado un trigger de suelo");
            tocaSuelo = true;
        }

        if(collision.gameObject.tag == "Muerte")
        {
            Debug.Log("He detectado un trigger de muerte");
            muerto = true;
            vidasActuales--;
            textoVidas.text = $"Vidas: {vidasActuales}";
        }
        
        if(collision.gameObject.tag == "Moneda")
        {
            Debug.Log("He detectado un trigger de moneda");
            tocaMoneda = true;
            Destroy(collision.gameObject);
            contadorMonedas++;
            textoMonedas.text = $"Monedas: {contadorMonedas}";
            //textoMonedas.text = (int.Parse(textoMonedas.text) + 1).ToString();
        }

        if(collision.gameObject.tag == "Llave")
        {
            Debug.Log("Tengo la llave");
            tienellave = true;
            Destroy(collision.gameObject);
            puerta.SetActive(false);
        }
    }

    #endregion


    /*
    + Para cuando queramos pausar el juego completo: 
    + Time.timeScale = 0f;
    */
}
