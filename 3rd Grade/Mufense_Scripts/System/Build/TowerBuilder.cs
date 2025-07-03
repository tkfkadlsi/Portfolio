using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializedDictionary("Type", "Size")] public SerializedDictionary<TowerType, float> TowerSizeDictionary = new SerializedDictionary<TowerType, float>();

    [SerializeField] private List<Vector3> _offsets = new List<Vector3>();

    [SerializeField] private LayerMask _platformAndGround;
    [SerializeField] private LayerMask _platform;

    [SerializeField] private Material _canMat;
    [SerializeField] private Material _notMat;

    private MeshRenderer _renderer;
    private SphereCollider _sphereCollider;

    private TowerType _buildTowerType;
    private bool _isBuilding;

    private int _overlapCount;
    private bool _isOnPlatform;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _sphereCollider = GetComponent<SphereCollider>();

        _isBuilding = false;
        _renderer.enabled = false;
        _sphereCollider.isTrigger = true;
    }

    private void Update()
    {
        _renderer.enabled = _isBuilding;

        if(_isBuilding == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue, _platformAndGround))
            {
                Vector3 point = hit.point;
                transform.position = point;
            }

            if (_overlapCount > 0)
            {
                _renderer.material = _notMat;
                return;
            }

            CheckOnPlatform();

            if(_isOnPlatform == false)
            {
                _renderer.material = _notMat;
                return;
            }

            _renderer.material = _canMat;

            if(Input.GetMouseButtonDown(0))
            {
                Managers.Instance.Pool.PopObject(Managers.Instance.Data.ConvertData.TowerType2PoolType[_buildTowerType], transform.position);
                SuccessBuild();
            }
        }
    }

    public void TowerBuild(TowerType type)
    {
        _overlapCount = 0;

        _buildTowerType = type;
        _isBuilding = true;
        _renderer.enabled = true;
        Managers.Instance.UI.GetRootUI().GetCanvas<MenuCanvas>().SetEnable(false);

        transform.localScale = new Vector3(TowerSizeDictionary[_buildTowerType], 0.1f, TowerSizeDictionary[_buildTowerType]);
    }

    private void SuccessBuild()
    {
        _isBuilding = false;
        _renderer.enabled = false;
        Managers.Instance.UI.GetRootUI().GetCanvas<MenuCanvas>().SetEnable(true);
    }

    private void CheckOnPlatform()
    {
        Vector3 position = transform.position;

        bool onPlatform = true;

        Ray ray;
        RaycastHit hit;

        for(int i = 0; i < _offsets.Count; i++)
        {
            ray = new Ray(position + _offsets[i], Vector3.down);
            Physics.Raycast(ray, out hit, float.MaxValue, _platform);

            if(hit.collider == null)
            {
                onPlatform = false;
                break;
            }
        }

        _isOnPlatform = onPlatform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            _overlapCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Tower"))
        {
            _overlapCount--;
        }
    }
}