using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TestApi.Models;
using static NuGet.Packaging.PackagingConstants;

namespace TestApi.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [Route("CreateCustomer")]
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer obj)
        {
            if (obj == null)
            {
                return BadRequest("Invalid request. Customer data is required in the request body.");
            }

            if (string.IsNullOrWhiteSpace(obj.Username))
            {
                return BadRequest("Username cannot be empty or whitespace.");
            }

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                string query = "INSERT INTO Customer VALUES (NEWID(), @UserName, @Email, @FirstName, @LastName, GETDATE(), @IsActive)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserName", obj.Username);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@FirstName", (object)obj.FirstName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LastName", (object)obj.LastName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);

                    con.Open();
                    string result = (string)cmd.ExecuteScalar();
                }

                return new ObjectResult($"Customer added successfully") { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal Server Error");
            }
            finally
            {
                // Ensure that the connection is closed even if an exception occurs
                con?.Close();
            }
        }

        [Route("GetAllCustomers")]
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            string query = "SELECT * FROM Customer";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Customer customer = new Customer
                    {
                        UserId = Guid.Parse(row["UserId"].ToString()),
                        Username = row["Username"].ToString(),
                        Email = row["Email"].ToString(),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        CreatedOn = Convert.ToDateTime(row["CreatedOn"]),
                        IsActive = Convert.ToBoolean(row["IsActive"])
                    };

                    customers.Add(customer);
                }

                return Ok(customers);
            }
            else
            {
                return NotFound("No customers found.");
            }

        }

        [Route("UpdateCustomer")]
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(Customer obj)
        {
            if (obj == null || obj.UserId == Guid.Empty)
            {
                return BadRequest("Invalid request. User ID is required in the request body.");
            }

            if (string.IsNullOrWhiteSpace(obj.Username))
            {
                return BadRequest("Username cannot be empty or whitespace.");
            }

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                string query = "Update Customer set Username = @Username, Email = @Email, FirstName = @FirstName, LastName = @LastName, IsActive = @IsActive where UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", obj.UserId);
                    cmd.Parameters.AddWithValue("@Username", obj.Username);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", obj.LastName);
                    cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);

                    con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok($"Customer updated successfully");
                    }
                    else
                    {
                        return NotFound($"User with ID {obj.UserId} not found.");
                    }
                }

            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal Server Error");
            }
            finally
            {
                // Ensure that the connection is closed even if an exception occurs
                con?.Close();
            }
        }

        [Route("DeleteCustomer")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest("Invalid request. UserId is required in the request body.");
            }

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                string query = "DELETE FROM Customer where UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
             
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok($"Customer deleted successfully" );
                    }
                    else
                    {
                        return NotFound($"Customer not found. ");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal Server Error");
            }
            finally
            {
                // Ensure that the connection is closed even if an exception occurs
                con?.Close();
            }
        }

    }
}