using Model.Models;

public interface ICompanyRepository
{
    List<User> GetAllCompanyUsers(int companyId);

    User AddUser(User user);
}