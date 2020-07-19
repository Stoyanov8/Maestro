using AutoMapper;
using Core.Models;
using Core.Services;
using Core.Services.Identity;
using Maestro.Core.Extensions;
using Maestro.Requests.Data;
using Maestro.Requests.Data.Models;
using Maestro.Requests.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static Maestro.Core.Constants.ErrorMessages;
namespace Maestro.Requests.Services.Requests
{
    public class RequestService : DataService<Request>, IRequestService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
    
        public RequestService(ICurrentUserService currentUserService,
            RequestDbContext context,
            IMapper mapper)
            : base(context)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
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


                //TODO Notify here with mb ?
                await Save(req);

                return Result.Success;
            }
            catch (System.Exception)
            {
                return Result.Failure(InternalServerError);
                throw;
            }
        }

        public async Task<IEnumerable<RequestOutputModel>> GetCurrentUserRequests()
        {
            var req = await All()
                .Include(r=> r.Category)
                .Where(r => r.IssuerId == _currentUserService.UserId).ToListAsync();

            return _mapper.Map<IEnumerable<Request>, IEnumerable<RequestOutputModel>>(req);
        }
    }
}
