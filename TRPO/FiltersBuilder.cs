using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TRPO
{
    public class FiltersBuilder
    {
        private string _commandExpression;
        private string _filters;
        private readonly SqlConnection _connection;
        private SqlCommand command = new SqlCommand();
        public FiltersBuilder(SqlConnection connection)
        {
            _connection = connection;
            _commandExpression = $"SELECT * FROM Flight ";
        }
        private bool addCondition()
        {
            if (_filters == ""||_filters == " "||_filters==null)
            {
                _filters += "WHERE ";
                return true;
            }
            else
            {
                _filters += "AND ";
            }
            return false;
        }

        //public void SetPriceFrom(string priceFrom)
        //{
        //    addCondition();
        //    command.Parameters.Add("@priceFrom", System.Data.SqlDbType.NChar).Value = priceFrom;
        //    _filters += $"PRICE >= @priceFrom";    
        //}
        //public void SetPriceTo(string priceTo)
        //{
        //    addCondition();
        //    command.Parameters.Add("@priceTo", System.Data.SqlDbType.NChar).Value = priceTo;
        //    _filters += $"PRICE <= @priceFrom";


        //}
        //public void SetDateFrom(DateTime DateFrom)
        //{
        //    addCondition();
        //    command.Parameters.Add("@priceTo", System.Data.SqlDbType.DateTime).Value = DateFrom;
        //    _filters += $"PRICE <= @priceFrom";
        //}
        //public void SetDateTo(string DateTo)
        //{
        //    addCondition();
        //    command.Parameters.Add("@priceTo", System.Data.SqlDbType.DateTime).Value = DateFrom;
        //    _filters += $"PRICE <= @priceFrom";

        //}
        public void SetFinishPoint(string finishPoint)
        {
            addCondition();
            
            _filters += $"Finish_point = @finishPoint";
            command.Parameters.Add("@finishPoint", System.Data.SqlDbType.NChar, 20).Value = finishPoint;
        }


        

        public SqlCommand GetResault()
        {
            _commandExpression += _filters;
            command.CommandText = _commandExpression;
            command.Connection = _connection;
            return command;
        }
    }
}
