using UnityEngine;

public class TurretControl : MonoBehaviour
{
    Transform _player;
    float dist;
    public float maxDist;
    public Transform head, barrel;
    public GameObject _projectile;
    public float _projectileSpeed = 5000;
    public float _fireRate, _nextFire;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(_player.position, transform.position);

        if(dist <= maxDist)
        {
            head.LookAt(_player);
            if (Time.time >= _nextFire) 
            {
                _nextFire = Time.time + 1f / _fireRate;
                Shoot();
            }
        }

    }

    void Shoot()
    {
        GameObject clone = Instantiate(_projectile, barrel.position, head.transform.rotation);
        clone.GetComponent<Rigidbody>().AddForce(head.forward * _projectileSpeed);
        Destroy(clone, 2);
    }
}
