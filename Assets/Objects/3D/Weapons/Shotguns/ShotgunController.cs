using UnityEngine;

public class ShotgunController : MonoBehaviour
{
    public GameObject projectilePrefab;     // Префаб пули
    public GameObject muzzleFlashPrefab;    // Префаб пышки
    public Transform firePoint;             // Точка выстрела
    public float reloadTime = 4f;           // Время перезарядки
    public float fireRate = 1f;             // Интервал между выстрелами
    public int projectilesPerShot = 5;      // Число снарядов за выстрел
    public int maxAmmo = 8;                 // Максимальное кол-во патронов
    public float BSpeed = 10f;                // Скорость пули

    private bool isReloading = false;        // Флаг перезарядки
    private float nextFireTime = 0f;        // Следующее доступное время выстрела
    private int currentAmmo;                // Текущее количество патронов

    private Vector3 randomSpreadDir;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (!isReloading && Input.GetMouseButtonDown(0))
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

            for (int i = 0; i < projectilesPerShot; i++)
            {
                FireProjectile();
            }

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
        // Создание мгновенной вспышки
        GameObject flashInstance = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);

        // Через некоторое время удаляем вспышку
        Destroy(flashInstance, 0.1f); // Здесь 0.1 секунды - продолжительность эффекта
    }
}