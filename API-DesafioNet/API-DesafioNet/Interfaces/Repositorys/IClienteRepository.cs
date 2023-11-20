using API_DesafioNet.Models;
using System.Data;

namespace API_DesafioNet.Interfaces.Repositorys
{
    public interface IClienteRepository
    {
        DataSet ClienteAsync(ClienteModel cliente, string operacao);
    }
}
