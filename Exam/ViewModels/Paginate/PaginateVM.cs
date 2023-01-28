namespace Exam.ViewModels.Paginate
{
    public class PaginateVM<T>
    {
        public int MaxPageCount { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
