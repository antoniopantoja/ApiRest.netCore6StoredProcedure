using API_DesafioNet.Interfaces.Repositorys;
using System.Data;
using System.Data.SqlClient;

namespace API_DesafioNet.Repositorys
{
    public class DashboardRepository : IDashboardRepository
    {
        public DataSet ListarDashboardAsync(string operacao)
        {
            BaseRepository DsConect = new BaseRepository();
            DsConect.cleanParameters();
            DsConect.addParameter(new SqlParameter("@Operacao", operacao));
            return DsConect.getDataSet("usp_v_dashboard", CommandType.StoredProcedure);
        }
    }
}
