using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
    public GameObject loginPanel;
    private string userEmail;
    private string secret;
    public void Start()
    {
        
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "64A28";
        }
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        if (PlayerPrefs.HasKey("userEmail") && PlayerPrefs.HasKey("userSecret"))
        {
            userEmail = PlayerPrefs.GetString("userEmail");
            secret = PlayerPrefs.GetString("userSecret");
            var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = secret };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }
        
    }

    
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call! " + "Login Success");
        loginPanel.SetActive(false);
    }
    public void RegisterUserSuccess(RegisterPlayFabUserResult response)
    {
        Debug.Log("Congratulations, you made your first successful API call! " + "Register Success");
        PlayerPrefs.SetString("userEmail", userEmail);
        PlayerPrefs.SetString("userSecret", secret);
        loginPanel.SetActive(false);
    }
    public void RegisterUserFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    private void OnLoginFailure(PlayFabError error)
    {
        var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = secret };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, RegisterUserSuccess, RegisterUserFailure);
    }
    public void SetEmail(string emailid)
    {
        userEmail = emailid;
    }
    public void SetPassword(string pass)
    {
        secret = pass;
    }
    public void OnClickLogin()
    {
        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
}