﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ICare.Core.ApiDTO
{
    public class RegistrationApiDTO
    {
        public class Request
        {
            /// <summary>
            /// the Email Of the patient
            /// </summary>
            /// <example>Test@test.test</example>
            [Required]
            [MaxLength(150)]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            /// the password of the patient
            /// </summary>
            /// <example>Q!qwe123</example>
            [Required]
            [MaxLength(100)]
            [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,64})", ErrorMessage = "Please enter a valid password")]
            public string Password { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <example>0777777777</example>
            [MaxLength(15)]
            [Phone]
            public string PhoneNumber { get; set; }

            /// <example>First Name</example>
            [Required]
            [MaxLength(50)]
            public string FirstName { get; set; }

            /// <example>Last Name</example>
            [Required]
            [MaxLength(50)]
            public string LastName { get; set; }


        }

        public class Response
        {
            public string AccessToken { get; set; }

            public string RefreshToken { get; set; }
        }
    }
}
