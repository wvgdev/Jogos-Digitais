using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class playercontroller : MonoBehaviour
{
    [Header("Movement Settings")]
    public Rigidbody2D theRB;
    public float moveSpeed;
 

    [Header("Input System")]
    public InputActionReference moveInput;

    [Header("Initial Move & Fade")]
    public float initialOffset = 1f;
    public float initialMoveDuration = 1f;
    public float initialFadeDuration = 1f;

    private SpriteRenderer sr;
    private bool initialDone = false;

    public Animator anim;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        theRB.bodyType = RigidbodyType2D.Dynamic;
        theRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Start()
    {
        StartCoroutine(InitialMoveAndFade());
    }

    private IEnumerator InitialMoveAndFade()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.down * initialOffset;
        Color startColor = sr.color;
        Color endColor = startColor; endColor.a = 0f;

        float elapsed = 0f;
        float total = Mathf.Max(initialMoveDuration, initialFadeDuration);

        while (elapsed < total)
        {
            float tMove = Mathf.Clamp01(elapsed / initialMoveDuration);
            transform.position = Vector3.Lerp(startPos, endPos, tMove);

            float tFade = Mathf.Clamp01(elapsed / initialFadeDuration);
            sr.color = Color.Lerp(startColor, endColor, tFade);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        sr.color = endColor;
        initialDone = true;
    }

    void Update()
    {
        if (!initialDone) return;

        Vector2 input = moveInput.action.ReadValue<Vector2>();
        if (input == Vector2.zero)
        {
            theRB.linearVelocity = Vector2.zero;
        }
        else
        {
            Vector2 dir;
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                dir = new Vector2(Mathf.Sign(input.x), 0f);
            else
                dir = new Vector2(0f, Mathf.Sign(input.y));

            theRB.linearVelocity = dir * moveSpeed;
        }

        if (theRB.linearVelocity.x < 0f)
        {
           transform.localScale = new Vector3(-1f, 1f, 1f);     
        } else if(theRB.linearVelocity.x > 0f){
            transform.localScale = Vector3.one;
        }

        if (theRB.linearVelocity.y < 0f)
        {
           transform.localScale = new Vector3(-1f, 1f, 1f);     
        } else if(theRB.linearVelocity.y > 0f){
            transform.localScale = Vector3.one;
        }

        // Atualiza o parâmetro do Animator
        anim.SetFloat("speed", theRB.linearVelocity.magnitude);
    }
}
