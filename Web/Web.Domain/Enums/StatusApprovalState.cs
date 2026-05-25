namespace Web.Domain.Enums;

public enum StatusApprovalState
{
    NotRequired = 0,
    MissingFile = 1,
    WaitingForApproval = 2,
    Approved = 3,
    Rejected = 4
}