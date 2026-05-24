using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seguirJugador : MonoBehaviour
{
    public Transform player; // Referencia al objeto jugador
    public Vector2 minXAndY; // Punto X e Y mínimo que la cámara puede seguir
    public Vector2 maxXAndY; // Punto X e Y máximo que la cámara puede seguir

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minXAndY.x, maxXAndY.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minXAndY.y, maxXAndY.y);
            transform.position = targetPosition;
        }
    }
}
