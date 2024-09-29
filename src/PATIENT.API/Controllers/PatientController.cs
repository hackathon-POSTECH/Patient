using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PATIENT.APPLICATION.Patient.GetAllPatient;
using PATIENT.APPLICATION.Patient.GetById;

namespace PATIENT.API.Controllers;

[ApiController]
[Route("[Controller]")]
[Authorize]
public class PatientController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAllPatientQuery();
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("getbyid/{patientId}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid patientId)
    {
        var query = new GetByIdQuery(patientId);
        var result = await _mediator.Send(query);
        if (result == null) return NotFound();
        return Ok(result);
    }
}
