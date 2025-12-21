using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/Coupon")]
    [ApiController]
    public class CouponAPIController(AppDbContext _dbContext, IMapper _mapper) : ControllerBase
    {
        private ResponseDto _response = new ResponseDto();

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _dbContext.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("{id}")]
        public ResponseDto Get(int id)
        {
            try
            {
                var coupon = _dbContext.Coupons.FirstOrDefault(p => p.CouponId == id);
                _response.Result= _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto Get(string code)
        {
            try
            {
                var coupon = _dbContext.Coupons.FirstOrDefault(p => p.CouponCode.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                var couponObj = _mapper.Map<Coupon>(couponDto);
                _dbContext.Coupons.Add(couponObj);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(couponObj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                var couponObj = _mapper.Map<Coupon>(couponDto);
                _dbContext.Coupons.Update(couponObj);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(couponObj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var couponObj = _dbContext.Coupons.First(u => u.CouponId == id);
                _dbContext.Coupons.Remove(couponObj);
                _dbContext.SaveChanges();
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
