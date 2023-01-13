using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _currentHealth;
    [SerializeField]  protected int _maxHealth;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _coins;
    [SerializeField] protected int _extraCoins;
    [SerializeField] protected int _damage;

    public int MaxHealth { get => _maxHealth;  set { _maxHealth = value; } }  
    public virtual float Speed { get => _speed; set { _speed = value; } }
    public virtual int Coins { get => _coins; set { _coins = value; } }
    public virtual int ExtraCoins { get => _extraCoins; set { _extraCoins = value; } }
    public virtual int Damage { get => _damage; set { _damage = value; } }

    protected Vector2 playerPosition = new Vector2(0, 0);

    public Rigidbody2D enemyRb;

    protected virtual void Awake()
    {
        RenameEnemyGameObject();
    }

    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
        enemyRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        Vector2 position2d = transform.position;

        Vector2 direction = playerPosition - position2d;
        direction.Normalize();

        transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);

        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Cannon"))
        {
            Destroy(gameObject);
            GameManager.PlayerHealth -= _damage;

        }     
    }

    void RenameEnemyGameObject()
    {
        if (transform.childCount == 0)
            gameObject.name = GameManager.EnemyCount.ToString();

        else
        {
            gameObject.name = GameManager.EnemyCount.ToString();
            gameObject.transform.GetChild(0).name = GameManager.EnemyCount.ToString();
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0) 
            Destroy();

    }

    private void Destroy()
    {
        Destroy(this.gameObject);
        GameManager.PlayerScore += _coins;
        GameManager.ExtraCoins += _extraCoins;
        Cannon.isDistanceToNearestTargenInFireRange = false;
        Debug.Log("object destroy!!");
    }




}
