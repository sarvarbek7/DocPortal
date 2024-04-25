namespace DocPortal.Contracts.Endpoints.Auth;

public record UpdateUserCredentialRequest(int UserId, string Login, string Password);
