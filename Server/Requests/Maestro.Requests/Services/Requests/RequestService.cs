using AutoMapper;
using Core.Models;
using Core.Services;
using Core.Services.Identity;
using Maestro.Core.Extensions;
using Maestro.Core.Messages;
using Maestro.Requests.Data;
using Maestro.Requests.Data.Models;
using Maestro.Requests.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using static Maestro.Core.Constants.ErrorMessages;
namespace Maestro.Requests.Services.Requests
{
    public class RequestService : DataService<Request>, IRequestService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public RequestService(ICurrentUserService currentUserService,
            RequestDbContext context,
            IBus bus,
            IMapper mapper)
            : base(context)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<Result> Create(RequestInputModel model)
        {
            try
            {
                var req = new Request
                {
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    IssuerId = _currentUserService.UserId
                };

                await Data.AddAsync(req);
                await Data.SaveChangesAsync();

                await _bus.Publish(new RequestCreatedMessage() { RequestId = req.Id });

                return Result.Success;
            }
            catch (System.Exception)
            {
                return Result.Failure(InternalServerError);
                throw;
            }
        }

        public async Task<IEnumerable<RequestOutputModel>> GetCurrentUserRequests()
            => await GetRequestsByFilter(r => r.IssuerId == _currentUserService.UserId);

        public async Task<IEnumerable<RequestOutputModel>> RequestsIn(RequestsInIdsInputModel input)
            => await GetRequestsByFilter(r => input.Ids.Contains(r.Id));

        private async Task<IEnumerable<RequestOutputModel>> GetRequestsByFilter(Expression<Func<Request, bool>> filter)
        {
            var req = await All()
                .Include(r => r.Category)
                .Where(filter)
                .ToListAsync();

            return _mapper.Map<IEnumerable<Request>, IEnumerable<RequestOutputModel>>(req);
        }
    }
}
