using TMPro;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField] float velocidadX = 3.0f;
    [SerializeField] float velocidadY = 3.0f;
    [SerializeField] float movimientoX = 0f;
    [SerializeField] float movimientoY = 0f;
    [SerializeField] Vector2 VolverTrasMuerte = new Vector2(2.3f, -8.77f);

    bool muerto = false;
    bool tocaSuelo = false; 
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Accede a x componente del objeto al que esta asociado el Script.
    }

    // Update is called once per frame
    void Update()
    {
        if(!tocaSuelo)
        {
            movimientoY = 0f;
        }

        if (muerto){
            Debug.Log("El Personaje ha muerto");
            //transform.position = new Vector3(VolverTrasMuerte.x, VolverTrasMuerte.y, transform.position.z);
            rb.transform.position = new Vector3(2.3f, -8.77f, 0f);
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
        }
    }
}
