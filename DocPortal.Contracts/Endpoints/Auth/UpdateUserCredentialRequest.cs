namespace DocPortal.Contracts.Endpoints.Auth;

public record UpdateUserCredentialRequest(int Id, string Login, string Password);
