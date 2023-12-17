using DG.Tweening;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private PlayerController _playerController;
    private ObjectPoolingSO _bulletPooling;
    private readonly float _fireRate = 1f;
    private float _lastShootTime;

    [SerializeField] private Transform shootPoint;

    private void Awake()
    {
        GetReference();
        InitValues();
    }
    
    internal void Fire()
    {
        if (Time.time > _lastShootTime + _fireRate)
        {
            _lastShootTime = Time.time;

            GameObject bullet = _bulletPooling.GetPooledObject();
            bullet.SetActive(true);
            bullet.transform.position = shootPoint.position;
            bullet.transform.DOMove(_playerController.transform.position, 0.5f)
                .SetEase(Ease.InQuint)
                .OnComplete(() =>
                {
                    var list = _playerController.stackList;
                    GameObject lastHuman = list[^1];
                    list.Remove(lastHuman);
                    Destroy(lastHuman);
                    
                    _bulletPooling.ReturnToPool(bullet);
                });
        }
    }
    
    private void GetReference()
    {
        _playerController = PlayerController.Instance;
        _bulletPooling = Resources.Load<ObjectPoolingSO>("Data/Pooling/BulletPooling");
    }
    
    private void InitValues()
    {
        _bulletPooling.InitializeObjectPool("BulletPool");
    }
}
