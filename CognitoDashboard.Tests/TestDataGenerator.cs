﻿//using System.Net;
//using Amazon.CognitoIdentityProvider;
//using Amazon.CognitoIdentityProvider.Model;
//using CognitoDashboard.IdentityManager;
//using CognitoDashboard.Tests.Fakes;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging.Abstractions;
//using Xunit;

//namespace CognitoDashboard.Tests
//{
//    public class TestDataGenerator
//    {
//        [Fact(Skip = "Test Data")]
//        public async Task Create_User_Basic_Test()
//        {
//            var logger = new NullLogger<IdentityProviderClient>();
//            var idpClient = new AmazonCognitoIdentityProviderClient();
//            var fakeHttpContextAccessor = new FakeHttpContextAccessor();
//            var idp = new IdentityProviderClient(logger, fakeHttpContextAccessor, idpClient);

//            var configuration = new ConfigurationBuilder()
//                .AddJsonFile("appsettings.json", false, true)
//                .Build();

//            var cognitoConfig = configuration.GetSection(CognitoConfig.Name).Get<CognitoConfig>();

//            for (var i = 0; i < 101; i++)
//            {
//                //setup
//                var email = $"{Faker.Name.First()}.{Faker.Name.First()}.test@roonga.com.au";
//                var request = new AdminCreateUserRequest
//                {
//                    UserPoolId = cognitoConfig.UserPoolId,
//                    Username = email,
//                    MessageAction = "SUPPRESS",
//                    UserAttributes = new List<AttributeType>
//                    {
//                        new() {Name = "email", Value = email},
//                        new() {Name = "email_verified", Value = "true"}
//                    }
//                };

//                //act
//                var response = await idp.AdminCreateUserAsync(request, CancellationToken.None);

//                //assert
//                Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
//            }
//        }

//        [Fact(Skip = "Test Data")]
//        public async Task Create_Group_Test()
//        {
//            var logger = new NullLogger<IdentityProviderClient>();
//            var idpClient = new AmazonCognitoIdentityProviderClient();
//            var fakeHttpContextAccessor = new FakeHttpContextAccessor();
//            var idp = new IdentityProviderClient(logger, fakeHttpContextAccessor, idpClient);

//            var configuration = new ConfigurationBuilder()
//                .AddJsonFile("appsettings.json", false, true)
//                .Build();

//            var cognitoConfig = configuration.GetSection(CognitoConfig.Name).Get<CognitoConfig>();

//            for (var i = 0; i < 31; i++)
//            {
//                //setup
//                var groupName = Faker.Country.TwoLetterCode();

//                var request = new CreateGroupRequest
//                {
//                    UserPoolId = cognitoConfig.UserPoolId,
//                    GroupName = groupName,
//                    Description = $"{groupName} Group",
//                    RoleArn = null,
//                    Precedence = Faker.RandomNumber.Next(100)
//                };

//                try
//                {
//                    //act
//                    var response = await idp.CreateGroupAsync(request, CancellationToken.None);

//                    //assert
//                    Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
//                }
//                catch (GroupExistsException)
//                {
//                    continue;
//                }
//            }
//        }
//    }
//}
