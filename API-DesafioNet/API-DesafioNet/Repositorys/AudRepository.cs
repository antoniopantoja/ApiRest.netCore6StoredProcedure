using System.Data;
using System.Data.SqlClient;

namespace API_DesafioNet.Repositorys
{
    public class AudRepository
    {
        public DataSet Log(string log_txtransacao, string log_txmensagem, string log_Json)
        {
            BaseRepository DsConect = new BaseRepository();
            DsConect.cleanParameters();
            DsConect.addParameter(new SqlParameter("@log_txtransacao", log_txtransacao));
            DsConect.addParameter(new SqlParameter("@log_txmensagem", log_txmensagem));
            DsConect.addParameter(new SqlParameter("@log_Json", log_Json));
            return DsConect.getDataSet("usp_p_log_requisicao", CommandType.StoredProcedure);
        }
    }
}
