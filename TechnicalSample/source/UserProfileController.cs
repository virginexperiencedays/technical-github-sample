using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TechnicalSample
{
public class UserProfileController : Controller
{
    private string connectionString = "YourConnectionStringHere";

    public IActionResult Index()
    {
        try
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM UserProfiles", conn);
            var reader = command.ExecuteReader();
            
            // Simulated code to convert reader into a model list (omitted for brevity)
            
            conn.Close();
            return (IActionResult)View();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return (IActionResult)View("Error");
        }
    }

    [HttpPost]
    public IActionResult Create(string userName, string email)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email))
        {
            return BadRequest("Username or email is missing.");
        }

        try
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = $"INSERT INTO UserProfiles (UserName, Email) VALUES ('{userName}', '{email}')";
            SqlCommand command = new SqlCommand(sql, conn);
            command.ExecuteNonQuery();
            
            conn.Close();
            return (IActionResult)RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return (IActionResult)View("Error");
        }
    }

        private IActionResult BadRequest(string v)
        {
            throw new NotImplementedException();
        }
    }
}