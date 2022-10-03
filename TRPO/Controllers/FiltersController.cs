using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using TRPO.Models;

namespace TRPO.Controllers
{
    public class FiltersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Check()
        {
            string finishPoint = Request.Form["FinishPoint"];
            DataBase dataBase = new DataBase();
            var connection = dataBase.GetConnection();
            FiltersBuilder filtersBuilder = new FiltersBuilder(connection);
            filtersBuilder.SetFinishPoint(finishPoint);
            var command = filtersBuilder.GetResault();
            dataBase.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetInt32(0));
                    Console.WriteLine(DateOnly.FromDateTime(reader.GetDateTime(1)));
                    Console.WriteLine(TimeOnly.FromDateTime(reader.GetDateTime(2))); 
                    //TimeOnly finishTime = TimeOnly.FromDateTime(reader.GetDateTime(3));
                    //string startPoint = reader.GetString(4);
                    //string finishPoint = reader.GetString(5);
                    //int planeId = reader.GetInt32(6);
                    //int crewId = reader.GetInt32(7);
                    //tmpFlights.Add(new Flight(id, date, startTime, finishTime, startPoint, finishPoint, planeId, crewId));
                }

            }
            dataBase.closeConnection();
            //DataTable table = new DataTable();
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //adapter.SelectCommand = command;
            ////adapter.Fill(table);
            //DataRow[] userInfo;
            //userInfo = table.Select();
            //if (userInfo.Length > 0)
            //{
            //    Console.WriteLine("Вы вошли");
            //}
            //else
            //{
            //    Console.WriteLine("Неправильный логин и пароль");
            //}
            //Console.WriteLine(priceFrom);

            return RedirectToAction("Index");
        }

    }
}
