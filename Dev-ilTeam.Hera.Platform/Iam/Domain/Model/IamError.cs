namespace Dev_ilTeam.Hera.Platform.Iam.Domain.Model;

public enum IamError
{
    None,
    UserNotFound,
    UsernameAlreadyTaken,
    InvalidCredentials,
    OperationCancelled,
    DatabaseError,
    InternalServerError,
    ExternalServiceError
}
