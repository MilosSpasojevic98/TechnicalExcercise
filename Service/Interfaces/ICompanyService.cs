using Model.Models;

public interface ICompanyService
{
    List<User> GetSortedUsers(int companyId);

    User AddUser(User user);
}