using UnityEngine;

public class Block : MonoBehaviour
{

    public bool Breakable;

    private bool _beingMined = false;
    private float _amountMined = 0;

    private bool _hidden = true;
    public bool Hidden { get => _hidden; }

    public float Health = 3.0f;

    public int damageWhenBroken = 0;

    public Sprite BlockSprite;
    public Sprite HiddenSprite;

    private SpriteRenderer _renderer;
    private float minDistort = 0.8f;
    private float maxDistort = 1.2f;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = _hidden ? HiddenSprite : BlockSprite;
    }

    public virtual void OnBlockBreak()
    {

    }

    public void Update()
    {
        if (!_beingMined && _amountMined > 0)
        {
            _amountMined = Mathf.Max(_amountMined - Time.deltaTime / 2.0f, 0);
        }

        _beingMined = false;

        _renderer.color = Color.Lerp(Color.white, Color.red, _amountMined / Health);

        float distortX = Mathf.Lerp(1, Random.Range(minDistort, maxDistort), _amountMined / Health);
        float distortY = Mathf.Lerp(1, Random.Range(minDistort, maxDistort), _amountMined / Health);
        transform.localScale = new Vector3(distortX, distortY, 0);
    }

    /// <summary>
    /// Call during Update()
    /// </summary>
    /// <returns></returns>
    public bool Mine()
    {
        _beingMined = true;
        _amountMined += Time.deltaTime;

        if (_amountMined >= Health)
        {
            Break();
            return true;
        }
        return false;
    }

    public void Break()
    {
        OnBlockBreak();
        Destroy(gameObject);
    }

    public void Reveal()
    {
        _hidden = false;
        GetComponent<SpriteRenderer>().sprite = BlockSprite;
    }

    public void Hide()
    {
        _hidden = true;
        GetComponent<SpriteRenderer>().sprite = HiddenSprite;
    }
}
