using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Prototype.NetworkLobby{
	public class TankLobby_Hook : Prototype.NetworkLobby.LobbyHook {
		public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager NetworkManager, GameObject lobbyPlayer, GameObject gamePlayer)
		{
			LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();

			TankColor tank = gamePlayer.GetComponent<TankColor>();
			tank.color = lobby.playerColor;

            TankManager tm = gamePlayer.GetComponent<TankManager>();
            tm.m_PlayerName = lobby.playerName;
		}
	}
}