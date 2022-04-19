#!/bin/bash -x
aws cloudformation deploy \
    --capabilities CAPABILITY_IAM CAPABILITY_NAMED_IAM \
    --stack-name demo-cognito-dashboard \
    --template-file demo-cognito-stack.yml \

# optional: create access key for the service user
aws iam create-access-key --user-name cognito-dashboard-service-user

# optional: enable termination protection
aws cloudformation update-termination-protection --stack-name demo-cognito-dashboard --enable-termination-protection

# optional: get user-pool-id and customize Hosted UI
USERPOOLID=$(aws cloudformation --region ap-southeast-2 describe-stacks --stack-name demo-cognito-dashboard --query "Stacks[0].Outputs[?OutputKey=='UserPoolId'].OutputValue" --output text)

aws cognito-idp set-ui-customization --user-pool-id $USERPOOLID \
--css ".logo-customizable {max-width: 80%;max-height: 100%;}.banner-customizable {padding: 25px 0px 25px 0px;background-color: #3459e6;}.label-customizable {font-weight: 400;}.textDescription-customizable {padding-top: 10px;padding-bottom: 10px;display: block;font-size: 16px;}.idpDescription-customizable {padding-top: 10px;padding-bottom: 10px;display: block;font-size: 16px;}.legalText-customizable {color: #747474;font-size: 11px;}.submitButton-customizable {font-size: 14px;font-weight: bold;margin: 20px 0px 10px 0px;height: 40px;width: 100%;color: #fff;background-color: #3459e6;}.submitButton-customizable:hover {color: #fff;background-color: #3459e6;}.errorMessage-customizable {padding: 5px;font-size: 14px;width: 100%;background: #F5F5F5;border: 2px solid #D64958;color: #D64958;}.inputField-customizable {width: 100%;height: 34px;color: #555;background-color: #fff;border: 1px solid #ccc;}.inputField-customizable:focus {border-color: #66afe9;outline: 0;}.idpButton-customizable {height: 40px;width: 100%;text-align: center;margin-bottom: 15px;color: #fff;background-color: #5bc0de;border-color: #46b8da;}.idpButton-customizable:hover {color: #fff;background-color: #31b0d5;}.socialButton-customizable {height: 40px;text-align: left;width: 100%;margin-bottom: 15px;}.redirect-customizable {text-align: center;}.passwordCheck-notValid-customizable {color: #DF3312;}.passwordCheck-valid-customizable {color: #19BF00;}.background-customizable {background-color: #fff;}" \
--image-file fileb://logo.png
