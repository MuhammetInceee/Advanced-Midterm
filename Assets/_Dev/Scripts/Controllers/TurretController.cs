using DG.Tweening;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    
    private readonly float _fireRate = 1.5f;
    
    private PlayerController _playerController;
    private ObjectPoolingSO _bulletPooling;
    private Animator _animator;
    private float _lastShootTime;

    [SerializeField] private Transform shootPoint;

    private void Awake()
    {
        GetReference();
        InitValues();
    }
    
    internal void Fire()
    {
        transform.LookAt(_playerController.transform);
        if (Time.time > _lastShootTime + _fireRate && !_playerController.isInRightLine)
        {
            _lastShootTime = Time.time;

            _animator.SetTrigger(Shoot);
            GameObject bullet = _bulletPooling.GetPooledObject();
            bullet.SetActive(true);
            bullet.transform.position = shootPoint.position;
            bullet.transform.parent = _playerController.stackList[0].transform;
            bullet.transform.DOLocalMove(Vector3.up, 1f)
                .SetEase(Ease.InSine)
                .OnComplete(() =>
                {
                    _playerController.Score--;
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
        _animator = GetComponent<Animator>();
    }
    
    private void InitValues()
    {
        _bulletPooling.InitializeObjectPool("BulletPool");
    }
}
