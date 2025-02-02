using CartonCaps.Shared.Models;

namespace CartonCaps.Services.Services.Interfaces
{
    /// <summary>
    /// Interface for referral service operations.
    /// </summary>
    public interface IReferralService : IScopedService
    {
        /// <summary>
        /// Retrieves a list of referrals based on the provided request.
        /// </summary>
        /// <param name="request">The request containing referral criteria.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the referrals response.</returns>
        Task<GetReferralsResponse> GetReferrals(GetReferralsRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a referral by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the referral.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the referral response.</returns>
        Task<GetReferralByIdResponse> GetReferralById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Creates a new referral.
        /// </summary>
        /// <param name="request">The request containing referral details.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the create referral response.</returns>
        Task<CreateReferralResponse> CreateReferral(CreateReferralRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing referral.
        /// </summary>
        /// <param name="request">The request containing updated referral details.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the update referral response.</returns>
        Task<UpdateReferralResponse> UpdateReferral(UpdateReferralRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the status of an existing referral.
        /// </summary>
        /// <param name="request">The request containing the referral status update details.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the update referral status response.</returns>
        Task<UpdateReferralStatusResponse> UpdateReferralStatus(UpdateReferralStatusRequest request, CancellationToken cancellationToken);
    }
}
