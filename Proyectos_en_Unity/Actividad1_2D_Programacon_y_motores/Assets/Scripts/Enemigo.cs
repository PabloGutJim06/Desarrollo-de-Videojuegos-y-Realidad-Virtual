using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private Rigidbody2D rb; // Referencia al componente de física, guardada para no buscarla cada frame.
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private float direccion = -1f; // -1 = izquierda, 0 = quieto, 1 = derecha.

    // Awake se llama una vez, al cargarse el objeto, antes de Start.
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   // FixedUpdate se ejecuta a intervalos fijos, sincronizado con el motor de física.
    // Es el lugar correcto para modificar un Rigidbody2D.
    private void FixedUpdate()
    {
        // Sustituimos la velocidad horizontal y conservamos la vertical
        // (así la gravedad sigue funcionando).
        rb.linearVelocity = new Vector2(direccion * velocidad, rb.linearVelocity.y);
    }
}
