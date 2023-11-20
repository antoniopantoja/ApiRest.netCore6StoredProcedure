using API_DesafioNet.Interfaces.Repositorys;
using API_DesafioNet.Models;
using System.Data.SqlClient;
using System.Data;

namespace API_DesafioNet.Repositorys
{
    public class ClienteRepository : IClienteRepository
    {
        public DataSet ClienteAsync(ClienteModel cliente, string operacao)
        {
            BaseRepository DsConect = new BaseRepository();
            DsConect.cleanParameters();
            DsConect.addParameter(new SqlParameter("@Operacao", operacao));
            DsConect.addParameter(new SqlParameter("@ClienteId", cliente.ClienteId));
            DsConect.addParameter(new SqlParameter("@Nome", cliente.Nome));
            DsConect.addParameter(new SqlParameter("@Email", cliente.Email));
            DsConect.addParameter(new SqlParameter("@Logotipo", cliente.Logotipo));
            return DsConect.getDataSet("usp_p_cliente", CommandType.StoredProcedure);
        }

    }
}
