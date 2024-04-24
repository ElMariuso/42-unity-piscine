using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUIManager : MonoBehaviour
{
    [SerializeField] private GameObject turretPrefab;

    private void OnMouseDown()
    {
        if (true)
        {
            TurretDragHandler.Instance.StartDraggingTurret(turretPrefab);
            gameObject.SetActive(false);
        }
    }

    public void ResetIcon()
    {
        gameObject.SetActive(true);
    }
}
