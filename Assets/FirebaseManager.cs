using UnityEngine;
using UnityEngine.UI;

public class FirebaseManager : MonoBehaviour
{
    // 이메일 InputField
    [SerializeField]
    InputField emailInput;
    // 비밀번호 InputField
    [SerializeField]
    InputField passInput;
    // 결과를 알려줄 텍스트
    [SerializeField]
    Text resultText;

    // 인증을 관리할 객체
    Firebase.Auth.FirebaseAuth auth;

    // Use this for initialization
    void Awake ()
    {
        // 인증을 관리할 객체를 초기화 한다.
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    // 회원가입 버튼을 눌렀을 때 작동할 함수
    public void SignUp()
    {
        // 회원가입 버튼은 인풋 필드가 비어있지 않을 때 작동한다.
        if(emailInput.text.Length != 0 && passInput.text.Length != 0)
        {
            auth.CreateUserWithEmailAndPasswordAsync(emailInput.text, passInput.text).ContinueWith(
                task =>
                {
                    if(!task.IsCanceled && !task.IsFaulted)
                    {
                        resultText.text = "회원가입 성공";
                    }
                    else
                    {
                        resultText.text = "회원가입 실패";
                    }
                });
        }
    }

    // 로그인 버튼을 눌렀을 때 작동할 함수
    public void SignIn()
    {
        // 로그인 버튼은 인풋 필드가 비어있지 않을 때 작동한다.
        if (emailInput.text.Length != 0 && passInput.text.Length != 0)
        {
            auth.SignInWithEmailAndPasswordAsync(emailInput.text, passInput.text).ContinueWith(
                task =>
                {
                    if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
                    {
                        Firebase.Auth.FirebaseUser newUser = task.Result;
                        resultText.text = "로그인 성공";
                    }
                    else
                    {
                        resultText.text = "로그인 실패";
                    }
                });
        }
    }
}
