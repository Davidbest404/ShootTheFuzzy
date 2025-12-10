using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SpriteRenderer hitSpritePrefab; // спрайт для эффекта попадания
    public float spriteDuration = 0.1f;   // Длительность существования спрайта

    public float lifetime = 10f; // Продолжительность жизни пули в секундах

    void Start()
    {
        Invoke("SelfDestruct", lifetime);
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall&Decore"))
        {
            CreateHitEffect(collision.contacts[0].point);

            Destroy(gameObject);
        }
    }

    private void CreateHitEffect(Vector3 position)
    {
        SpriteRenderer sprite = Instantiate(hitSpritePrefab, position, Quaternion.identity);
        Destroy(sprite.gameObject, spriteDuration);
    }
}