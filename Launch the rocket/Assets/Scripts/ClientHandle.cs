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

        Debug.Log($"Message from server: {_msg}, your id: {_myId}");
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
        //Debug.Log(_id+ ", pos: "+ _position);
        GameManager.players[_id].transform.GetChild(2).GetComponent<Rigidbody2D>().MovePosition(new Vector3(_position.x, _position.y, GameManager.players[_id].transform.GetChild(2).position.z));
    }

    public static void PlayerDisconnected(Packet _packet){
        int _id = _packet.ReadInt();
        Destroy(GameManager.players[_id]);
        GameManager.players.Remove(_id);
    }
    public static void PlayerProgressBar(Packet _packet)
    {
        int _fromClient = _packet.ReadInt();
        if(_fromClient != Client.instance.myId && _fromClient != 0)
        {
            GameManager.players[_fromClient].transform.GetChild(2).GetComponent<actionAni>().MakeCollection();
        }
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

    public static void PlayerClock(Packet _packet)
    {
        float remainTime = _packet.ReadFloat();
        if(remainTime >= 0)
        {
            //Debug.Log(remainTime);
            countdownTimer.SetTimeRemaining(remainTime);
        }
    }

    public static void PlayerActionCollect(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _resource = _packet.ReadString();
        Debug.Log(_id + ", collected: " + _resource);
        GameManager.players[_id].transform.GetChild(2).GetComponent<actionAni>().MakeCollected(_resource);
    }

    /*public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }*/
}
