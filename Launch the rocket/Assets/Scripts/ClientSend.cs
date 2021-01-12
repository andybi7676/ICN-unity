using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }


    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("tom");

            SendTCPData(_packet);
        }
    }
    

    public static void PlayerMovement (Vector2 position)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(position);

            SendUDPData(_packet);
        }
        
    }

    public static void PlayerCollection (Vector3 _collection)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerCollection))
        {
            _packet.Write(true);
            _packet.Write(_collection);

            SendTCPData(_packet);
        }
    }

    public static void PlayerClockInit()
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerClockStart))
        {
            SendTCPData(_packet);
        }
    }

    public static void PlayerCollected(string _resource)
    {
        using ( Packet _packet = new Packet((int)ClientPackets.playerActionCollect))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(_resource);
            SendTCPData(_packet);
        }
        Debug.Log($"Collect {_resource}");
    }
    #endregion
}
