using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    private PlayerController thePlayer;
    private CameraFollow theCamera;
    public Vector2 facingDirection = Vector2.zero;

    public string PlaceName;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        theCamera = FindObjectOfType<CameraFollow>();

        if (!thePlayer.nextPlaceName.Equals(PlaceName))
        {
            return;
        }

        thePlayer.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y,0);
        theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
        thePlayer.lastMovement = facingDirection;
    }

}
