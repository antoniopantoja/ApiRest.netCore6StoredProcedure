using API_DesafioNet.Interfaces.Repositorys;
using API_DesafioNet.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace API_DesafioNet.Repositorys
{
    public class EnderecoRepository : IEnderecoRepository
    {
        public DataSet EnderecoAsync(List<EnderecoModel> endereco, int index, int clienteId, string operacao)
        {
            BaseRepository DsConect = new BaseRepository();
            DsConect.cleanParameters();
            DsConect.addParameter(new SqlParameter("@Operacao", operacao));
            DsConect.addParameter(new SqlParameter("@ClienteId", clienteId));
            DsConect.addParameter(new SqlParameter("@LogradouroId", endereco[index].LogradouroId));
            DsConect.addParameter(new SqlParameter("@Cep", endereco[index].Cep));
            DsConect.addParameter(new SqlParameter("@Logradura", endereco[index].Logradura));
            DsConect.addParameter(new SqlParameter("@Bairro", endereco[index].Bairro));
            DsConect.addParameter(new SqlParameter("@Numero", endereco[index].Numero));
            DsConect.addParameter(new SqlParameter("@Complemento", endereco[index].Complemento));
            DsConect.addParameter(new SqlParameter("@Uf", endereco[index].Uf));
            DsConect.addParameter(new SqlParameter("@Cidade", endereco[index].Cidade));
            return DsConect.getDataSet("usp_p_clienteEndereco", CommandType.StoredProcedure);
        }
        public DataSet DeletarEnderecoAsync(int LogradouroId, string operacao)
        {
            BaseRepository DsConect = new BaseRepository();
            DsConect.cleanParameters();
            DsConect.addParameter(new SqlParameter("@Operacao", operacao));
            DsConect.addParameter(new SqlParameter("@LogradouroId", LogradouroId));
            return DsConect.getDataSet("usp_p_clienteEndereco", CommandType.StoredProcedure);
        }
        public DataSet ListarEnderecoAsync(int clienteId, string operacao)
        {
            BaseRepository DsConect = new BaseRepository();
            DsConect.cleanParameters();
            DsConect.addParameter(new SqlParameter("@Operacao", operacao));
            DsConect.addParameter(new SqlParameter("@ClienteId", clienteId));
            return DsConect.getDataSet("usp_v_clienteEndereco", CommandType.StoredProcedure);
        }

    }
}
