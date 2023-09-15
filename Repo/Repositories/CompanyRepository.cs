using Model.Models;
using System.Data;
using System.Data.SqlClient;

public class CompanyRepository : ICompanyRepository
{
    private readonly ILogger<CompanyRepository> _logger;
    private readonly IConfiguration _configuration;

    public CompanyRepository(ILogger<CompanyRepository> logger, IConfiguration config)
    {
        _logger = logger;
        _configuration = config;
    }

    public List<User> GetAllCompanyUsers(int companyId)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("SqlConnectionString")))
            {
                using (SqlCommand cmd = new SqlCommand("GetCompanyUsers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@companyId", SqlDbType.Int).Value = companyId;

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<User> users = new List<User>();
                        var user = new User();

                        while (reader.Read())
                        {
                            user.Id = int.Parse(reader["Id"].ToString() ?? String.Empty);
                            user.FirstName = reader["FirstName"].ToString() ?? String.Empty;
                            user.LastName = reader["LastName"].ToString() ?? String.Empty;
                            user.CompanyId = int.Parse(reader["CompanyId"].ToString() ?? String.Empty);
                            user.Email = reader["Email"].ToString() ?? String.Empty;

                            users.Add(user);
                        }
                        return users;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured while geting users for company: " + companyId);
            throw;
        }
    }

    public User AddUser(User user)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("SqlConnectionString")))
            {
                using (SqlCommand cmd = new SqlCommand("AddUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@firstName", SqlDbType.VarChar).Value = user.FirstName;
                    cmd.Parameters.AddWithValue("@lastName", SqlDbType.VarChar).Value = user.LastName;
                    cmd.Parameters.AddWithValue("@companyId", SqlDbType.Int).Value = user.CompanyId;
                    cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = user.Email;

                    con.Open();
                    var result = cmd.ExecuteScalar();

                    return (User)result;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured while adding user: " + user.FirstName + ", for company: " + user.CompanyId);
            throw;
        }
    }
}