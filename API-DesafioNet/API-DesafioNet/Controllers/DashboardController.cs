using API_DesafioNet.Interfaces.Services;
using API_DesafioNet.Services;
using API_DesafioNet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_DesafioNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Listar Dashboard.
        /// </summary>
        /// <response code="200">O Retono da API do Dashboard com sucesso.</response>
        /// <response code="400">O modelo do Dashboard enviado é inválido.</response>
        /// <response code="500">Ocorreu um erro.</response>
        #region Listar Dashboard
        [HttpPost]
        [Route("Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                DashboardVm Ds = await _dashboardService.ListarDashboardAsync();
                if (Ds == null)
                    return NotFound(new { message = "dados não encontrado." });

                return Ok(Ds);
            }
            catch (Exception e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message.Length > 0 ? new
                {
                    Code = 400,
                    Message = e.Message.ToString(),
                    Output = 0
                } : new RetornoPostVm());
            }
        }
        #endregion


    }
}
