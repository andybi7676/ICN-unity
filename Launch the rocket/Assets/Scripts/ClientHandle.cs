using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector2 _position = _packet.ReadVector2();
        Debug.Log(_id+ ", pos: "+ _position);
        GameManager.players[_id].transform.GetChild(2).GetComponent<Rigidbody2D>().MovePosition(new Vector3(_position.x, _position.y, GameManager.players[_id].transform.GetChild(2).position.z));
    }

    public static void PlayerDisconnected(Packet _packet){
        int _id = _packet.ReadInt();
        Destroy(GameManager.players[_id]);
        GameManager.players.Remove(_id);
    }
    public static void PlayerProgressBar(Packet _packet)
    {
        Vector3 _progressBar = _packet.ReadVector3();
        Debug.Log("ProgressBar: " + _progressBar);
        if(_progressBar.x >= 0)
        {
            ProgressBar.instance.coalSlider.SetAmount((int)_progressBar.x);
        }
        if(_progressBar.y >= 0)
        {
            ProgressBar.instance.waterSlider.SetAmount((int)_progressBar.y);
        }
        if(_progressBar.z >= 0)
        {
            ProgressBar.instance.metalSlider.SetAmount((int)_progressBar.z);
        }
    }

    public static void PlayerUseWeapon(Packet _packet){
        int _id = _packet.ReadInt();
        bool useWeapon = _packet.ReadBool();

        if (useWeapon){
            Vector3 pos = _packet.ReadVector3();
            //GameObject weapon = Instantiate(GameManager.players[_id].transform.GetChild(2).GetComponent<playerMovement>().gun, pos, Quaternion.identity, GameManager.players[_id].transform.GetChild(2));
            Transform astronaut = GameManager.players[_id].transform.GetChild(2);
            GameObject weapon = Instantiate(astronaut.GetComponent<playerMovement>().gun, astronaut.GetComponent<playerMovement>().weaponSpawnPoint.position, Quaternion.identity, astronaut);
            weapon.transform.localScale = new Vector3(2.5f, 2.5f, 0.5f);
        }
        else{
            Destroy(GameManager.players[_id].transform.GetChild(2).GetChild(1).gameObject);
        }
    }

    public static void GunRotation(Packet _packet){
        int _id = _packet.ReadInt();
        Quaternion rot = _packet.ReadQuaternion();
        GameManager.players[_id].transform.GetChild(2).GetChild(1).rotation = rot;    
    }

    public static void SpawnBullet(Packet _packet){
        int _id = _packet.ReadInt();
        Vector3 shootPos = _packet.ReadVector3();
        Quaternion shootRot = _packet.ReadQuaternion();

        Transform gun = GameManager.players[_id].transform.GetChild(2).GetChild(1);
        Weapon gunScript = gun.GetComponent<Weapon>();
        //Instantiate(gunScript.shotEffect, gunScript.shotPoint.position, Quaternion.identity);
        //Instantiate(gunScript.projectile, gunScript.shotPoint.position, gun.rotation);
        Instantiate(gunScript.shotEffect, shootPos, Quaternion.identity);
        Instantiate(gunScript.projectile, shootPos, shootRot);
    }

    public static void DropBomb(Packet _packet){
        int _id = _packet.ReadInt();
        Vector3 dropPos = _packet.ReadVector3();

        droppingBomb player = GameManager.players[_id].transform.GetChild(2).GetComponent<droppingBomb>();
        Instantiate(player.bombPrefab, dropPos,Quaternion.identity);
    }


    /*public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }*/
}
