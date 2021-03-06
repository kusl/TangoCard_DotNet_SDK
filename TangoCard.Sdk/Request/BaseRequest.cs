﻿//  
//  BaseRequest.cs
//  TangoCard_DotNet_SDK
//  
//  Copyright (c) 2013 Tango Card, Inc
//  All rights reserved.
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions: 
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software. 
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
// 

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

using TangoCard.Sdk.Response;
using TangoCard.Sdk.Common;
using TangoCard.Sdk.Service;
using System.Runtime.Serialization;
using TangoCard.Sdk.Response.Success;

namespace TangoCard.Sdk.Request
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Abstract request base.
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [DataContract]
    public abstract class BaseRequest
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or
        ///                                         illegal values. </exception>
        ///
        /// <param name="enumTangoCardServiceApi">  The enum tango card service api. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public BaseRequest(
            TangoCardServiceApiEnum enumTangoCardServiceApi = TangoCardServiceApiEnum.UNDEFINED
        ) {
            // -----------------------------------------------------------------
            // validate inputs
            // -----------------------------------------------------------------
            if (enumTangoCardServiceApi.Equals(TangoCardServiceApiEnum.UNDEFINED))
            {
                SdkConfig appConfig = SdkConfig.Instance;
                string tc_sdk_environment_default = appConfig["tc_sdk_environment_default"];
                this.TangoCardServiceApi = (TangoCardServiceApiEnum)Enum.Parse(typeof(TangoCardServiceApiEnum), tc_sdk_environment_default);
            }
            else if (!Enum.IsDefined(typeof(TangoCardServiceApiEnum), enumTangoCardServiceApi))
            {
                throw new ArgumentException(message: "Parameter 'enumTangoCardServiceApi' is not a defined service environment.");
            }
            else
            {
                this.TangoCardServiceApi = enumTangoCardServiceApi;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the tango card service api. </summary>
        ///
        /// <value> The tango card service api. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public TangoCardServiceApiEnum TangoCardServiceApi { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the full pathname of the request file. </summary>
        ///
        /// <value> The full pathname of the request file. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract string RequestAction
        {
            get;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the request controller. </summary>
        ///
        /// <value> The request controller. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract string RequestController
        {
            get;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Executes the given out T. </summary>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="response"> [out] The response. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public virtual bool Execute<T>(string request, out T response) where T : SuccessResponse
        {
            var proxy = new ServiceProxy(this);
            return proxy.ExecuteRequest<T>(request, out response);
        }
    }
}
