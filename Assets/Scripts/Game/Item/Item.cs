public class Item :RiceCakeComponent {
    public new string name;

    private AreaManager _areaManager;

    private void Awake() {
        ManagerManager.SetManagers(this);
    }

    private void Start() {
        _areaManager.RegisterItem(this);
    }

    private void OnDestroy() {
        try {
            _areaManager.UnregisterItem(this);
        } catch (System.Exception) { };
    }

}