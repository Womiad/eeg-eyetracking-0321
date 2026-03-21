using System.Collections;
using System;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    public Transform Head;
    public Transform RightHand;
    public Transform LeftHand;

    private Quaternion LastHeadRotation;

    private Vector3 lastLeftHandPosition;
    private Vector3 lastRightHandPosition;

    
    private int HeadMove;
    private int RightMove;
    private int LeftMove;

    void Start(){
        LastHeadRotation = Head.rotation;
        lastLeftHandPosition = LeftHand.position;
        lastRightHandPosition = RightHand.position;
        StartCoroutine(MovementLog());
    }

    
    void Update()
    {

    }

    IEnumerator MovementLog(){
        while(true){

            HeadMove = Quaternion.Angle(Head.rotation, LastHeadRotation) > 15 ? 1 : 0;

            // 計算右手移動距離
            if(RightHand.position != new Vector3(0,0,0)) {
                RightMove = Vector3.Distance(RightHand.position, lastRightHandPosition) > 0.30 ? 1 : 0;
            }else{
                RightMove = 0;
            }
            // 計算左手移動距離
            if(LeftHand.position != new Vector3(0,0,0)) {
                LeftMove = Vector3.Distance(LeftHand.position, lastLeftHandPosition) > 0.30 ? 1 : 0;
            }else{
                LeftMove = 0;
            }

            if(HeadMove == 1){
                Debug.Log("【動作】頭：動");
            }
            LastHeadRotation = Head.rotation;

            if(RightMove == 1){
                Debug.Log("【動作】右手:動");
            }
            if(RightHand.position != new Vector3(0,0,0)) lastRightHandPosition = RightHand.position;

            if(LeftMove == 1){
                Debug.Log("【動作】左手:動");
            }
            if(LeftHand.position != new Vector3(0,0,0)) lastLeftHandPosition = LeftHand.position;

            MovementCSVHandler.instance.WriteRecord("TimeStamp",MovementCSVHandler.instance.GetDateTime());
            MovementCSVHandler.instance.WriteRecord("Head",HeadMove.ToString());
            MovementCSVHandler.instance.WriteRecord("RightHand",RightMove.ToString());
            MovementCSVHandler.instance.WriteRecord("LeftHand",LeftMove.ToString());
            MovementCSVHandler.instance.WriteFile();

            yield return new WaitForSeconds(0.5f);
        }
    }
}
