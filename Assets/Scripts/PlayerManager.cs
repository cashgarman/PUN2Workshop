using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Camera _camera;

    private void Start()
    {
        // Remove the camera if this isn't the local player
        if (!photonView.IsMine)
        {
            Debug.Log($"Removing non-local camera");
            Destroy(_camera.gameObject);
        }
    }
}
