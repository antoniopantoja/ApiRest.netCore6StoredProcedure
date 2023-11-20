using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;

namespace API_DesafioNet.Repositorys
{
    public class BaseRepository
    {
        #region conexão de banco de dados

        private SqlParameterCollection sqlParametros;
        private static string connectionString;

        public BaseRepository() => sqlParametros = new SqlCommand().Parameters;

        public SqlConnection getConnection()
        {
            connectionString = getConfiguration("ConnectionStrings", "Connection");
            return new SqlConnection(connectionString);
        }

        public void addParameter(SqlParameter sqlParameter) => sqlParametros.Add(sqlParameter);

        public void addParameter(string name, object value) => sqlParametros.Add(new SqlParameter(name, value));

        public void cleanParameters() => sqlParametros.Clear();

        private void setParameters(SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.Clear();
            foreach (SqlParameter param in sqlParametros)
                sqlCommand.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
        }

        public DataSet getDataSet(string strCommand, CommandType cmdType)
        {
            using (SqlConnection sqlConn = getConnection())
            {
                sqlConn.Open();
                SqlCommand sqlCommand = sqlConn.CreateCommand();
                sqlCommand.CommandText = strCommand;
                sqlCommand.CommandType = cmdType;
                sqlCommand.CommandTimeout = 7200;
                setParameters(sqlCommand);
                SqlDataAdapter dtaDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();


                dtaDataAdapter.Fill(ds, "Consulta");
                return ds;
            }
        }

        public string getConfiguration(string chave, string item)
        {
            string retornString;
            string jsonFilePath = "appsettings.json";
            string jsonString = File.ReadAllText(jsonFilePath);
            JObject jsonObject = JObject.Parse(jsonString);
            retornString = jsonObject[chave][item].ToString();

            return retornString;
        }

        #endregion
    }
}
