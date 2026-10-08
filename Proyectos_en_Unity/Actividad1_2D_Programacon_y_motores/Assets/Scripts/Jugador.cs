using TMPro;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField] float velocidadX = 3.0f;
    [SerializeField] float velocidadY = 3.0f;
    [SerializeField] float movimientoX = 0f;
    [SerializeField] float movimientoY = 0f;
    
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
        //Otra forma de controlar el movimiento del jugador es con Input.GetAxis("Horizontal") y Input.GetAxis("Vertical")
        movimientoX = Input.GetAxis("Horizontal");
        movimientoY = Input.GetAxis("Vertical");

        if(!tocaSuelo)
        {
            movimientoY = 0f;
        }

        if(Input.GetKeyDown(KeyCode.Space) && tocaSuelo)
        {
            rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            tocaSuelo = false;
        }

        rb.AddForce(new Vector2(movimientoX * velocidadX, 0));
        rb.AddForce(new Vector2(0, movimientoY * velocidadY * 7));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Suelo")
        {
            tocaSuelo = true;
        }
    }
}
