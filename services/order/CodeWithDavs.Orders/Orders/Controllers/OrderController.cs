using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Models.Dto;
using Orders.Repository.IRepository;

namespace Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepos;

        public OrderController(IOrderRepository orderRepo, IMapper mapper) 
        {
            _mapper = mapper;
            _response = new ResponseDto();
            _orderRepos = orderRepo;
        }

        [Authorize]
        [HttpGet("id:string")]
        public async Task<ActionResult<ResponseDto>>? GetOrderById(string id)
        {
            try
            {
                var order = await _orderRepos.GetOrderAsync(id);
                if(order == null)
                {
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                _response.Result = _mapper.Map<OrderDto>(order);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Authorize]
        [HttpGet("userId:string")]
        public ActionResult<ResponseDto>? GetOrderByUserId(int userId)
        {
            //try
            //{
            //    OrderHeader orderHeader = _db.OrderHeaders.Include(u => u.OrderDetails).First(u => u.OrderHeaderId == id);
            //    _response.Result = _mapper.Map<OrderHeaderDto>(orderHeader);
            //}
            //catch (Exception ex)
            //{
            //    _response.IsSuccess = false;
            //    _response.Message = ex.Message;
            //}
            return _response;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ResponseDto>>? GetOrders()
        {
            try
            {
                var orders = await _orderRepos.GetOrdersAsync();
                _response.Result = _mapper.Map<OrderDto>(orders);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ResponseDto> CreateOrder([FromBody] CartDto cartDto)
        {
            try
            {
                //OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto.CartHeader);
                //orderHeaderDto.OrderTime = DateTime.Now;
                //orderHeaderDto.Status = SD.Status_Pending;
                //orderHeaderDto.OrderDetails = _mapper.Map<IEnumerable<OrderDetailsDto>>(cartDto.CartDetails);
                //orderHeaderDto.OrderTotal = Math.Round(orderHeaderDto.OrderTotal, 2);
                //OrderHeader orderCreated = _db.OrderHeaders.Add(_mapper.Map<OrderHeader>(orderHeaderDto)).Entity;
                //await _db.SaveChangesAsync();

                //orderHeaderDto.OrderHeaderId = orderCreated.OrderHeaderId;
                //_response.Result = orderHeaderDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}