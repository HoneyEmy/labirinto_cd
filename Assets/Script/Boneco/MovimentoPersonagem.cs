using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class MovimentoPersonagem : MonoBehaviour
{
    [Header("Movimentação")]
    public float velocidade = 7f;

    private Rigidbody2D rb;
    private Vector2 movimento;

    [Header("Coletáveis")]
    public Tilemap tilemapColetaveis;

    public static int quantidadeColetada = 0;

    [Header("Posição da coleta")]
    public Vector2 deslocamentoColeta = new Vector2(0f, -0.3f);

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        quantidadeColetada = 0;
    }

    void Update()
    {
        movimento = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            movimento.y = 1;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            movimento.y = -1;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            movimento.x = -1;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            movimento.x = 1;

        movimento = movimento.normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movimento * velocidade;
    }

    private void OnTriggerStay2D(Collider2D outro)
    {
        if (tilemapColetaveis == null)
            return;

        if (outro.gameObject == tilemapColetaveis.gameObject)
        {
            Vector3 posicao = transform.position + (Vector3)deslocamentoColeta;
            Vector3Int celula = tilemapColetaveis.WorldToCell(posicao);

            if (tilemapColetaveis.HasTile(celula))
            {
                tilemapColetaveis.SetTile(celula, null);
                quantidadeColetada++;
                Debug.Log("Itens coletados: " + quantidadeColetada);
            }
        }
    }
}