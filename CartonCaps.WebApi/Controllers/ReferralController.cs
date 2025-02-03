using CartonCaps.Services.Services.Interfaces;
using CartonCaps.Shared.Extensions;
using CartonCaps.Shared.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CartonCaps.WebApi.Controllers;

/// <summary>
/// Controller for handling referral-related operations.
/// </summary>
[ApiController]
[Route("api/v{apiVersion:apiVersion}/[controller]")]
public class ReferralController : ControllerBase
{
    private readonly IReferralService _referralService;
    private readonly IEnumerable<IValidator> _validators;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReferralController"/> class.
    /// </summary>
    /// <param name="referralService">The referral service.</param>
    /// <param name="validators">The collection of validators.</param>
    public ReferralController(IReferralService referralService, IEnumerable<IValidator> validators)
    {
        _referralService = referralService;
        _validators = validators;
    }


    /// <summary>
    /// This GET method retrieves a collection of referrals based on the request. The 'ReferralCode' property is required and filters referrals with the referrer's code.
    /// The 'status' property is optional and can be appended to filter referrals of a given status. If no referrals are found, a 404 response is returned.
    /// </summary>
    /// <param name="referralCode">The referral code to filter referrals.</param>
    /// <param name="referralStatus">The optional status to filter referrals.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A list of referrals matching the criteria.</returns>
    /// <remarks>
    /// Example request:
    ///     GET: api/v1/referral/getreferrals?referralCode=ABC123
    /// </remarks>
    [HttpGet]
    [Route("getreferrals")]
    [ProducesResponseType(typeof(GetReferralsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReferrals([FromQuery] string referralCode, [FromQuery] ReferralStatus? referralStatus, CancellationToken cancellationToken)
    {
        var request = new GetReferralsRequest { ReferralCode = referralCode, ReferralStatus = referralStatus };
        var response = await _referralService.GetReferrals(request, cancellationToken);
        return !response.Referrals.Any() ? NotFound() : Ok(response);
    }

    /// <summary>
    /// This GET method retrieves an existing referral specified by the referral ID in the path. If no existing referral is found, a 404 response is returned.
    /// </summary>
    /// <param name="id">The unique identifier of the referral.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>The referral details.</returns>
    /// <remarks>
    /// Example request:
    ///     GET: api/v1/referral/getreferralbyid/d290f1ee-6c54-4b01-90e6-d701748f0851
    /// </remarks>
    [HttpGet]
    [Route("getreferralbyid/{id}")]
    [ProducesResponseType(typeof(GetReferralByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReferralsById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _referralService.GetReferralById(id, cancellationToken);
        return response.Referral == default ? NotFound() : Ok(response);
    }

    /// <summary>
    /// This POST method creates a new referral using the provided information. The request body must include the code from the referrer and referee first and
    /// last name. By default, the created referral has status 'Pending' but can be overridden if provided on the request body.
    /// </summary>
    /// <param name="request">The request containing referral details.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>The created referral details.</returns>   
    /// <remarks>
    /// Example request:
    ///     POST: api/v1/referral/createreferral
    ///     {
    ///         "firstName": "John",
    ///         "lastName": "Doe",
    ///         "phooneNumber": "555-902-6489",
    ///         "email": "",
    ///         "referralStatus": "Pending",
    ///         "referralCode": "ABC123"
    ///     }
    /// </remarks>
    [HttpPost]
    [Route("createreferral")]
    [ProducesResponseType(typeof(CreateReferralResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateReferral([FromBody] CreateReferralRequest request, CancellationToken cancellationToken)
    {
        var validator = _validators.GetValidator<CreateReferralRequest>();
        var result = await validator?.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors.Select(x => x.ErrorMessage).ToList());
        }

        var response = await _referralService.CreateReferral(request, cancellationToken);
        return Created($"/getreferralbyid/{response.Referral.Id}", response);
    }

    /// <summary>
    /// This PATCH method updates the status of an existing referral specified by the referral ID in the path. The request body must include the new status as a string for the referral. 
    /// If no existing referral is found, a 404 response is returned.
    /// </summary>
    /// <param name="request">The request containing the referral status update details.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>The updated referral status details.</returns>
    /// <remarks>
    /// Example request:
    /// PATCH: api/v1/referral/updatereferralstatus
    /// {
    ///     "referralId": "d290f1ee-6c54-4b01-90e6-d701748f0851",
    ///     "referralStatus": "Complete"
    /// }
    /// </remarks>
    [HttpPatch]
    [Route("updatereferralstatus")]
    [ProducesResponseType(typeof(UpdateReferralStatusResponse), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateReferralStatus([FromBody] UpdateReferralStatusRequest request, CancellationToken cancellationToken)
    {
        var response = await _referralService.UpdateReferralStatus(request, cancellationToken);
        return response.Referral == default ? NotFound() : Accepted(response);
    }

    /// <summary>  
    /// This PUT method updates a referral specified by the referral ID in the path. The request body must include the full updated information for the referral.  
    /// This operation will accept and override the existing referral with the new information. If no matching referral exists, it will be created with the provided information.  
    /// </summary>  
    /// <param name="request">The request containing updated referral details.</param>  
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>  
    /// <returns>The updated referral details.</returns>  
    /// <remarks>  
    /// Example request:  
    /// PUT: api/v1/referral/updatereferral  
    /// {
    ///     "referralId": "d290f1ee-6c54-4b01-90e6-d701748f0851",  
    ///     "firstName": "John",  
    ///     "lastName": "Doe",  
    ///     "phoneNumber": "555-902-6489",  
    ///     "email": "",  
    ///     "referralStatus": "Complete",  
    ///     "referralCode": "ABC123"  
    /// }
    /// </remarks>  
    [HttpPut]
    [Route("updatereferral")]
    [ProducesResponseType(typeof(UpdateReferralResponse), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateReferral([FromBody] UpdateReferralRequest request, CancellationToken cancellationToken)
    {
        var response = await _referralService.UpdateReferral(request, cancellationToken);
        return response.Referral == default ? NotFound() : Accepted(response);
    }
}
