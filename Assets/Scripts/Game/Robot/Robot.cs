using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot :KhsComponent {
    public ItemStack stack;
    public Vector2 destination;
    public Vector2 inventoryPos;
    public Vector2 requestInventoryPos;
    public Vector2 containerPos;
    public enum RobotPhase { ToInventory, ToRequestInventory, ToContainer }
    public RobotPhase phase;
    public float speed;
    public string willItem;
    public int willCount;

    private SpriteRenderer _sr;
    private AreaManager _am;
    private SpriteManager _sm;

    private void Awake() {
        _am = FindObjectOfType<AreaManager>();
        _sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        _sm = FindObjectOfType<SpriteManager>();
    }

    private void Start() {
        phase = RobotPhase.ToInventory;
        destination = inventoryPos;
    }

    public void RefreshSprite() {
        if (stack == null || stack.count == 0) {
            _sr.sprite = null;
        } else {
            _sr.sprite = _sm.GetSprite("item", stack.name);
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
                        var inventory = _am.GetUser(destination).GetComponent<Inventory>();
                        int how = inventory.PullItem(willItem, willCount);
                        stack = new ItemStack {
                            name = willItem,
                            count = how
                        };
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
                        var requestInventory = _am.GetUser(destination).GetComponent<RequestInventory>();
                        requestInventory.GetComponent<Inventory>().AddItem(stack.name, stack.count);
                        requestInventory.incomingRobotCount -= 1;
                        stack = null;
                        RefreshSprite();
                        destination = containerPos;
                        phase = RobotPhase.ToContainer;
                        break;
                    case RobotPhase.ToContainer:
                        var container = _am.GetUser(destination).GetComponent<RobotContainer>();
                        container.count++;
                        Destroy(gameObject);
                        break;
                }
            } catch (Exception) {
                phase = RobotPhase.ToContainer;
                destination = KhsUtil.GetNearestObject<RobotContainer>(FindObjectsOfType<RobotContainer>(), transform.position).transform.position;
            }
        }
    }

}
