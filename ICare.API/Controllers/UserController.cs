﻿using ICare.Core.ApiDTO;
using ICare.Core.Data;
using ICare.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ICare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IFileService _fileService;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ITokenService _jWTService;
        private readonly IResetPasswordServices _resetPasswordServices;
        private readonly IAuthService _authService;
        private readonly ISocialLoginAndRegistrationService _socialLoginAndRegistration;

        public UserController(IUserServices userServices,IFileService fileService, IPasswordHashingService passwordHashingService, ITokenService jWTService,IResetPasswordServices resetPasswordServices,IAuthService authService,ISocialLoginAndRegistrationService socialLoginAndRegistration)
        {
            this._userServices = userServices;
            this._fileService = fileService;
            this._passwordHashingService = passwordHashingService;
            this._jWTService = jWTService;
            this._resetPasswordServices = resetPasswordServices;
            this._authService = authService;
            this._socialLoginAndRegistration = socialLoginAndRegistration;
        }

        /// <summary>
        /// SignUp Page for Patient
        /// </summary>
        /// <param name="request"></param>
        /// <returns>token</returns>
        [HttpPost]
        [Route("PatientRegistration")]
        public ActionResult<ApiResponse<RegistrationApiDTO.Response>> PatientRegistration(RegistrationApiDTO.Request request)
        {


            var response = new ApiResponse<RegistrationApiDTO.Response>();
            if (_userServices.CheckEmailExist(request.Email))
            {
                response.AddError("The email is already exist");
                return Ok(response);
            }
            var hashedPassword = _passwordHashingService.GetHash(request.Password);
            var passwordForLogin = request.Password;
                request.Password = hashedPassword;
            _userServices.Registration(request);
            var login = new LoginApiDTO.Request()
            {
                Email = request.Email,
                Password = passwordForLogin
            };
            string refreshToken;
            //TODO: Return the Token 
            var token = _jWTService.AuthAndGetToken(login,out refreshToken);

            response.Data = new RegistrationApiDTO.Response();
            response.Data.AccessToken = token;
            response.Data.RefreshToken = refreshToken;

            return Ok(response);
        }
        /// <summary>
        /// SignUp Page for Patient
        /// </summary>
        /// <param name="request"></param>
        /// <returns>token</returns>
        [HttpPost]
        [Route("PatientRegistrationUsingSocial")]
        public async Task<ActionResult<ApiResponse<RegistrationApiDTO.Response>>> PatientRegistrationUsingSocial(RegistrationApiDTO.Request request)
        {


            var response = new ApiResponse<RegistrationApiDTO.Response>();

            response.Data =await _socialLoginAndRegistration.LoginAndRegistrationUsingSocial(request);

            return Ok(response);
        }


            /// <summary>
            /// Upload profile image for user
            /// </summary>
            /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("UploadProilePicture")]
        public async Task< ActionResult<ApiResponse>> UploadProilePicture(IFormFile image)
        {
            var user = _userServices.GetUser(User);
            var response = new ApiResponse();
            if(!_fileService.IsPicture(image.FileName))
            {
                response.AddError("The file must be an image");
                return Ok(response);
            }
            if(_fileService.CheckPictureSizeInMB(image.Length,2))
            {
                response.AddError("The file must be less than 2 MB");
                return Ok(response);
            }
            var imageName = _fileService.GenerateFileName(image.FileName);
            await  _fileService.SavePic(image, imageName);
            _userServices.AddOrUpdateProfilePicture(imageName, user.Id);
            return Ok(response);
        }


        /// <summary>
        /// Get for my account page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("MyAccount")]
        [Authorize]
        public ActionResult<ApiResponse<MyAccountApiDTO.Response>> MyAccount()
        {
            var user = _userServices.GetUser(User);
            if (user == null)
            {
                return NoContent();
            }

            var response = new ApiResponse<MyAccountApiDTO.Response>();
            response.Data = new MyAccountApiDTO.Response();

            response.Data.Email = user.Email;
            response.Data.FirstName = user.FirstName;
            response.Data.LastName = user.LastName;
            response.Data.PhoneNumber = user.PhoneNumber;

            return Ok(response);
        }

        /// <summary>
        /// Post for my account page 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MyAccount")]
        [Authorize]
        public ActionResult<ApiResponse> MyAccount(MyAccountApiDTO.Request request)
        {
            //TODO: Set a new Token 
            var user = _userServices.GetUser(User);
            var response = new ApiResponse();
            if (user == null)
            {
                response.AddError("There is something error");
                return Ok(response);
            }
            //if (_userServices.CheckEmailExist(request.Email))
            //{
            //    response.AddError("The email is already exist please try another one ");
            //    return Ok(response);
            //}

            _userServices.Update(user.Id, request);
                 return Ok(response);
        }

        [HttpPost]
        [Route("GetDrugByNameSearch")]
        public ActionResult<ApiResponse<GetBySearchDTO.Response>> GetDrugByNameSearch(GetBySearchDTO.Request request)
        {
            var response = new ApiResponse<IEnumerable<GetBySearchDTO.Response>>();
            var drugList = _userServices.GetDrugByNameSearch(request);
            if (drugList == null)
            {
                response.AddError("No medicine left for sale");
                return Ok(response);
            }
            response.Data = new List<GetBySearchDTO.Response>();
            response.Data = drugList;
            return Ok(response);

        }
        [HttpGet]
        [Route("getAllEmployees")]
        
        public ActionResult<ApiResponse<getAllEmployeeDTO>> getAllEmployee()
        {
            var users = _userServices.getAllEmployee();
            var response = new ApiResponse<IEnumerable<getAllEmployeeDTO>>();

            if (users == null)
            {
                return NoContent();
            }

            response.Data = users;

            return Ok(response);
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<ActionResult<ApiResponse>> ForgotPassword(ChangeUserPasswordDTO.Request request)
        {

            var response = new ApiResponse();
            var result = _resetPasswordServices.ForgotPassword(request);
            if (result == false)
            {
                response.AddError("This Email Not Exist");
                return Ok(response);
            }
           

            return Ok(response);
        }

        [HttpPut]
        [Route("ChangePassword")]
        [Authorize]
        public ActionResult<ApiResponse> ChangePassword(CahngePasswordApiDTO.Request request)
        {
            var response = new ApiResponse();
            var user = _userServices.GetUser(User);
            if(user == null)
            {
                response.AddError("there is something error");
                return Ok(response);
            }
            var loginModel = new LoginApiDTO.Request()
            {
                Email = user.Email,
                Password = request.OldPassword
            };
            if(_authService.Authentication(loginModel) == null)
            {
                response.AddError("Wrong password");
                return Ok(response);
            }
            _userServices.ChangPassword(user.Id, request.NewPassword); 
            
            return Ok(response);

        }

       

    }
}
