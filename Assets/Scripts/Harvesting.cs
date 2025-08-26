using UnityEngine;

public class Harvesting : MonoBehaviour
{
    
    public Tool Tool
    {
        get
        {
            return _tool;
        }
        private set
        {
            if (_tool != value)
            {
                _tool = value;
                UpdateSprite();
            }
        }
    }

    private void UpdateSprite()
    {
        if (_tool != null)
        {
            _renderer.sprite = _tool.Sprite;
        }
        else
        {
            _renderer.sprite = null;
        }
    }

    [SerializeField] private Tool _tool;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Harvestable harvestable = collision.GetComponent<Harvestable>();
        if (harvestable != null)
        {
            int amountToHarvest = Random.Range(Tool.MinHarvest, Tool.MaxHarvest);
            harvestable.TryHarvest(Tool.Type, amountToHarvest);
        }
    }
}
