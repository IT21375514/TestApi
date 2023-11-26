using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using TestApi.Models;

namespace TestApi.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("GetActiveOrdersByCustomer")]
        [HttpGet]
        public async Task<IActionResult> GetActiveOrdersByCustomer(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                return BadRequest("Invalid request. UserId is required in the request body.");
            }

            try
            {
                List<Order> orders = new List<Order>();
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                string procedure = "GetActiveOrdersByCustomer";
                using (SqlCommand cmd = new SqlCommand(procedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        Order order = new Order
                        {
                            OrderId = Guid.Parse(row["OrderId"].ToString()),
                            ProductId = Guid.Parse(row["ProductId"].ToString()),
                            OrderStatus = Convert.ToInt32(row["OrderStatus"]),
                            OrderType = Convert.ToInt32(row["OrderType"]),
                            OrderBy = Guid.Parse(row["CustomerId"].ToString()),
                            OrderedOn = Convert.ToDateTime(row["OrderedOn"]),
                            ShippedOn = row["ShippedOn"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["ShippedOn"]) : null,
                            IsActive = Convert.ToBoolean(row["IsActive"])
                        };
                        orders.Add(order);
                    }

                    if (orders.Count > 0)
                    {
                        return Ok(orders);
                    }
                    else
                    {
                        return NotFound($"No active orders found for customer with ID {customerId}.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
