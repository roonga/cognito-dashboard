using Amazon.CognitoIdentityProvider.Model;

namespace CognitoDashboard.Models;

public class UserTypeModel
{
    public UserType UserType { get; }

    public bool Selected { get; set; }

    public UserTypeModel(UserType userType)
    {
        UserType = userType;
    }
}
