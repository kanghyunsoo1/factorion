using System;
using UnityEngine;

public class Robot :RiceCakeComponent {/*
    public ItemBundle bundle;
    public Vector2 destination;
    public Vector2 inventoryPos;
    public Vector2 requestInventoryPos;
    public Vector2 containerPos;
    public enum RobotPhase { ToInventory, ToRequestInventory, ToContainer }
    public RobotPhase phase;
    public float speed;
    public string willItem;
    public int willCount;

    private SpriteRenderer _spriteRenderer;
    private AreaManager _areaManager;
    private SpriteManager _spriteManager;

    private void Awake() {
        _areaManager = FindObjectOfType<AreaManager>();
        _spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        _spriteManager = FindObjectOfType<SpriteManager>();
    }

    private void Start() {
        phase = RobotPhase.ToInventory;
        destination = inventoryPos;
    }

    public void RefreshSprite() {
        if (bundle == null || bundle.count == 0) {
            _spriteRenderer.sprite = null;
        } else {
            _spriteRenderer.sprite = _spriteManager.GetSprite("item", bundle.name);
        }
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    private void FixedUpdate() {
        if (Vector2.Distance(transform.position, destination) <= 0.3f) {
            try {
                switch (phase) {
                    case RobotPhase.ToInventory:
                        var inventory = _areaManager.GetUser(destination).GetComponent<Inventory>();
                        int how = inventory.PullItem(willItem, willCount);
                        bundle = new ItemBundle(willItem,how);
                        RefreshSprite();
                        if (how == 0) {
                            destination = containerPos;
                            phase = RobotPhase.ToContainer;
                            break;
                        }
                        destination = requestInventoryPos;
                        phase = RobotPhase.ToRequestInventory;
                        break;

                    case RobotPhase.ToRequestInventory:
                        var requestInventory = _areaManager.GetUser(destination).GetComponent<RequestInventory>();
                        requestInventory.GetComponent<Inventory>().AddItem(bundle.name, bundle.count);
                        requestInventory.incomingRobotCount -= 1;
                        bundle = null;
                        RefreshSprite();
                        destination = containerPos;
                        phase = RobotPhase.ToContainer;
                        break;
                    case RobotPhase.ToContainer:
                        var container = _areaManager.GetUser(destination).GetComponent<RobotContainer>();
                        container.count++;
                        Destroy(gameObject);
                        break;
                }
            } catch (Exception) {
                phase = RobotPhase.ToContainer;
                destination = RiceCakeUtil.GetNearestObject<RobotContainer>(FindObjectsOfType<RobotContainer>(), transform.position).transform.position;
            }
        }
    }*/
}