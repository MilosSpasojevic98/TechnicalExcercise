using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("company")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }

        [HttpGet("{companyId}/users")]
        public IActionResult GetUsers(int? companyId)
        {
            try
            {
                if (companyId == null)
                {
                    _logger.LogDebug("Bad request at CompanyController.GetUsers because companyId is missing");
                    return BadRequest("companyId cannot be null");
                }
                var users = _companyService.GetSortedUsers((int)companyId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while geting users for company: " + companyId);
                return StatusCode(500, "Error occured while geting users for company: " + companyId);
            }
        }

        [HttpPost("{companyId}/user")]
        public IActionResult AddUser([FromBody] User user, int? companyId)
        {
            try
            {
                if (user == null || user.CompanyId != companyId || !ModelState.IsValid)
                {
                    _logger.LogDebug("Bad request at CompanyController.AddUser because user data is not valid or null");
                    return BadRequest("User data is not valid or null");
                }
                var result = _companyService.AddUser(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while adding user: " + user.Email + ", for company: " + companyId);
                return StatusCode(500, "Error occured while adding user: " + user.Email + ", for company: " + companyId);
            }
        }
    }
}