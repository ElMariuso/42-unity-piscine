using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TurretUIManager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private Image turretIconImage;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text fireRateText;

    // Attributes
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (turretIconImage == null)
            turretIconImage = GetComponentInChildren<Image>();
        if (turretPrefab != null)
            InitTurretStats();
    }

    void Update()
    {
        UpdateTurretIconColor();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        TurretController turretComponent = turretPrefab.GetComponent<TurretController>();
        if (turretComponent == null)
            return ;
        
        transform.position = startPosition;
        canvasGroup.blocksRaycasts = true;
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("Zone"));
        if (GameManager.Instance != null && hit.collider != null && GameManager.Instance.HasEnoughEnergy(turretComponent.cost))
        {
            GameObject turretInstance = Instantiate(turretPrefab, hit.point, Quaternion.identity);
            turretInstance.SetActive(true);
            GameManager.Instance.RemoveEnergy(turretComponent.cost);
        }
    }

    private void InitTurretStats()
    {
        TurretController turretComponent = turretPrefab.GetComponent<TurretController>();
        if (turretComponent != null)
        {
            damageText.text = $"{turretComponent.damages}";
            costText.text = $"{turretComponent.cost}";
            fireRateText.text = $"{turretComponent.fireRates}";
        }
    }

    private void UpdateTurretIconColor()
    {
        TurretController turretComponent = turretPrefab.GetComponent<TurretController>();
        if (turretComponent != null)
        {
            if (GameManager.Instance != null && GameManager.Instance.HasEnoughEnergy(turretComponent.cost))
                turretIconImage.color = Color.white;
            else
                turretIconImage.color = Color.gray;
        }
    }
}
