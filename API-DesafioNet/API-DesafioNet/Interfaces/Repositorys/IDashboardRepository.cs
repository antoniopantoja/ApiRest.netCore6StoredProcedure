using System.Data;

namespace API_DesafioNet.Interfaces.Repositorys
{
    public interface IDashboardRepository
    {
        DataSet ListarDashboardAsync(string operacao);
    }
}
