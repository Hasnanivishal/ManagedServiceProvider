using Microsoft.AspNetCore.Mvc;
using MSP.Order.Model;
using MSP.Order.Repository;

namespace MSP.Order.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IMongoDbContext<OrderEntity> mongoDbContext) : ControllerBase
{
    private readonly IMongoDbContext<OrderEntity> mongoDbContext = mongoDbContext;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> Get()
    {
        var result = (await mongoDbContext.GetAllAsync()).Select(item => item.AsOrderDto());

        return Ok(result);
    }

    // GET api/<OrderController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById(Guid id)
    {
        var item = await mongoDbContext.GetAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return item.AsOrderDto();
    }

    // POST api/<OrderController>
    [HttpPost]
    public async Task<ActionResult<OrderDto>> Post([FromBody] AddOrderDto addOrderDto)
    {
        var orderEntity = new OrderEntity
        {
            Id = Guid.NewGuid(),
            Name = addOrderDto.Name,
            Amount = addOrderDto.Amount
        };

        await mongoDbContext.CreateAsync(orderEntity);

        // Once order is created send message to rabbit mq bus using mass transit

        return CreatedAtAction(nameof(GetById), new { id = orderEntity.Id }, orderEntity.AsOrderDto());
    }
}
