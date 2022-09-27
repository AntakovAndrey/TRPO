 using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using TRPO.Models;
using TRPO.VievModels;

namespace TRPO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DataBase _dataBase = new DataBase();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ViewResult Index()
        {

            var allFlights = new HomeViewModel
            {
                 
            }

            return View(allFlights);
        }

        public IActionResult Index()
        {
            Console.WriteLine(_dataBase.GetConnection());
            
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT * FROM Passanger WHERE Passanger_id = @uI ", _dataBase.GetConnection());
            command.Parameters.Add("@uI", SqlDbType.Int).Value = 2;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                DataRow[] foundrow = table.Select();
                for(int i =0;i<9;i++)
                {
                    Console.WriteLine(foundrow[0][i]);
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}