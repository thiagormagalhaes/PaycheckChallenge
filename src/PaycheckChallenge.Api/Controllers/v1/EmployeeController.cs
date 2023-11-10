using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaycheckChallenge.Api.Requests;
using PaycheckChallenge.Api.Responses;
using PaycheckChallenge.Application.Commands.CreateEmployee;
using PaycheckChallenge.Application.Queries.GetEmployeeById;
using PaycheckChallenge.Application.Queries.GetPaycheck;

namespace PaycheckChallenge.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public EmployeeController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeRequest request)
    {
        var createCommand = _mapper.Map<CreateEmployeeCommand>(request);

        var employee = await _mediator.Send(createCommand);

        if (employee is null)
        {
            return Ok();
        }

        return Created($"api/v1/employee/{employee.Id}", null);
    }

    [HttpGet("{employeeId:long}")]
    public async Task<IActionResult> GetById(long employeeId)
    {
        var query = new GetEmployeeByIdQuery(employeeId);

        var employee = await _mediator.Send(query);

        if (employee is null)
        {
            return NotFound();
        }

        var employeeResponse = _mapper.Map<EmployeeResponse>(employee);

        return Ok(employeeResponse);
    }

    [HttpGet("{employeeId:long}/paycheck")]
    public async Task<IActionResult> GetPaycheck(long employeeId)
    {
        var query = new GetPaycheckQuery(employeeId);

        var paycheck = await _mediator.Send(query);

        var paycheckResponse = _mapper.Map<PaycheckResponse>(paycheck);

        return Ok(paycheckResponse);
    }
}
