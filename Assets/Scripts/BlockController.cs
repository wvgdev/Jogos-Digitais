using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BlockController : MonoBehaviour
{
    [Tooltip("Se verdadeiro, prioriza movimento horizontal quando há componentes em X e Y")]
    public bool prioritizeHorizontal = true;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Garante que o bloco seja dinâmico e não rotacione
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void FixedUpdate()
    {
        Vector2 vel = rb.linearVelocity;

        // Se não há movimento, nada a fazer
        if (vel == Vector2.zero) return;

        // Decide qual componente manter
        if (prioritizeHorizontal)
        {
            // Se |vx| >= |vy|, mantém só vx; senão só vy
            if (Mathf.Abs(vel.x) >= Mathf.Abs(vel.y))
                vel.y = 0f;
            else
                vel.x = 0f;
        }
        else
        {
            // Prioriza vertical em caso de empate
            if (Mathf.Abs(vel.y) >= Mathf.Abs(vel.x))
                vel.x = 0f;
            else
                vel.y = 0f;
        }

        // Atualiza a velocidade com o componente escolhido
        rb.linearVelocity = vel;
    }
}