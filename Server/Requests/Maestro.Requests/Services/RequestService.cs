using AutoMapper;
using Core.Models;
using Maestro.Core.Extensions;
using Maestro.Requests.Models;
using Requests.Data;
using Requests.Data.Model;
using System.Threading.Tasks;

namespace Maestro.Requests.Services
{
    public class RequestService : IRequestService
    {
        private readonly IMapper _mapper;
        private readonly RequestDbContext _context;

        public RequestService(IMapper mapper, RequestDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result> Create(RequestInputModel model)
        {
            var req = _mapper.Map<RequestInputModel, Request>(model);

            try
            {
                await _context.AddAsync(req);
                return Result.Success;
            }
            catch (System.Exception e)
            {
                return Result.Failure(e.ReadException());
                throw;
            }
        }
    }
}
