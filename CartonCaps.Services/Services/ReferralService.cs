using CartonCaps.Data;
using CartonCaps.Data.Entities;
using CartonCaps.Services.Services.Interfaces;
using CartonCaps.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CartonCaps.Services.Services
{
    /// <summary>
    /// Service for managing referrals.
    /// </summary>
    public class ReferralService : IReferralService
    {
        private readonly ReferralDbContext context;
        private readonly ILogger<ReferralService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferralService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logger">The logger instance.</param>
        public ReferralService(ReferralDbContext context, ILogger<ReferralService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        /// <summary>
        /// Creates a new referral.
        /// </summary>
        /// <param name="request">The request containing referral details.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response containing the created referral.</returns>
        public async Task<CreateReferralResponse> CreateReferral(CreateReferralRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating referral with code {ReferralCode}", request.ReferralCode);
            var referral = new Referral
            {
                ReferralCode = request.ReferralCode,
                ReferralStatus = request.ReferralStatus,
                Referee = new Referee
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email
                }
            };

            context.Referrals.Add(referral);
            await context.SaveChangesAsync(cancellationToken: cancellationToken);

            logger.LogInformation("Referral with code {ReferralCode} created", request.ReferralCode);
            return new CreateReferralResponse
            {
                Referral = new ReferralModel
                {
                    Id = referral.Id,
                    ReferralCode = referral.ReferralCode,
                    ReferralStatus = referral.ReferralStatus,
                    RefereeId = referral.Referee.Id,
                    Referee = new RefereeModel
                    {
                        FirstName = referral.Referee.FirstName,
                        LastName = referral.Referee.LastName,
                        PhoneNumber = referral.Referee.PhoneNumber,
                        Email = referral.Referee.Email
                    }
                }
            };
        }

        /// <summary>
        /// Gets a referral by its ID.
        /// </summary>
        /// <param name="id">The referral ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response containing the referral details.</returns>
        public async Task<GetReferralByIdResponse> GetReferralById(Guid id, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting referral with id {Id}", id);
            var referral = await context.Referrals
                .Include(x => x.Referee)
                .SingleOrDefaultAsync(r => r.Id == id, cancellationToken: cancellationToken);

            if (referral == null)
            {
                logger.LogInformation("Referral with id {Id} not found", id);
                return new GetReferralByIdResponse(); // or handle the case when referral is not found
            }

            logger.LogInformation("Referral with id {Id} found", id);
            return new GetReferralByIdResponse()
            {
                Referral = new ReferralModel
                {
                    Id = referral.Id,
                    RefereeId = referral.Referee.Id,
                    ReferralCode = referral.ReferralCode,
                    ReferralStatus = referral.ReferralStatus,
                    Referee = new RefereeModel
                    {
                        FirstName = referral.Referee.FirstName,
                        LastName = referral.Referee.LastName,
                        PhoneNumber = referral.Referee.PhoneNumber,
                        Email = referral.Referee.Email
                    }
                }
            };
        }

        /// <summary>
        /// Gets referrals based on the specified request.
        /// </summary>
        /// <param name="request">The request containing referral filter criteria.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response containing the list of referrals.</returns>
        public async Task<GetReferralsResponse> GetReferrals(GetReferralsRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting referrals with code {ReferralCode}", request.ReferralCode);
            var query = context.Referrals
                .Include(r => r.Referee)
                .Where(r => r.ReferralCode == request.ReferralCode);

            if (request.ReferralStatus.HasValue)
            {
                logger.LogInformation("Filtering referrals by status {ReferralStatus}", request.ReferralStatus);
                query = query.Where(r => r.ReferralStatus == request.ReferralStatus);
            }

            var referrals = await query.ToListAsync(cancellationToken: cancellationToken);

            if (referrals?.Any() != true)
            {
                logger.LogInformation("Referrals with code {ReferralCode} not found", request.ReferralCode);
                return new GetReferralsResponse(); // or handle the case when referrals are not found
            }

            logger.LogInformation("Referrals with code {ReferralCode} found", request.ReferralCode);
            return new GetReferralsResponse()
            {
                Referrals = await query
                .Select(r => new ReferralModel
                {
                    Id = r.Id,
                    RefereeId = r.Referee.Id,
                    ReferralCode = r.ReferralCode,
                    ReferralStatus = r.ReferralStatus,
                    Referee = new RefereeModel
                    {
                        FirstName = r.Referee.FirstName,
                        LastName = r.Referee.LastName,
                        PhoneNumber = r.Referee.PhoneNumber,
                        Email = r.Referee.Email
                    }
                }).ToListAsync(cancellationToken: cancellationToken)
            };
        }

        /// <summary>
        /// Updates the status of a referral.
        /// </summary>
        /// <param name="request">The request containing the referral ID and new status.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response containing the updated referral.</returns>
        public async Task<UpdateReferralStatusResponse> UpdateReferralStatus(UpdateReferralStatusRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating referral status with id {ReferralId} to status {ReferralStatus}", request.ReferralId, request.ReferralStatus.ToString());
            var referral = await context.Referrals
                .Include(r => r.Referee)
                .SingleOrDefaultAsync(r => r.Id == request.ReferralId);

            if (referral == null)
            {
                logger.LogInformation("Referral with id {ReferralId} not found", request.ReferralId);
                return new UpdateReferralStatusResponse(); // or handle the case when referral is not found
            }

            referral.ReferralStatus = request.ReferralStatus;
            await context.SaveChangesAsync(cancellationToken: cancellationToken);

            logger.LogInformation("Referral status with id {ReferralId} updated", request.ReferralId);
            return new UpdateReferralStatusResponse
            {
                Referral = new ReferralModel
                {
                    Id = referral.Id,
                    RefereeId = referral.Referee.Id,
                    ReferralCode = referral.ReferralCode,
                    ReferralStatus = referral.ReferralStatus,
                    Referee = new RefereeModel
                    {
                        FirstName = referral.Referee.FirstName,
                        LastName = referral.Referee.LastName,
                        PhoneNumber = referral.Referee.PhoneNumber,
                        Email = referral.Referee.Email
                    }
                }
            };
        }

        /// <summary>
        /// Updates a referral.
        /// </summary>
        /// <param name="request">The request containing the referral details to update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response containing the updated referral.</returns>
        public async Task<UpdateReferralResponse> UpdateReferral(UpdateReferralRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating referral with id {ReferralId}", request.ReferralId);
            var referral = await context.Referrals
                .Include(r => r.Referee)
                .SingleOrDefaultAsync(r => r.Id == request.ReferralId);

            if (referral == null)
            {
                logger.LogInformation("Referral with id {ReferralId} not found", request.ReferralId);
                return new UpdateReferralResponse(); // or handle the case when referral is not found
            }

            referral.ReferralStatus = request.ReferralStatus;
            referral.Referee.FirstName = request.FirstName;
            referral.Referee.LastName = request.LastName;
            referral.Referee.PhoneNumber = request.PhoneNumber;
            referral.Referee.Email = request.Email;
            referral.ReferralCode = request.ReferralCode;

            context.Referrals.Update(referral);
            await context.SaveChangesAsync(cancellationToken: cancellationToken);

            logger.LogInformation("Referral with id {ReferralId} updated", request.ReferralId);
            return new UpdateReferralResponse
            {
                Referral = new ReferralModel
                {
                    Id = referral.Id,
                    RefereeId = referral.Referee.Id,
                    ReferralCode = referral.ReferralCode,
                    ReferralStatus = referral.ReferralStatus,
                    Referee = new RefereeModel
                    {
                        FirstName = referral.Referee.FirstName,
                        LastName = referral.Referee.LastName,
                        PhoneNumber = referral.Referee.PhoneNumber,
                        Email = referral.Referee.Email
                    }
                }
            };
        }
    }
}
