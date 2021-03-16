using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
	[SerializeField] private GameObject _playerPrefabName;
	[SerializeField] private Vector3 _spawnPoint;

	private void Start()
	{
		// Create the character for the connected player
		PhotonNetwork.Instantiate(_playerPrefabName.name, _spawnPoint, Quaternion.identity, 0);
	}
	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log($"Leaving room");
			PhotonNetwork.LeaveRoom();
		}
	}

	public override void OnLeftRoom()
	{
		// Load the menu scene
		Debug.Log($"Loading menu");
		SceneManager.LoadScene("Menu");
	}

	private void OnGUI()
	{
		GUILayout.Label($"Is MasterClient: {PhotonNetwork.IsMasterClient}");
	}
}
