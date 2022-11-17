using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TRPO.Services;

namespace TRPO.Controllers
{
    public class AddingFlightController : Controller
    {
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                DateTime Date = DateTime.Now;
                DateTime TimeUp = DateTime.Now;
                DateTime TimeIn = DateTime.Now;
                string DeparturePoint = Request.Form["DeparturePoint"];
                string PlaceOfArrival = Request.Form["PlaceOfArrival"];
                string FirstPilot = Request.Form["FirstPilot"];
                string SecondPilot = Request.Form["SecondPilot"];
                string NumberPlane = Request.Form["NumberPlane"];

                string commandExpression = $"INSERT INTO AddingFlight (Date,TimeUp,TimeIn,DeparturePoint,PlaceOfArrival,FirstPilot,TwoPilot,NumberPlane) VALUES" +
                    $" (@Date,@TimeUp,@TimeIn,@DeparturePoint,@PlaceOfArrival,@FirstPilot,@TwoPilot,@NumberPlane)";
             
                SqlParameter DateParameter = new SqlParameter("@Date", Date);
                SqlParameter TimeUpParapmeter = new SqlParameter("@TimeUp", TimeUp);
                SqlParameter TimeInParameter = new SqlParameter("@TimeIn", TimeIn);
                SqlParameter DeparturePointParameter = new SqlParameter("@DeparturePoint", DeparturePoint);
                SqlParameter PlaceOfArrivalParameter = new SqlParameter("@PlaceOfArrival", PlaceOfArrival);
                SqlParameter FirstPilotParameter = new SqlParameter("@FirstPilot", FirstPilot);
                SqlParameter TwoPilotParameter = new SqlParameter("@SecondPilot", SecondPilot);
                SqlParameter NumberPlaneParameter = new SqlParameter("@NumberPlane", NumberPlane);
               
                

                DataBase dataBase = DataBase.getInstance();
                dataBase.openConnection();
                SqlCommand command = new SqlCommand(commandExpression, dataBase.getConnection());
                command.Parameters.Add(Date);
                command.Parameters.Add(TimeUp);
                command.Parameters.Add(TimeIn);
                command.Parameters.Add(DeparturePoint);
                command.Parameters.Add(PlaceOfArrival);
                command.Parameters.Add(FirstPilot);
                command.Parameters.Add(SecondPilot);
                command.Parameters.Add(NumberPlane);
                command.ExecuteNonQuery();
                dataBase.closeConnection();
            }
            return View("Index");
        }
    }
}
