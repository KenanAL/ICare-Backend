﻿using ICare.Core.ApiDTO;
using ICare.Core.Data;
using ICare.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICare.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IUserServices _userServices;
        private readonly IPatientServices _patientServices;

        public CartsController(ICartService cartService, IUserServices userServices,IPatientServices patientServices)
        {
            _cartService = cartService;
            this._userServices = userServices;
            this._patientServices = patientServices;
        }

       

        [HttpPost]
        [Route("AddDrugToCategory")]
        [Authorize]
        public ActionResult<ApiResponse> AddCart(AddToCartApiDTO.Request request)
        {
            var response = new ApiResponse();
            var user = _userServices.GetUser(User);
            var patient = _patientServices.GetPatientByUserId(user.Id);

            var cart = new Cart()
            {
                 PatientId = patient.Id,
                 DrugId = request.drugId,
                Quantity =request.Quantity
            };
             _cartService.Create(cart);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetCartItems")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<IEnumerable<GetCartItemsApiDTO.Response>>>> GetCartItems()
        {
            var response = new ApiResponse<IEnumerable<GetCartItemsApiDTO.Response>>();
            var a = User;
            var user = _userServices.GetUser(User);
            var patient = _patientServices.GetPatientByUserId(user.Id);

            response.Data = await _cartService.GetCartItems(patient.Id);

            return Ok(response);

        }


        [HttpDelete]
        [Route("DeleteCartItem/{id:int}")]
        [Authorize]
        public ActionResult<ApiResponse> DeleteCartItem(int id)
        {
            var response = new ApiResponse();

            _cartService.Delete(id);

            return Ok(response);
        }

        [HttpGet]
        [Route("CheckItemIfInCart/{id:int}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> CheckItemIfInCart(int id)
        {
            var response = new ApiResponse();
            var user = _userServices.GetUser(User);
            var patient = _patientServices.GetPatientByUserId(user.Id);

            if( await _cartService.CheckItemExist(patient.Id,id))
            {
                response.AddError("Item is already in your cart");
                return Ok(response);
            }
            else
            {
            return Ok(response);
            }
        }

        [HttpGet]
        [Route("AddQuantity/{id:int}")]
        [Authorize]
        public ActionResult<ApiResponse> AddQuantity(int id)
        {
            var result = new ApiResponse();

            _cartService.AddQuantity(id);

            return result;
        }

        [HttpGet]
        [Route("MinusQuantity/{id:int}")]
        [Authorize]
        public ActionResult<ApiResponse> MinusQuantity(int id)
        {
            var result = new ApiResponse();

            _cartService.MinusQuantity(id);

            return result;
        }

        [HttpGet]
        [Route("ChangeQuantity/{id:int}&{Quantity:int}")]
        [Authorize]
        public ActionResult<ApiResponse> ChangeQuantity(int id,int Quantity)
        {
            var result = new ApiResponse();

            _cartService.ChangeQuantity(id, Quantity);

            return result;
        }

    }
}
