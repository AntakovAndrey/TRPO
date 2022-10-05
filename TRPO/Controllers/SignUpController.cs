using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TRPO.Models;

namespace TRPO.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Check(Passanger passange)
        {
            if (ModelState.IsValid)
            {
                string Name = Request.Form["Name"];
                string Surname = Request.Form["Surname"];
                string Pasport_ser = Request.Form["PassportSeries"];
                string Pasport_num = Request.Form["PassportNumber"];
                string Telephone = Request.Form["Telephone"];
                string Nationality = Request.Form["Nationality"];
                string Password = Request.Form["Password"];
                DateTime Date_of_birth = DateTime.Now;

                string commandExpression = $"INSERT INTO Passanger (Name,Surname,Pasport_ser,Pasport_num,Nationality,Date_of_birth,Telephone,Password) VALUES" +
                    $" (@Name,@Surname,@Pasport_ser,@Pasport_num,@Nationality,@Date_of_birth,@Telephone,@Password)";
                SqlParameter NameParameter = new SqlParameter("@Name", System.Data.SqlDbType.NChar,20);
                NameParameter.Value = Name;
                SqlParameter SurnameameParameter = new SqlParameter("@Surname", Surname);
                SqlParameter PasportSerParapmeter = new SqlParameter("@Pasport_ser", Pasport_ser);
                SqlParameter PasportNumParameter = new SqlParameter("@Pasport_num", Pasport_num);
                SqlParameter TelephoneParameter = new SqlParameter("@Telephone", Telephone);
                SqlParameter NationalityParameter = new SqlParameter("@Nationality", Nationality);
                SqlParameter PasswordParameter = new SqlParameter("@Password", Password);
                SqlParameter DateOfBirthParameter = new SqlParameter("@Date_of_birth", System.Data.SqlDbType.DateTime);
                DateOfBirthParameter.Value = Date_of_birth;

                DataBase dataBase = DataBase.getInstance();
                dataBase.openConnection();
                SqlCommand command = new SqlCommand(commandExpression,dataBase.GetConnection());
                command.Parameters.Add(NameParameter);
                command.Parameters.Add(SurnameameParameter);
                command.Parameters.Add(PasportSerParapmeter);
                command.Parameters.Add(PasportNumParameter);
                command.Parameters.Add(TelephoneParameter);
                command.Parameters.Add(NationalityParameter);
                command.Parameters.Add(PasswordParameter);
                command.Parameters.Add(DateOfBirthParameter);
                command.ExecuteNonQuery();
                dataBase.closeConnection();
            }
            return View("Index");
        }
    }
}
