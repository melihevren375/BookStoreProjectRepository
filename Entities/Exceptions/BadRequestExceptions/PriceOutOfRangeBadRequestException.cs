namespace Entities.Exceptions.BadRequestExceptions
{
    public sealed class PriceOutOfRangeBadRequestException : BadRequestException
    {
        public PriceOutOfRangeBadRequestException() : base($"Max price min pricetan büyük olmalıdır!")
        {
        }
    }
}
