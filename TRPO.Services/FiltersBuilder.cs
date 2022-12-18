using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace TRPO.Services
{
    public class FiltersBuilder
    {
        private string _commandExpression;
        private string _filters;

        readonly SqlConnection _connection;

        private SqlCommand _command;


        public FiltersBuilder(Models.FlightsFiltersViewModel filtersModel, SqlConnection connection)
        {
            _connection = connection;
            _commandExpression = "SELECT * FROM Flight ";
            _filters = "";
            _command = new SqlCommand();
        }

        public SqlCommand GetResault()
        {
            _commandExpression += _filters;
            _command.CommandText = _commandExpression;
            _command.Connection = _connection;
            return _command;
        }

        public void SetFinishDate(string finishDate)
        {
            if (finishDate != null && !Regex.IsMatch(finishDate, @"(\s)+", RegexOptions.IgnoreCase) && finishDate != "")
            {
                addCondition();
                _filters += $"Date <= @finishDate ";
                _command.Parameters.Add("@finishDate", System.Data.SqlDbType.DateTime).Value = Convert.ToDateTime(finishDate);
            }

        }
        public void SetStartDate(string startDate)
        {
            if (startDate != null && !Regex.IsMatch(startDate, @"(\s)+", RegexOptions.IgnoreCase) && startDate != "")
            {
                addCondition();
                _filters += $"Date >= @startDate ";
                _command.Parameters.Add("@startDate", System.Data.SqlDbType.DateTime).Value = Convert.ToDateTime(startDate);
            }

        }
        public void SetFinishPoint(string finishPoint)
        {
            if (finishPoint != null && !Regex.IsMatch(finishPoint, @"(\s)+", RegexOptions.IgnoreCase) && finishPoint != "")
            {
                addCondition();
                _filters += $"Finish_point = @finishPoint ";
                _command.Parameters.Add("@finishPoint", System.Data.SqlDbType.NChar, 20).Value = finishPoint;
            }
        }
        public void SetStartPoint(string finishPoint)
        {
            
            if (finishPoint != null && !Regex.IsMatch(finishPoint, @"(\s)+", RegexOptions.IgnoreCase) && finishPoint != "")
            {
                addCondition();
                _filters += $"Finish_point = @finishPoint ";
                _command.Parameters.Add("@finishPoint", System.Data.SqlDbType.NChar, 20).Value = finishPoint;
            }
        }

        private void addCondition()
        {
            if (_filters != "" && _filters != " " && _filters != null)
            {
                _filters += "AND ";
            }
            else
            {
                _filters += "WHERE ";
            }
        }
    }
}