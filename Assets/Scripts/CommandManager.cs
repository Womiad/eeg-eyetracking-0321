using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using UnityEngine.SceneManagement;

public class CommandManager : MonoBehaviour
{

    private bool English = false;
    string[] command = new string[] {"請舉起你的雙手" , "請左右搖頭" , "請舉起右手" , "請不要眨眼直到此提示消失" , "請反覆眨眼"};
    string[] end_command = new string[] {"請放下" , "停止" , "請放下" , "可以眨眼" , "停止"};
    string[] command_code = new string[] {"HandsUp", "Wagging", "RightHand", "NoBlink", "Blink"}; // Handsup, Wagging, Object, NoBlink, Blink
    string[] blink_command = new string[] {"2秒後停止眨眼" , "1秒後停止眨眼","請不要眨眼直到此提示消失"};
    public VideoSceneManager VideoSceneManager;

    
    void Start()
    {
        if(PlayerPrefs.GetInt("English") == 1){
            English = true;
        }
        if (English) command = new string[] {"Please raise your hands." , "Please shake your head." , "Please raise your right hand" , "Don't blink until this disappears." , "Blink repeatedly."};
        if (English) end_command = new string[] {"Put down." , "Stop." , "Put down." , "You can blink." , "Stop."};
        if (English) blink_command = new string[] {"Don't blink after 2s." , "Don't blink after 1s.","Don't blink until this disappears."};
        if(PlayerPrefs.GetInt("Command") == 1){
            StartCoroutine(Display_Command());
        }else{
            CommandController.instance.SetTextActive(false);
        }
        
    }

