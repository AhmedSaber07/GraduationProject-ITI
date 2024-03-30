namespace E_Commerce.MVC.DTOs.listResultDto
{
    public class resultDto<TEntity>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TEntity Entity { get; set; }
    }
}
