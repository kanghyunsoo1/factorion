﻿public class RobotManager :Manager {/*
    private readonly float _delay = 1f;
    private RiceCakeManager _riceCakeManager;
    private ValueManager _valueManager;
    private Robot[] _robots;
    private RobotContainer[] _containers;
    private RequestInventory[] _requestInventories;
    private Inventory[] _inventories;
    private float oldSpeed;

    private void Awake() {
        _robots = new Robot[0];
        _containers = new RobotContainer[0];
        _requestInventories = new RequestInventory[0];
        _inventories = new Inventory[0];
        _riceCakeManager = GetComponent<RiceCakeManager>();
        _valueManager = GetComponent<ValueManager>();
    }

    private void Start() {
        oldSpeed = _valueManager.GetValue("robotSpeed").Value;
        StartCoroutine(Proc());
    }

    IEnumerator Proc() {
        while (true) {
            yield return new WaitForSeconds(_delay);
            _robots = FindObjectsOfType<Robot>();
            _containers = FindObjectsOfType<RobotContainer>();
            _requestInventories = FindObjectsOfType<RequestInventory>();
            _inventories = FindObjectsOfType<Inventory>();

            var newSpeed = _valueManager.GetValue("robotSpeed").Value;
            if (oldSpeed != newSpeed) {
                oldSpeed = newSpeed;
                foreach (var r in _robots) {
                    r.speed = newSpeed;
                }
            }

            foreach (var requestInventory in _requestInventories) {
                foreach (var item in requestInventory.GetBundles()) {
                    var inventoryOfRequestInventory = requestInventory.GetComponent<Inventory>();
                    var nearestInventory = RiceCakeUtil.GetNearestObjectExcept<Inventory>(_inventories, requestInventory.transform.position, x => x.GetItemCount(item.name) > 0, inventoryOfRequestInventory);
                    if (nearestInventory == null)
                        continue;

                    var nearestContainer = RiceCakeUtil.GetNearestObject<RobotContainer>(_containers, nearestInventory.transform.position, x => x.count > 0);
                    if (nearestContainer == null)
                        continue;

                    var requestInventoryOfnearestInventory = nearestInventory.GetComponent<RequestInventory>();
                    if (requestInventoryOfnearestInventory != null && requestInventoryOfnearestInventory.GetItemCount(item.name) >= nearestInventory.GetItemCount(item.name)) {
                        continue;
                    }

                    int itemCountOfInventoryOfRequestInventory = inventoryOfRequestInventory.GetItemCount(item.name);

                    var robotCapacity = _valueManager.GetValue("robotCapacity").Value;
                    int needRobotCount = (int)Math.Ceiling((float)(item.count - itemCountOfInventoryOfRequestInventory) / robotCapacity);
                    needRobotCount -= requestInventory.incomingRobotCount;
                    needRobotCount = Math.Min(nearestContainer.count, needRobotCount);

                    for (int i = 0; i < needRobotCount; i++) {
                        requestInventory.incomingRobotCount += 1;
                        nearestContainer.count -= 1;
                        var robot = _riceCakeManager.Instantiate("robot").GetComponent<Robot>();
                        robot.transform.position = nearestContainer.transform.position;
                        robot.RefreshSprite();
                        robot.inventoryPos = nearestInventory.transform.position;
                        robot.requestInventoryPos = requestInventory.transform.position;
                        robot.containerPos = nearestContainer.transform.position;
                        robot.speed = newSpeed;
                        robot.willItem = item.name;
                        robot.willCount = (int)robotCapacity;
                        yield return new WaitForSeconds(0.1f);
                    }
                }
            }
        }
    }

    public int GetReadyCount() {
        var r = 0;
        foreach (var i in _containers)
            r += i.count;
        return r;
    }

    public int GetActiveCount() {
        return _robots.Length;
    }*/
}