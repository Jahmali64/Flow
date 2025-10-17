using Microsoft.AspNetCore.Mvc;
using Flow.Application.Models;
using Flow.Application.Services;

namespace Flow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class DemandRecordsController : Controller {
    private readonly IDemandRecordService _demandRecordService;
    private readonly ILogger<DemandRecordsController> _logger;
    
    public DemandRecordsController(IDemandRecordService demandRecordService, ILogger<DemandRecordsController> logger) {
        _demandRecordService = demandRecordService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<DemandRecordVm>>> GetAllDemandRecordsAsync() {
        string requestPath = HttpContext.Request.Path.Value ?? string.Empty;
        _logger.LogInformation("Request path: '{path}'", requestPath);

        try {
            List<DemandRecordVm> demandRecords = await _demandRecordService.GetAllDemandRecordsAsync();

            _logger.LogInformation("Request to '{path}' was successful", requestPath);
            return Ok(demandRecords);
        } catch (Exception ex) {
            _logger.LogError(ex, "Request to '{path}' has failed", requestPath);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}