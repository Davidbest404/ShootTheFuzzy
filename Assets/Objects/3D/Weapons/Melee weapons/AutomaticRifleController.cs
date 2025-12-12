using UnityEngine;

public class AutomaticRifleController : MonoBehaviour
{
    public GameObject projectilePrefab;     // Префаб пули
    public GameObject muzzleFlashPrefab;    // Префаб пышки
    public Transform firePoint;             // Точка выстрела
    public float reloadTime = 3f;           // Время перезарядки
    public float fireRate = 0.1f;           // Интервал между выстрелами (быстрая очередь)
    public int maxAmmo = 30;                 // Максимальное кол-во патронов
    public float BSpeed = 10f;                // Скорость пули

    private bool isReloading = false;        // Флаг перезарядки
    private float nextFireTime = 0f;        // Следующее доступное время выстрела
    private int currentAmmo;                // Текущее количество патронов

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
        // Создание мгновенной вспышки
        GameObject flashInstance = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);

        // Через некоторое время удаляем вспышку
        Destroy(flashInstance, 0.1f); // Здесь 0.1 секунды - продолжительность эффекта
    }
}