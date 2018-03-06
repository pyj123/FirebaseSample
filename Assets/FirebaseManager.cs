using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class FirebaseManager : MonoBehaviour
{
    //이메일
    [SerializeField]
    InputField emailInput;

    //PS
    [SerializeField]
    InputField psInput;


    //디버그
    [SerializeField]
    Text debugText;
    StringBuilder stringBuilder = new StringBuilder();


    //인증 관리
    Firebase.Auth.FirebaseAuth auth;

    private void Awake()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;        
    }    

    private void AddLog(string log)
    {
        if (stringBuilder == null || debugText == null) return;
        stringBuilder.Append(string.Format("{0}\n",log));
        debugText.text = stringBuilder.ToString();
    }

    //가입버튼 클릭시
    public void SignUp()
    {
        if (emailInput.text.Length == 0 || psInput.text.Length == 0) return;

        auth.CreateUserWithEmailAndPasswordAsync(emailInput.text, psInput.text).ContinueWith
            (
            task =>
            {
                if(task.IsCanceled==false && task.IsFaulted == false)
                {
                    AddLog("가입 성공");
                }
                else
                {
                    AddLog("가입 실패");
                }
            }

            );      
    }

    //로그인 버튼 눌렀을때
    public void SignIn()
    {
        if (emailInput.text.Length == 0 || psInput.text.Length == 0) return;

        auth.SignInWithEmailAndPasswordAsync(emailInput.text, psInput.text).ContinueWith
            (
            task =>
            {
                if (task.IsCompleted == true && task.IsCanceled == false && task.IsFaulted == false)
                {
                    Firebase.Auth.FirebaseUser newUser = task.Result;
                    AddLog("로그인 성공");
                }
                else
                {
                    AddLog("로그인 실패");
                }
            }
            );

    }



}
