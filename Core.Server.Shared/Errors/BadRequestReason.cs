namespace Core.Server.Shared.Errors
{
    public enum BadRequestReason
    {
        SameExists = 1,
        InvalidUserOrPassword,
        InvalidToken,
        InvalidGuid,
        PropertyNotFound,
        PropertyNotCurectType,
        Unchangeable
    }
}