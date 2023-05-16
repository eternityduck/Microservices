using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersService.Api.Constants;
using UsersService.Api.Models;
using UsersService.Api.Results;
using UsersService.Business.Dtos;
using UsersService.Business.Interfaces.Services;

namespace UsersService.Api.Controllers;
[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public sealed class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = (await _userService.GetAllAsync())
            .Select(u => UserResult.Create(u));
        return Ok(result);
    }

    [HttpGet("{id}", Name = EndpointNames.GetUserById)]
    [ProducesResponseType(typeof(UserResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
    {
        var result = UserResult.Create(await _userService.GetByIdAsync(id));
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateUserModel model, 
        [FromServices] LinkGenerator linkGenerator)
    {
        var dto = _mapper.Map<UserDto>(model);
        dto = await _userService.AddAsync(dto);
        var result = UserResult.Create(dto);
        return Created(linkGenerator.GetUriByName(HttpContext, EndpointNames.GetUserById, new { dto.Id })!, result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromRoute]int id, [FromBody] UpdateUserModel model)
    {
        var dto = _mapper.Map<UserDto>(model);
        dto.Id = id;

        dto = await _userService.UpdateAsync(dto);

        var result = UserResult.Create(dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await _userService.DeleteByIdAsync(id);

        return NoContent();
    }
}
