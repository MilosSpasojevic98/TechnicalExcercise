using Model.Models;

namespace Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ILogger<CompanyService> _logger;
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ILogger<CompanyService> logger, ICompanyRepository companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }

        public List<User> GetSortedUsers(int companyId)
        {
            var users = _companyRepository.GetAllCompanyUsers(companyId).OrderBy(u => u.FirstName).ToList();
            return users;
        }

        public User AddUser(User user)
        {
            var newUser = _companyRepository.AddUser(user);
            return newUser;
        }
    }
}