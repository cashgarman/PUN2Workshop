using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerList : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerListing _playerListingPrefab;

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        var playerListing = Instantiate(_playerListingPrefab, transform);
        playerListing.SetInfo(newPlayer);

        Debug.Log($"{newPlayer.NickName} joined the room");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        for(var i = 0; i < transform.childCount; ++i)
        {
            var child = transform.GetChild(i);
            if (child.GetComponent<PlayerListing>()._nameText.text == otherPlayer.NickName)
            {
                Destroy(child.gameObject);
            }
        }
        
        Debug.Log($"{otherPlayer.NickName} left the room");
    }
}