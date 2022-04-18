# Cognito Dashboard

Please note this is work in progress

This is a general purpose AWS Cognito User Management dashboard.

![Screenshot](/images/screenshot-users.png)

**Tech Stack**
- C# with .net 6
- Blazor Server side framework
- Boostrap CSS

**Getting Started**
1. Create a AWS Cognito UserPool, Application Client & dashboard-administrators user group to be used by the dashboard.

    [Sample CloudFormation file](.cloudformation/demo-cognito-stack.yml)

    [Deployment & Hosted UI customization script sample](.cloudformation/demo-cognito-stack.sh)

2. Configure Hosted UI

    ![Screenshot](/images/hosted-ui-config.png)

    OAuth grant types should include "Authorization code grant" and "Implicit grant".

    OpenID Connect scopes should include "email, openid & profile".

    Under Hosted UI -> Allowed callback URL's, you will need to have the url which redirects to the cognito and the url to receive back control after the sign in process.

        In the above sample,
        https://localhost:5001/ is the home page from which the redirection to Hosted UI happens.
        https://localhost:5001/signin-oidc is the url Hosted UI redirects to after sign in.


3. Configure appsettings.json.  

    ```javascript
        {
            "Cognito": 
            {
                "UserPoolId": "ap-southeast-2_xxxx",
                "DashboardClientId": "xxxx",
                "DashboardClientSecret": "xxxxxxxx",
                "Region": "ap-southeast-2",
                "RedirectUri": "https://localhost:5001/signin-oidc",
                "PostLogoutRedirectUri": "https://localhost:5001"
            }
        }
    ```
    Please note the "RedirectUri" in appsettings needs to match exactly with one of the "Allowed callback URL's" in AWS Cognito, Hosted UI configuration. If they do not match you will get the dreaded redirect mismatch error.  A common gotcha is the missing/additional trailing slash in the the url.

4. Build & Run

5. Create a user using AWS Web Console and add to dashboard-administrators group. 

6. Login to the dashboard with user created in the previous step.
