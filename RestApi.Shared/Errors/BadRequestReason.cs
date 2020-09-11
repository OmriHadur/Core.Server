namespace RestApi.Shared.Errors
{
    public enum BadRequestReason
    {
        SameExists = 1,
        InvalidUserOrPassword,
        InvalidToken,
        InvalidGuid
    }
}