using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

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
            _commandExpression = "SELECT * FROM Flight ";
        }
        private bool addCondition()
        {
            if (_filters != ""&&_filters != " "&&_filters!=null)
            {
                _filters += "AND ";
            }
            else
            {
                _filters += "WHERE ";
                return true;
                
            }
            return false;
        }
        public void SetFinishDate(string finishDate)
        {
            if (finishDate != null && !Regex.IsMatch(finishDate, @"(\s)+", RegexOptions.IgnoreCase) && finishDate != "")
            {
                addCondition();
                _filters += $"Date <= @finishDate ";
                command.Parameters.Add("@finishDate", System.Data.SqlDbType.DateTime).Value = Convert.ToDateTime(finishDate);
            }

        }
        public void SetStartDate(string startDate)
        {
            if(startDate != null && !Regex.IsMatch(startDate, @"(\s)+", RegexOptions.IgnoreCase) && startDate != "")
            {
                addCondition();
                _filters += $"Date >= @startDate ";
                command.Parameters.Add("@startDate", System.Data.SqlDbType.DateTime).Value = Convert.ToDateTime(startDate);
            }
            
        }
        public void SetFinishPoint(string finishPoint)
        {
            if (finishPoint != null && !Regex.IsMatch(finishPoint, @"(\s)+", RegexOptions.IgnoreCase) && finishPoint != "")
            {
                addCondition();
                _filters += $"Finish_point = @finishPoint ";
                command.Parameters.Add("@finishPoint", System.Data.SqlDbType.NChar, 20).Value = finishPoint;
            }
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