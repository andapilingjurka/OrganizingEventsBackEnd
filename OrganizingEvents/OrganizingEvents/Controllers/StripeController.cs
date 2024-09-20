﻿using OrganizingEvents.Contracts;
using OrganizingEvents.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizingEvents.Controllers
{

    public class StripeController : Controller
    {
        private readonly IStripeAppService _stripeService;

        public StripeController(IStripeAppService stripeService)
        {
            _stripeService = stripeService;
        }



        [HttpPost("/api/payment/add")] // Rruga për shtimin e pagesës
        public async Task<ActionResult<StripePayment>> AddStripePayment(
    [FromBody] AddStripePayment payment,
    CancellationToken ct)
        {
            StripePayment createdPayment = await _stripeService.AddStripePaymentAsync(
                payment,
                ct);

            return StatusCode(StatusCodes.Status200OK, createdPayment);
        }
    }
}
