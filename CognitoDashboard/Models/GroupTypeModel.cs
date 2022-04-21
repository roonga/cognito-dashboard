using Amazon.CognitoIdentityProvider.Model;

namespace CognitoDashboard.Models;

public class GroupTypeModel
{
    public GroupType GroupType { get; }

    public bool Selected { get; set; }

    public GroupTypeModel(GroupType groupType)
    {
        GroupType = groupType;
    }
}
