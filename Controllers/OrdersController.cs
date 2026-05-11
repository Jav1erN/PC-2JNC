using Microsoft.AspNetCore.Mvc;
using PC_2JNC.Services;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _service;

    public OrdersController(OrderService service)
    {
        _service = service;
    }

    [HttpGet("ej1")]
    public async Task<IActionResult> Ej1()
    {
        return Ok(await _service.Ej1());
    }
    
    [HttpGet("ej2")] public async Task<IActionResult> Ej2() => Ok(await _service.Ej2());
    [HttpGet("ej3")] public async Task<IActionResult> Ej3() => Ok(await _service.Ej3());
    [HttpGet("ej4")] public async Task<IActionResult> Ej4() => Ok(await _service.Ej4());
    [HttpGet("ej5")] public async Task<IActionResult> Ej5() => Ok(await _service.Ej5());
    [HttpGet("ej6")] public async Task<IActionResult> Ej6() => Ok(await _service.Ej6());
    [HttpGet("ej7")] public async Task<IActionResult> Ej7() => Ok(await _service.Ej7());
    [HttpGet("ej8")] public async Task<IActionResult> Ej8() => Ok(await _service.Ej8());
    [HttpGet("ej9")] public async Task<IActionResult> Ej9() => Ok(await _service.Ej9());
    [HttpGet("ej10")] public async Task<IActionResult> Ej10() => Ok(await _service.Ej10());
    [HttpGet("ej11")] public async Task<IActionResult> Ej11() => Ok(await _service.Ej11());
    [HttpGet("ej12")] public async Task<IActionResult> Ej12() => Ok(await _service.Ej12());
}