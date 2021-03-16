using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerListing : MonoBehaviour
{
	[SerializeField] public TMP_Text _nameText;

	public void SetInfo(Player newPlayer)
	{
		_nameText.text = newPlayer.NickName;
	}
}