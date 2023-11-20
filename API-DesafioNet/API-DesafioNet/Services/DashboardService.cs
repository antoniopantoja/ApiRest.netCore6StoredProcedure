using API_DesafioNet.Interfaces.Repositorys;
using API_DesafioNet.Interfaces.Services;
using API_DesafioNet.Models;
using API_DesafioNet.Repositorys;
using API_DesafioNet.ViewModels;
using Newtonsoft.Json;

namespace API_DesafioNet.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardVm> ListarDashboardAsync()
        {
            BaseRepository Ds = new BaseRepository();
            RetornoPostVm retornoPostVm = new RetornoPostVm();

            var jsonString = JsonConvert.SerializeObject(_dashboardRepository.ListarDashboardAsync(OperacaoModel.List.GetDescription()));
            jsonString = jsonString.Substring(1, jsonString.Length - 3);
            jsonString = jsonString.Remove(0, 12);

            DashboardVm listarDashboard = JsonConvert.DeserializeObject<DashboardVm>(jsonString);

            return listarDashboard;
        }

    }
}
