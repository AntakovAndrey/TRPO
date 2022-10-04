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

        //PriceFrom
        //    PriceTo
        public void SetDateFrom(DateTime date)
            DateTo


        public void SetFinishPoint(string finishPoint)
        {
            addCondition();

            _filters += $"Finish_point = @finishPoint";
            command.Parameters.Add("@finishPoint", System.Data.SqlDbType.NChar, 20).Value = finishPoint;
        }

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
