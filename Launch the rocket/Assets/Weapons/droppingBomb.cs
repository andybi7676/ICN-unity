using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droppingBomb : MonoBehaviour
{
    [SerializeField]public GameObject bombPrefab;
    public Animator player;
    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
      if (Client.instance.myId!= transform.parent.GetComponent<PlayerManager>().id){
        return;
      }
      if(Input.GetKeyDown(KeyCode.B)){
        DropBomb();
        ClientSend.DropBomb(this.gameObject.transform.position);
      }
    }

    void DropBomb(){
      Instantiate(bombPrefab,this.gameObject.transform.position,Quaternion.identity);
    }

    IEnumerator actionTime(){
      yield return new WaitForSeconds(2);
      player.SetBool("isBombed",false);
      rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
    }

    public void getBombed(){
      player.SetBool("increase_metal",false);
      player.SetBool("increase_water",false);
      player.SetBool("increase_coal",false);
      player.SetBool("isBombed",true);
      rb.constraints = RigidbodyConstraints2D.FreezePosition |RigidbodyConstraints2D.FreezeRotation;
      StartCoroutine(actionTime());
    }
}
