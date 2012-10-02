﻿//
//  TangoCardServiceApi.cs
//  TangoCard_DotNet_SDK
//  
//  Copyright (c) 2012 Tango Card, Inc
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
using System.Linq;
using System.Text;
using TangoCard.Sdk.Service;
using TangoCard.Sdk.Response.Success;
using TangoCard.Sdk.Request;

namespace TangoCard.Sdk
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Tango card service api.
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public sealed class TangoCardServiceApi
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor that prevents a default instance of this class from being created.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private TangoCardServiceApi() { }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets an available balance. </summary>
        ///
        /// <param name="enumTangoCardServiceApi">  The enum tango card service api. </param>
        /// <param name="username">                 The username. </param>
        /// <param name="password">                 The password. </param>
        /// <param name="response">                 [out] The response. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static bool GetAvailableBalance(
           TangoCardServiceApiEnum enumTangoCardServiceApi,
           string username,
           string password,
           out GetAvailableBalanceResponse response
           )
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new System.ArgumentNullException("username");
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new System.ArgumentNullException("password");
            }

            // set up the request
            var request = new GetAvailableBalanceRequest
            (
                enumTangoCardServiceApi: enumTangoCardServiceApi,
                username: username.Trim(),
                password: password
            );

            // make the request
            return request.Execute(out response);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Purchase card. </summary>
        ///
        /// <param name="enumTangoCardServiceApi">  The enum tango card service api. </param>
        /// <param name="username">                 The username. </param>
        /// <param name="password">                 The password. </param>
        /// <param name="cardSku">                  The card sku. </param>
        /// <param name="cardValue">                The card value. </param>
        /// <param name="tcSend">                   Determines if Tango Card Service will send an email with gift card information to recipient. </param>
        /// <param name="recipientName">            Name of the recipient. </param>
        /// <param name="recipientEmail">           The recipient email. </param>
        /// <param name="giftMessage">              Message describing the gift. </param>
        /// <param name="giftFrom">                 The gift from. </param>
        /// <param name="companyIdentifier">        (optional) The Company identifier for which Email Template to use when sending Gift Card. </param>
        /// <param name="response">                 [out] The response. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static bool PurchaseCard(
            TangoCardServiceApiEnum enumTangoCardServiceApi,
            string username,
            string password,
            string cardSku,
            int cardValue,
            bool tcSend,
            string recipientName,
            string recipientEmail,
            string giftMessage,
            string giftFrom,
            string companyIdentifier,
            out PurchaseCardResponse response
            )
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new System.ArgumentNullException("username");
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new System.ArgumentNullException("password");
            }

            // set up the request
            var request = new PurchaseCardRequest
            (
                enumTangoCardServiceApi: enumTangoCardServiceApi,
                username:       username.Trim(),
                password:       password,
                cardSku:        cardSku.Trim(),
                cardValue:      cardValue,
                tcSend:         tcSend,
                recipientName:  String.IsNullOrEmpty(recipientName)         ? null : recipientName.Trim(),
                recipientEmail: String.IsNullOrEmpty(recipientEmail)        ? null : recipientEmail.Trim(),
                giftMessage:    String.IsNullOrEmpty(giftMessage)           ? null : giftMessage.Trim().Replace(System.Environment.NewLine, "<br>"),
                giftFrom:       String.IsNullOrEmpty(giftFrom)              ? null : giftFrom.Trim(),
                companyIdentifier: String.IsNullOrEmpty(companyIdentifier)  ? null : companyIdentifier.Trim()
            );

            // make the request
            return request.Execute(out response);
        }
    }
}
