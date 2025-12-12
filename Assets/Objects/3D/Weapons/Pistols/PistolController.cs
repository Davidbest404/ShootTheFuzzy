using UnityEngine;

public class PistolController : MonoBehaviour
{
    public GameObject projectilePrefab;       // Префаб пули
    public GameObject muzzleFlashPrefab;      // Префаб пышки
    public Transform firePoint;               // Точка выстрела
    public float reloadTime = 2f;             // Время перезарядки
    public float fireRate = 0.5f;             // Интервал между выстрелами (секунды)
    public int maxAmmo = 15;                  // Максимальное кол-во патронов
    public float BSpeed = 10f;                // Скорость пули

    private bool isReloading = false;          // Флаг перезарядки
    private float nextFireTime = 0f;          // Следующее доступное время выстрела
    private int currentAmmo;                   // Текущее количество патронов

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (!isReloading && Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time > nextFireTime && currentAmmo > 0)
        {
            ShowMuzzleFlash();
            nextFireTime = Time.time + fireRate;

            FireProjectile();

            currentAmmo--;

            if (currentAmmo <= 0)
                Reload();
        }
    }

    void FireProjectile()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * BSpeed);
    }

    void Reload()
    {
        isReloading = true;
        Invoke("FinishReload", reloadTime);
    }

    void FinishReload()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void ShowMuzzleFlash()
    {
        GameObject flashInstance = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);

        Destroy(flashInstance, 0.1f); // Здесь 0.1 секунды - продолжительность эффекта
    }
}