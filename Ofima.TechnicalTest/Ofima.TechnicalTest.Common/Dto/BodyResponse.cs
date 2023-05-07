namespace Ofima.TechnicalTest.Common.Dto
{
    public class BodyResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }

        public int Code { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }
    }
}
