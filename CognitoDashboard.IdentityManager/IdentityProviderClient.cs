using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CognitoDashboard.IdentityManager
{
    public class IdentityProviderClient : IIdentityProviderClient
    {
        private readonly ILogger<IdentityProviderClient> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AmazonCognitoIdentityProviderClient _amazonCognitoIdentityProviderClient;

        public ILogger<IdentityProviderClient> Logger => _logger;

        public IHttpContextAccessor HttpContextAccessor => _httpContextAccessor;

        public IdentityProviderClient(ILogger<IdentityProviderClient> logger, IHttpContextAccessor httpContextAccessor, AmazonCognitoIdentityProviderClient amazonCognitoIdentityProviderClient)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _amazonCognitoIdentityProviderClient = amazonCognitoIdentityProviderClient;
        }

        public IClientConfig Config => _amazonCognitoIdentityProviderClient.Config;

        public ICognitoIdentityProviderPaginatorFactory Paginators => _amazonCognitoIdentityProviderClient.Paginators;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<AddCustomAttributesResponse> AddCustomAttributesAsync(AddCustomAttributesRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AddCustomAttributesAsync(request, cancellationToken);
        }

        public async Task<AdminAddUserToGroupResponse> AdminAddUserToGroupAsync(AdminAddUserToGroupRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminAddUserToGroupAsync(request, cancellationToken);
        }

        public async Task<AdminConfirmSignUpResponse> AdminConfirmSignUpAsync(AdminConfirmSignUpRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminConfirmSignUpAsync(request, cancellationToken);
        }

        public async Task<AdminCreateUserResponse> AdminCreateUserAsync(AdminCreateUserRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminCreateUserAsync(request, cancellationToken);
        }

        public async Task<AdminDeleteUserResponse> AdminDeleteUserAsync(AdminDeleteUserRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminDeleteUserAsync(request, cancellationToken);
        }

        public async Task<AdminDeleteUserAttributesResponse> AdminDeleteUserAttributesAsync(AdminDeleteUserAttributesRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminDeleteUserAttributesAsync(request, cancellationToken);
        }

        public async Task<AdminDisableProviderForUserResponse> AdminDisableProviderForUserAsync(AdminDisableProviderForUserRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminDisableProviderForUserAsync(request, cancellationToken);
        }

        public async Task<AdminDisableUserResponse> AdminDisableUserAsync(AdminDisableUserRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminDisableUserAsync(request, cancellationToken);
        }

        public async Task<AdminEnableUserResponse> AdminEnableUserAsync(AdminEnableUserRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminEnableUserAsync(request, cancellationToken);
        }

        public async Task<AdminForgetDeviceResponse> AdminForgetDeviceAsync(AdminForgetDeviceRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminForgetDeviceAsync(request, cancellationToken);
        }

        public async Task<AdminGetDeviceResponse> AdminGetDeviceAsync(AdminGetDeviceRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminGetDeviceAsync(request, cancellationToken);
        }

        public async Task<AdminGetUserResponse> AdminGetUserAsync(AdminGetUserRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminGetUserAsync(request, cancellationToken);
        }

        public async Task<AdminInitiateAuthResponse> AdminInitiateAuthAsync(AdminInitiateAuthRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminInitiateAuthAsync(request, cancellationToken);
        }

        public async Task<AdminLinkProviderForUserResponse> AdminLinkProviderForUserAsync(AdminLinkProviderForUserRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminLinkProviderForUserAsync(request, cancellationToken);
        }

        public async Task<AdminListDevicesResponse> AdminListDevicesAsync(AdminListDevicesRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminListDevicesAsync(request, cancellationToken);
        }

        public async Task<AdminListGroupsForUserResponse> AdminListGroupsForUserAsync(AdminListGroupsForUserRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminListGroupsForUserAsync(request, cancellationToken);
        }

        public async Task<AdminListUserAuthEventsResponse> AdminListUserAuthEventsAsync(AdminListUserAuthEventsRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminListUserAuthEventsAsync(request, cancellationToken);
        }

        public async Task<AdminRemoveUserFromGroupResponse> AdminRemoveUserFromGroupAsync(AdminRemoveUserFromGroupRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminRemoveUserFromGroupAsync(request, cancellationToken);
        }

        public async Task<AdminResetUserPasswordResponse> AdminResetUserPasswordAsync(AdminResetUserPasswordRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminResetUserPasswordAsync(request, cancellationToken);
        }

        public async Task<AdminRespondToAuthChallengeResponse> AdminRespondToAuthChallengeAsync(AdminRespondToAuthChallengeRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminRespondToAuthChallengeAsync(request, cancellationToken);
        }

        public async Task<AdminSetUserMFAPreferenceResponse> AdminSetUserMFAPreferenceAsync(AdminSetUserMFAPreferenceRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminSetUserMFAPreferenceAsync(request, cancellationToken);
        }

        public async Task<AdminSetUserPasswordResponse> AdminSetUserPasswordAsync(AdminSetUserPasswordRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminSetUserPasswordAsync(request, cancellationToken);
        }

        public async Task<AdminSetUserSettingsResponse> AdminSetUserSettingsAsync(AdminSetUserSettingsRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminSetUserSettingsAsync(request, cancellationToken);
        }

        public async Task<AdminUpdateAuthEventFeedbackResponse> AdminUpdateAuthEventFeedbackAsync(AdminUpdateAuthEventFeedbackRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminUpdateAuthEventFeedbackAsync(request, cancellationToken);
        }

        public async Task<AdminUpdateDeviceStatusResponse> AdminUpdateDeviceStatusAsync(AdminUpdateDeviceStatusRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminUpdateDeviceStatusAsync(request, cancellationToken);
        }

        public async Task<AdminUpdateUserAttributesResponse> AdminUpdateUserAttributesAsync(AdminUpdateUserAttributesRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminUpdateUserAttributesAsync(request, cancellationToken);
        }

        public async Task<AdminUserGlobalSignOutResponse> AdminUserGlobalSignOutAsync(AdminUserGlobalSignOutRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AdminUserGlobalSignOutAsync(request, cancellationToken);
        }

        public async Task<AssociateSoftwareTokenResponse> AssociateSoftwareTokenAsync(AssociateSoftwareTokenRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.AssociateSoftwareTokenAsync(request, cancellationToken);
        }

        public async Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ChangePasswordAsync(request, cancellationToken);
        }

        public async Task<ConfirmDeviceResponse> ConfirmDeviceAsync(ConfirmDeviceRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ConfirmDeviceAsync(request, cancellationToken);
        }

        public async Task<ConfirmForgotPasswordResponse> ConfirmForgotPasswordAsync(ConfirmForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ConfirmForgotPasswordAsync(request, cancellationToken);
        }

        public async Task<ConfirmSignUpResponse> ConfirmSignUpAsync(ConfirmSignUpRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ConfirmSignUpAsync(request, cancellationToken);
        }

        public async Task<CreateGroupResponse> CreateGroupAsync(CreateGroupRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.CreateGroupAsync(request, cancellationToken);
        }

        public async Task<CreateIdentityProviderResponse> CreateIdentityProviderAsync(CreateIdentityProviderRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.CreateIdentityProviderAsync(request, cancellationToken);
        }

        public async Task<CreateResourceServerResponse> CreateResourceServerAsync(CreateResourceServerRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.CreateResourceServerAsync(request, cancellationToken);
        }

        public async Task<CreateUserImportJobResponse> CreateUserImportJobAsync(CreateUserImportJobRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.CreateUserImportJobAsync(request, cancellationToken);
        }

        public async Task<CreateUserPoolResponse> CreateUserPoolAsync(CreateUserPoolRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.CreateUserPoolAsync(request, cancellationToken);
        }

        public async Task<CreateUserPoolClientResponse> CreateUserPoolClientAsync(CreateUserPoolClientRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.CreateUserPoolClientAsync(request, cancellationToken);
        }

        public async Task<CreateUserPoolDomainResponse> CreateUserPoolDomainAsync(CreateUserPoolDomainRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.CreateUserPoolDomainAsync(request, cancellationToken);
        }

        public async Task<DeleteGroupResponse> DeleteGroupAsync(DeleteGroupRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DeleteGroupAsync(request, cancellationToken);
        }

        public async Task<DeleteIdentityProviderResponse> DeleteIdentityProviderAsync(DeleteIdentityProviderRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DeleteIdentityProviderAsync(request, cancellationToken);
        }

        public async Task<DeleteResourceServerResponse> DeleteResourceServerAsync(DeleteResourceServerRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DeleteResourceServerAsync(request, cancellationToken);
        }

        public async Task<DeleteUserResponse> DeleteUserAsync(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DeleteUserAsync(request, cancellationToken);
        }

        public async Task<DeleteUserAttributesResponse> DeleteUserAttributesAsync(DeleteUserAttributesRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DeleteUserAttributesAsync(request, cancellationToken);
        }

        public async Task<DeleteUserPoolResponse> DeleteUserPoolAsync(DeleteUserPoolRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DeleteUserPoolAsync(request, cancellationToken);
        }

        public async Task<DeleteUserPoolClientResponse> DeleteUserPoolClientAsync(DeleteUserPoolClientRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DeleteUserPoolClientAsync(request, cancellationToken);
        }

        public async Task<DeleteUserPoolDomainResponse> DeleteUserPoolDomainAsync(DeleteUserPoolDomainRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DeleteUserPoolDomainAsync(request, cancellationToken);
        }

        public async Task<DescribeIdentityProviderResponse> DescribeIdentityProviderAsync(DescribeIdentityProviderRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DescribeIdentityProviderAsync(request, cancellationToken);
        }

        public async Task<DescribeResourceServerResponse> DescribeResourceServerAsync(DescribeResourceServerRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DescribeResourceServerAsync(request, cancellationToken);
        }

        public async Task<DescribeRiskConfigurationResponse> DescribeRiskConfigurationAsync(DescribeRiskConfigurationRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DescribeRiskConfigurationAsync(request, cancellationToken);
        }

        public async Task<DescribeUserImportJobResponse> DescribeUserImportJobAsync(DescribeUserImportJobRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DescribeUserImportJobAsync(request, cancellationToken);
        }

        public async Task<DescribeUserPoolResponse> DescribeUserPoolAsync(DescribeUserPoolRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DescribeUserPoolAsync(request, cancellationToken);
        }

        public async Task<DescribeUserPoolClientResponse> DescribeUserPoolClientAsync(DescribeUserPoolClientRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DescribeUserPoolClientAsync(request, cancellationToken);
        }

        public async Task<DescribeUserPoolDomainResponse> DescribeUserPoolDomainAsync(DescribeUserPoolDomainRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.DescribeUserPoolDomainAsync(request, cancellationToken);
        }

        public async Task<ForgetDeviceResponse> ForgetDeviceAsync(ForgetDeviceRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ForgetDeviceAsync(request, cancellationToken);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ForgotPasswordAsync(request, cancellationToken);
        }

        public async Task<GetCSVHeaderResponse> GetCSVHeaderAsync(GetCSVHeaderRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.GetCSVHeaderAsync(request, cancellationToken);
        }

        public async Task<GetDeviceResponse> GetDeviceAsync(GetDeviceRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.GetDeviceAsync(request, cancellationToken);
        }

        public async Task<GetGroupResponse> GetGroupAsync(GetGroupRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.GetGroupAsync(request, cancellationToken);
        }

        public async Task<GetIdentityProviderByIdentifierResponse> GetIdentityProviderByIdentifierAsync(GetIdentityProviderByIdentifierRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.GetIdentityProviderByIdentifierAsync(request, cancellationToken);
        }

        public async Task<GetSigningCertificateResponse> GetSigningCertificateAsync(GetSigningCertificateRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.GetSigningCertificateAsync(request, cancellationToken);
        }

        public async Task<GetUICustomizationResponse> GetUICustomizationAsync(GetUICustomizationRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.GetUICustomizationAsync(request, cancellationToken);
        }

        public async Task<GetUserResponse> GetUserAsync(GetUserRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.GetUserAsync(request, cancellationToken);
        }

        public async Task<GetUserAttributeVerificationCodeResponse> GetUserAttributeVerificationCodeAsync(GetUserAttributeVerificationCodeRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.GetUserAttributeVerificationCodeAsync(request, cancellationToken);
        }

        public async Task<GetUserPoolMfaConfigResponse> GetUserPoolMfaConfigAsync(GetUserPoolMfaConfigRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.GetUserPoolMfaConfigAsync(request, cancellationToken);
        }

        public async Task<GlobalSignOutResponse> GlobalSignOutAsync(GlobalSignOutRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.GlobalSignOutAsync(request, cancellationToken);
        }

        public async Task<InitiateAuthResponse> InitiateAuthAsync(InitiateAuthRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.InitiateAuthAsync(request, cancellationToken);
        }

        public async Task<ListDevicesResponse> ListDevicesAsync(ListDevicesRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ListDevicesAsync(request, cancellationToken);
        }

        public async Task<ListGroupsResponse> ListGroupsAsync(ListGroupsRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ListGroupsAsync(request, cancellationToken);
        }

        public async Task<ListIdentityProvidersResponse> ListIdentityProvidersAsync(ListIdentityProvidersRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ListIdentityProvidersAsync(request, cancellationToken);
        }

        public async Task<ListResourceServersResponse> ListResourceServersAsync(ListResourceServersRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ListResourceServersAsync(request, cancellationToken);
        }

        public async Task<ListTagsForResourceResponse> ListTagsForResourceAsync(ListTagsForResourceRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ListTagsForResourceAsync(request, cancellationToken);
        }

        public async Task<ListUserImportJobsResponse> ListUserImportJobsAsync(ListUserImportJobsRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ListUserImportJobsAsync(request, cancellationToken);
        }

        public async Task<ListUserPoolClientsResponse> ListUserPoolClientsAsync(ListUserPoolClientsRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ListUserPoolClientsAsync(request, cancellationToken);
        }

        public async Task<ListUserPoolsResponse> ListUserPoolsAsync(ListUserPoolsRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ListUserPoolsAsync(request, cancellationToken);
        }

        public async Task<ListUsersResponse> ListUsersAsync(ListUsersRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ListUsersAsync(request, cancellationToken);
        }

        public async Task<ListUsersInGroupResponse> ListUsersInGroupAsync(ListUsersInGroupRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ListUsersInGroupAsync(request, cancellationToken);
        }

        public async Task<ResendConfirmationCodeResponse> ResendConfirmationCodeAsync(ResendConfirmationCodeRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.ResendConfirmationCodeAsync(request, cancellationToken);
        }

        public async Task<RespondToAuthChallengeResponse> RespondToAuthChallengeAsync(RespondToAuthChallengeRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.RespondToAuthChallengeAsync(request, cancellationToken);
        }

        public async Task<RevokeTokenResponse> RevokeTokenAsync(RevokeTokenRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.RevokeTokenAsync(request, cancellationToken);
        }

        public async Task<SetRiskConfigurationResponse> SetRiskConfigurationAsync(SetRiskConfigurationRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.SetRiskConfigurationAsync(request, cancellationToken);
        }

        public async Task<SetUICustomizationResponse> SetUICustomizationAsync(SetUICustomizationRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.SetUICustomizationAsync(request, cancellationToken);
        }

        public async Task<SetUserMFAPreferenceResponse> SetUserMFAPreferenceAsync(SetUserMFAPreferenceRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.SetUserMFAPreferenceAsync(request, cancellationToken);
        }

        public async Task<SetUserPoolMfaConfigResponse> SetUserPoolMfaConfigAsync(SetUserPoolMfaConfigRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.SetUserPoolMfaConfigAsync(request, cancellationToken);
        }

        public async Task<SetUserSettingsResponse> SetUserSettingsAsync(SetUserSettingsRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.SetUserSettingsAsync(request, cancellationToken);
        }

        public async Task<SignUpResponse> SignUpAsync(SignUpRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.SignUpAsync(request, cancellationToken);
        }

        public async Task<StartUserImportJobResponse> StartUserImportJobAsync(StartUserImportJobRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.StartUserImportJobAsync(request, cancellationToken);
        }

        public async Task<StopUserImportJobResponse> StopUserImportJobAsync(StopUserImportJobRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.StopUserImportJobAsync(request, cancellationToken);
        }

        public async Task<TagResourceResponse> TagResourceAsync(TagResourceRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.TagResourceAsync(request, cancellationToken);
        }

        public async Task<UntagResourceResponse> UntagResourceAsync(UntagResourceRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.UntagResourceAsync(request, cancellationToken);
        }

        public async Task<UpdateAuthEventFeedbackResponse> UpdateAuthEventFeedbackAsync(UpdateAuthEventFeedbackRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.UpdateAuthEventFeedbackAsync(request, cancellationToken);
        }

        public async Task<UpdateDeviceStatusResponse> UpdateDeviceStatusAsync(UpdateDeviceStatusRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.UpdateDeviceStatusAsync(request, cancellationToken);
        }

        public async Task<UpdateGroupResponse> UpdateGroupAsync(UpdateGroupRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.UpdateGroupAsync(request, cancellationToken);
        }

        public async Task<UpdateIdentityProviderResponse> UpdateIdentityProviderAsync(UpdateIdentityProviderRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.UpdateIdentityProviderAsync(request, cancellationToken);
        }

        public async Task<UpdateResourceServerResponse> UpdateResourceServerAsync(UpdateResourceServerRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.UpdateResourceServerAsync(request, cancellationToken);
        }

        public async Task<UpdateUserAttributesResponse> UpdateUserAttributesAsync(UpdateUserAttributesRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.UpdateUserAttributesAsync(request, cancellationToken);
        }

        public async Task<UpdateUserPoolResponse> UpdateUserPoolAsync(UpdateUserPoolRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.UpdateUserPoolAsync(request, cancellationToken);
        }

        public async Task<UpdateUserPoolClientResponse> UpdateUserPoolClientAsync(UpdateUserPoolClientRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.UpdateUserPoolClientAsync(request, cancellationToken);
        }

        public async Task<UpdateUserPoolDomainResponse> UpdateUserPoolDomainAsync(UpdateUserPoolDomainRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.UpdateUserPoolDomainAsync(request, cancellationToken);
        }

        public async Task<VerifySoftwareTokenResponse> VerifySoftwareTokenAsync(VerifySoftwareTokenRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.VerifySoftwareTokenAsync(request, cancellationToken);
        }

        public async Task<VerifyUserAttributeResponse> VerifyUserAttributeAsync(VerifyUserAttributeRequest request, CancellationToken cancellationToken)
        {
            return await _amazonCognitoIdentityProviderClient.VerifyUserAttributeAsync(request, cancellationToken);
        }
    }
}
