using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager :MonoBehaviour {
    private readonly float _delay = 1f;
    private KhsManager _km;
    private ValueManager _vm;

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

        _km = GetComponent<KhsManager>();
        _vm = GetComponent<ValueManager>();

    }

    private void Start() {
        oldSpeed = _vm.GetValue("robotSpeed").Value;
        StartCoroutine(Proc());
    }

    IEnumerator Proc() {
        while (true) {
            yield return new WaitForSeconds(_delay);
            _robots = FindObjectsOfType<Robot>();
            _containers = FindObjectsOfType<RobotContainer>();
            _requestInventories = FindObjectsOfType<RequestInventory>();
            _inventories = FindObjectsOfType<Inventory>();

            var newSpeed = _vm.GetValue("robotSpeed").Value;
            if (oldSpeed != newSpeed) {
                oldSpeed = newSpeed;
                foreach (var r in _robots) {
                    r.speed = newSpeed;
                }
            }
            
            foreach (var requestInventory in _requestInventories) {
                foreach (var stack in requestInventory.GetStacks()) {
                    var nearestInventory = KhsUtil.GetNearestObject<Inventory>(_inventories, requestInventory.transform.position, x => x.GetItemCount(stack.name) > 0);
                    if (nearestInventory == null)
                        continue;

                    var nearestContainer = KhsUtil.GetNearestObject<RobotContainer>(_containers, nearestInventory.transform.position, x => x.count > 0);
                    if (nearestContainer == null)
                        continue;
                    var nearestRequestInventory = nearestInventory.GetComponent<RequestInventory>();
                    if (nearestRequestInventory != null) {
                        if (nearestRequestInventory.GetItemCount(stack.name) >= nearestInventory.GetItemCount(stack.name))
                            continue;
                    }

                    int have = requestInventory.GetComponent<Inventory>().GetItemCount(stack.name);

                    var robotCapacity = _vm.GetValue("robotCapacity").Value;
                    int needRobotCount = (int)Math.Ceiling((float)(stack.count - have) / robotCapacity);
                    needRobotCount -= requestInventory.incomingRobotCount;
                    needRobotCount = Math.Min(nearestContainer.count, needRobotCount);


                    for (int i = 0; i < needRobotCount; i++) {
                        requestInventory.incomingRobotCount += 1;
                        nearestContainer.count -= 1;
                        var robot = _km.Instantiate("robot").GetComponent<Robot>();
                        robot.transform.position = nearestContainer.transform.position;
                        robot.RefreshSprite();
                        robot.inventoryPos = nearestInventory.transform.position;
                        robot.requestInventoryPos = requestInventory.transform.position;
                        robot.containerPos = nearestContainer.transform.position;
                        robot.speed = newSpeed;
                        robot.willItem = stack.name;
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
    }

}