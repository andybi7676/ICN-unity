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
            _packet.Write(0);
            _packet.Write(_collection);

            SendTCPData(_packet);
        }
    }

    public static void PlayerUseWeapon(bool useWeapon, Vector3 pos){
        using (Packet _packet = new Packet((int)ClientPackets.playerUseWeapon))
        {
            _packet.Write(useWeapon);

            if(useWeapon){
                _packet.Write(pos);
            }
            SendTCPData(_packet);
        }
    }

    public static void GunRotation(Quaternion rot){
        using (Packet _packet = new Packet((int)ClientPackets.gunRotation))
        {
            _packet.Write(rot);
            SendUDPData(_packet);
        }
    }

    public static void SpawnBullet (Vector3 shootPos, Quaternion shootRot)
    {
        using (Packet _packet = new Packet((int)ClientPackets.spawnBullet))
        {
            _packet.Write(shootPos);
            _packet.Write(shootRot);

            SendTCPData(_packet);
        }
    }

    public static void DropBomb (Vector3 dropPos)
    {
        using (Packet _packet = new Packet((int)ClientPackets.dropBomb))
        {
            _packet.Write(dropPos);

            SendTCPData(_packet);
        }
    }

    #endregion
}
