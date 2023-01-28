using Exam.DAL;

namespace Exam.Services
{
    public class LayoutService
    {

        AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public Dictionary<string,string> GetSetting()
        {
            return _context.Settings.ToDictionary(s=>s.Key,s=>s.Value);
        }
    }
}