    public IEnumerator Display_Command(){
        Debug.Log("command_StartCoroutine");
        CommandController.instance.SetTextActive(false);
        
        yield return new WaitForSeconds(60f + VideoSceneManager.trialFadeInTime + VideoSceneManager.trialFadeOutTime + VideoSceneManager.trialDisplayTime + VideoSceneManager.delayTime);//等待60秒後，video剩30秒

        int L , random;
        L = command.Length;

        random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
        int randomDisplayMoment = random % 2;//亂數選擇前15秒顯示或後15秒顯示

        random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
        int randomDisplayTimes = random % 2;//亂數選擇顯示1次或2次

        if(randomDisplayMoment == 0){
            //前15秒顯示
            if(randomDisplayTimes == 0){
                //顯示一次
                CommandController.instance.SetTextActive(true);
                random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
                int randomCommand = random % L;//取得亂數command

                if(command_code[randomCommand] == "NoBlink"){

                    CommandController.instance.UpdateText(blink_command[0]);
                    yield return new WaitForSeconds(1f);
                    CommandController.instance.UpdateText(blink_command[1]);
                    yield return new WaitForSeconds(1f);
                    CommandController.instance.UpdateText(blink_command[2]);

                    CommandCSVHandler.instance.WriteRecord("video_number",SkyboxVideoPlayerController.instance.Clip.number.ToString());//紀錄vidio num
                    CommandCSVHandler.instance.WriteRecord("command",command_code[randomCommand]);//紀錄command
                    CommandCSVHandler.instance.WriteRecord("excuted_time",VideoCSVHandler.instance.GetDateTime());//紀錄command開始時間

                    yield return new WaitForSeconds(3f);
                    CommandController.instance.UpdateText(end_command[randomCommand]);//停止動作
                    CommandCSVHandler.instance.WriteRecord("end_time",VideoCSVHandler.instance.GetDateTime());//紀錄command結束時間
                    yield return new WaitForSeconds(2f);//顯示2秒

                    CommandController.instance.SetTextActive(false);//關閉

                    CommandCSVHandler.instance.WriteFile();//把紀錄寫進檔案
                }else{

                    CommandController.instance.UpdateText(command[randomCommand]);//設定顯示command

                    CommandCSVHandler.instance.WriteRecord("video_number",SkyboxVideoPlayerController.instance.Clip.number.ToString());//紀錄vidio num
                    CommandCSVHandler.instance.WriteRecord("command",command_code[randomCommand]);//紀錄command
                    CommandCSVHandler.instance.WriteRecord("excuted_time",VideoCSVHandler.instance.GetDateTime());//紀錄command開始時間

                    yield return new WaitForSeconds(5f);//顯示5秒
                    CommandController.instance.UpdateText(end_command[randomCommand]);//停止動作
                    yield return new WaitForSeconds(2f);//顯示2秒

                    CommandController.instance.SetTextActive(false);//關閉
                    CommandCSVHandler.instance.WriteRecord("end_time",VideoCSVHandler.instance.GetDateTime());//紀錄command結束時間

                    CommandCSVHandler.instance.WriteFile();//把紀錄寫進檔案 
                }        
    

                Debug.Log("前15秒顯示一次：" + command[randomCommand]);

            }else{
                //顯示兩次
                CommandController.instance.SetTextActive(true);
                random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
                int randomCommand = random % L;//取得亂數command

                if(command_code[randomCommand] == "NoBlink"){

                    CommandController.instance.UpdateText(blink_command[0]);
                    yield return new WaitForSeconds(1f);
                    CommandController.instance.UpdateText(blink_command[1]);
                    yield return new WaitForSeconds(1f);
                    CommandController.instance.UpdateText(blink_command[2]);

                    CommandCSVHandler.instance.WriteRecord("video_number",SkyboxVideoPlayerController.instance.Clip.number.ToString());//紀錄vidio num
                    CommandCSVHandler.instance.WriteRecord("command",command_code[randomCommand]);//紀錄command
                    CommandCSVHandler.instance.WriteRecord("excuted_time",VideoCSVHandler.instance.GetDateTime());//紀錄command開始時間

                    yield return new WaitForSeconds(3f);
                    CommandController.instance.UpdateText(end_command[randomCommand]);//停止動作
                    CommandCSVHandler.instance.WriteRecord("end_time",VideoCSVHandler.instance.GetDateTime());//紀錄command結束時間
                    yield return new WaitForSeconds(2f);//顯示2秒

                    CommandController.instance.SetTextActive(false);//關閉

                    CommandCSVHandler.instance.WriteFile();//把紀錄寫進檔案
                }else{

                    CommandController.instance.UpdateText(command[randomCommand]);//設定顯示command

                    CommandCSVHandler.instance.WriteRecord("video_number",SkyboxVideoPlayerController.instance.Clip.number.ToString());//紀錄vidio num
                    CommandCSVHandler.instance.WriteRecord("command",command_code[randomCommand]);//紀錄command
                    CommandCSVHandler.instance.WriteRecord("excuted_time",VideoCSVHandler.instance.GetDateTime());//紀錄command開始時間

                    yield return new WaitForSeconds(5f);//顯示5秒
                    CommandController.instance.UpdateText(end_command[randomCommand]);//停止動作
                    yield return new WaitForSeconds(2f);//顯示2秒

                    CommandController.instance.SetTextActive(false);//關閉
                    CommandCSVHandler.instance.WriteRecord("end_time",VideoCSVHandler.instance.GetDateTime());//紀錄command結束時間

                    CommandCSVHandler.instance.WriteFile();//把紀錄寫進檔案 
                } 

                Debug.Log("前15秒顯示兩次(1/2)：" + command[randomCommand]);
                yield return new WaitForSeconds(3f);//等待3秒
                                CommandController.instance.SetTextActive(true);
                random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
                randomCommand = random % L;//取得亂數command

                if(command_code[randomCommand] == "NoBlink"){

                    CommandController.instance.UpdateText(blink_command[0]);
                    yield return new WaitForSeconds(1f);
                    CommandController.instance.UpdateText(blink_command[1]);
                    yield return new WaitForSeconds(1f);
                    CommandController.instance.UpdateText(blink_command[2]);

                    CommandCSVHandler.instance.WriteRecord("video_number",SkyboxVideoPlayerController.instance.Clip.number.ToString());//紀錄vidio num
                    CommandCSVHandler.instance.WriteRecord("command",command_code[randomCommand]);//紀錄command
                    CommandCSVHandler.instance.WriteRecord("excuted_time",VideoCSVHandler.instance.GetDateTime());//紀錄command開始時間

                    yield return new WaitForSeconds(3f);
                    CommandController.instance.UpdateText(end_command[randomCommand]);//停止動作
                    CommandCSVHandler.instance.WriteRecord("end_time",VideoCSVHandler.instance.GetDateTime());//紀錄command結束時間
                    yield return new WaitForSeconds(2f);//顯示2秒

                    CommandController.instance.SetTextActive(false);//關閉

                    CommandCSVHandler.instance.WriteFile();//把紀錄寫進檔案
                }else{

                    CommandController.instance.UpdateText(command[randomCommand]);//設定顯示command

                    CommandCSVHandler.instance.WriteRecord("video_number",SkyboxVideoPlayerController.instance.Clip.number.ToString());//紀錄vidio num
                    CommandCSVHandler.instance.WriteRecord("command",command_code[randomCommand]);//紀錄command
                    CommandCSVHandler.instance.WriteRecord("excuted_time",VideoCSVHandler.instance.GetDateTime());//紀錄command開始時間

                    yield return new WaitForSeconds(5f);//顯示5秒
                    CommandController.instance.UpdateText(end_command[randomCommand]);//停止動作
                    yield return new WaitForSeconds(2f);//顯示2秒

                    CommandController.instance.SetTextActive(false);//關閉
                    CommandCSVHandler.instance.WriteRecord("end_time",VideoCSVHandler.instance.GetDateTime());//紀錄command結束時間

                    CommandCSVHandler.instance.WriteFile();//把紀錄寫進檔案 
                }  

                Debug.Log("前15秒顯示兩次(2/2)：" + command[randomCommand]);
            }
        }else{
            //第10~25秒顯示
            yield return new WaitForSeconds(10f);//10秒後再回來(15秒會撞到fade out難以閱讀)

            if(randomDisplayTimes == 0){
                //顯示一次
                                CommandController.instance.SetTextActive(true);
                random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
                int randomCommand = random % L;//取得亂數command

                if(command_code[randomCommand] == "NoBlink"){

                    CommandController.instance.UpdateText(blink_command[0]);
                    yield return new WaitForSeconds(1f);
                    CommandController.instance.UpdateText(blink_command[1]);
                    yield return new WaitForSeconds(1f);
                    CommandController.instance.UpdateText(blink_command[2]);

                    CommandCSVHandler.instance.WriteRecord("video_number",SkyboxVideoPlayerController.instance.Clip.number.ToString());//紀錄vidio num
                    CommandCSVHandler.instance.WriteRecord("command",command_code[randomCommand]);//紀錄command
                    CommandCSVHandler.instance.WriteRecord("excuted_time",VideoCSVHandler.instance.GetDateTime());//紀錄command開始時間

                    yield return new WaitForSeconds(3f);
                    CommandController.instance.UpdateText(end_command[randomCommand]);//停止動作
                    CommandCSVHandler.instance.WriteRecord("end_time",VideoCSVHandler.instance.GetDateTime());//紀錄command結束時間
                    yield return new WaitForSeconds(2f);//顯示2秒

                    CommandController.instance.SetTextActive(false);//關閉

                    CommandCSVHandler.instance.WriteFile();//把紀錄寫進檔案
                }else{

                    CommandController.instance.UpdateText(command[randomCommand]);//設定顯示command

                    CommandCSVHandler.instance.WriteRecord("video_number",SkyboxVideoPlayerController.instance.Clip.number.ToString());//紀錄vidio num
                    CommandCSVHandler.instance.WriteRecord("command",command_code[randomCommand]);//紀錄command
                    CommandCSVHandler.instance.WriteRecord("excuted_time",VideoCSVHandler.instance.GetDateTime());//紀錄command開始時間

                    yield return new WaitForSeconds(5f);//顯示5秒
                    CommandController.instance.UpdateText(end_command[randomCommand]);//停止動作
                    yield return new WaitForSeconds(2f);//顯示2秒

                    CommandController.instance.SetTextActive(false);//關閉
                    CommandCSVHandler.instance.WriteRecord("end_time",VideoCSVHandler.instance.GetDateTime());//紀錄command結束時間

                    CommandCSVHandler.instance.WriteFile();//把紀錄寫進檔案 
                }  
                Debug.Log("後15秒顯示一次：" + command[randomCommand]);
            }else{
                //顯示兩次
                                CommandController.instance.SetTextActive(true);
                random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
                int randomCommand = random % L;//取得亂數command

                if(command_code[randomCommand] == "NoBlink"){

                    CommandController.instance.UpdateText(blink_command[0]);
                    yield return new WaitForSeconds(1f);
                    CommandController.instance.UpdateText(blink_command[1]);
                    yield return new WaitForSeconds(1f);
                    CommandController.instance.UpdateText(blink_command[2]);

                    CommandCSVHandler.instance.WriteRecord("video_number",SkyboxVideoPlayerController.instance.Clip.number.ToString());//紀錄vidio num
                    CommandCSVHandler.instance.WriteRecord("command",command_code[randomCommand]);//紀錄command
                    CommandCSVHandler.instance.WriteRecord("excuted_time",VideoCSVHandler.instance.GetDateTime());//紀錄command開始時間

                    yield return new WaitForSeconds(3f);
                    CommandController.instance.UpdateText(end_command[randomCommand]);//停止動作
                    CommandCSVHandler.instance.WriteRecord("end_time",VideoCSVHandler.instance.GetDateTime());//紀錄command結束時間
                    yield return new WaitForSeconds(2f);//顯示2秒

                    CommandController.instance.SetTextActive(false);//關閉

                    CommandCSVHandler.instance.WriteFile();//把紀錄寫進檔案
                }else{

                    CommandController.instance.UpdateText(command[randomCommand]);//設定顯示command

                    CommandCSVHandler.instance.WriteRecord("video_number",SkyboxVideoPlayerController.instance.Clip.number.ToString());//紀錄vidio num
                    CommandCSVHandler.instance.WriteRecord("command",command_code[randomCommand]);//紀錄command
                    CommandCSVHandler.instance.WriteRecord("excuted_time",VideoCSVHandler.instance.GetDateTime());//紀錄command開始時間

                    yield return new WaitForSeconds(5f);//顯示5秒
                    CommandController.instance.UpdateText(end_command[randomCommand]);//停止動作
                    yield return new WaitForSeconds(2f);//顯示2秒

                    CommandController.instance.SetTextActive(false);//關閉
                    CommandCSVHandler.instance.WriteRecord("end_time",VideoCSVHandler.instance.GetDateTime());//紀錄command結束時間

                    CommandCSVHandler.instance.WriteFile();//把紀錄寫進檔案 
                } 

                Debug.Log("前15秒顯示兩次(1/2)：" + command[randomCommand]);
                yield return new WaitForSeconds(3f);//等待3秒
                                CommandController.instance.SetTextActive(true);
                random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
                randomCommand = random % L;//取得亂數command

                if(command_code[randomCommand] == "NoBlink"){

                    CommandController.instance.UpdateText(blink_command[0]);
                    yield return new WaitForSeconds(1f);
                    CommandController.instance.UpdateText(blink_command[1]);
                    yield return new WaitForSeconds(1f);
                    CommandController.instance.UpdateText(blink_command[2]);

                    CommandCSVHandler.instance.WriteRecord("video_number",SkyboxVideoPlayerController.instance.Clip.number.ToString());//紀錄vidio num
                    CommandCSVHandler.instance.WriteRecord("command",command_code[randomCommand]);//紀錄command
                    CommandCSVHandler.instance.WriteRecord("excuted_time",VideoCSVHandler.instance.GetDateTime());//紀錄command開始時間

                    yield return new WaitForSeconds(3f);
                    CommandController.instance.UpdateText(end_command[randomCommand]);//停止動作
                    CommandCSVHandler.instance.WriteRecord("end_time",VideoCSVHandler.instance.GetDateTime());//紀錄command結束時間
                    yield return new WaitForSeconds(2f);//顯示2秒

                    CommandController.instance.SetTextActive(false);//關閉

                    CommandCSVHandler.instance.WriteFile();//把紀錄寫進檔案
                }else{

                    CommandController.instance.UpdateText(command[randomCommand]);//設定顯示command

                    CommandCSVHandler.instance.WriteRecord("video_number",SkyboxVideoPlayerController.instance.Clip.number.ToString());//紀錄vidio num
                    CommandCSVHandler.instance.WriteRecord("command",command_code[randomCommand]);//紀錄command
                    CommandCSVHandler.instance.WriteRecord("excuted_time",VideoCSVHandler.instance.GetDateTime());//紀錄command開始時間

                    yield return new WaitForSeconds(5f);//顯示5秒
                    CommandController.instance.UpdateText(end_command[randomCommand]);//停止動作
                    yield return new WaitForSeconds(2f);//顯示2秒

                    CommandController.instance.SetTextActive(false);//關閉
                    CommandCSVHandler.instance.WriteRecord("end_time",VideoCSVHandler.instance.GetDateTime());//紀錄command結束時間

                    CommandCSVHandler.instance.WriteFile();//把紀錄寫進檔案 
                }  
                Debug.Log("後15秒顯示兩次(2/2)：" + command[randomCommand]);
            }
        }
    }
}
