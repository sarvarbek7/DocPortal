using DocPortal.Domain.Entities;

namespace DocPortal.Application.Common.Authentication;

public record AccessToken(Guid Id, User User, string Token, DateTime ExpireAt);
