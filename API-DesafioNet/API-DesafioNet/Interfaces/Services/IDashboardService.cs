using API_DesafioNet.Models;
using API_DesafioNet.ViewModels;

namespace API_DesafioNet.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<DashboardVm> ListarDashboardAsync();
    }
}
