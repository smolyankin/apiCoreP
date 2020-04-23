using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using apiCoreP.Data;
using apiCoreP.Extensions;
using apiCoreP.Model;
using apiCoreP.Requests;
using apiCoreP.Responses;

namespace apiCoreP.Services
{
    /// <summary>
    /// subscriber service
    /// </summary>
    public class SubscriberService : ISubscriberService
    {
        private readonly ApplicationContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public SubscriberService(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get subscriber by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Subscriber> GetById(long id)
        {
            return await _context.Subscribers.FindAsync(id);
        }

        /// <summary>
        /// get subscribers by page
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<SubscribersResponse> GetSubscribers(int? skip = 0, int? count = 10)
        {
            var result = new SubscribersResponse();

            count.LimitCount();

            result.Count = await _context.Subscribers.CountAsync();
            result.Items = await _context.Subscribers.OrderByDescending(x => x.Id).Skip(skip ?? 0).Take((int)count).ToListAsync();

            return result;
        }

        /// <summary>
        /// add subscriber
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Subscriber> Create(CreateSubscriberRequest request)
        {
            var subscriber = request.Transform<Subscriber>();

            _context.Subscribers.Add(subscriber);

            await _context.SaveChangesAsync();

            return subscriber;
        }

        /// <summary>
        /// update subscriber
        /// </summary>
        /// <param name="subscriber">subscriber to update</param>
        /// <param name="request">edit subscriber request</param>
        /// <returns></returns>
        public async Task Edit(Subscriber subscriber, EditSubscriberRequest request)
        {
            _context.Entry(subscriber).State = EntityState.Modified;

            try
            {
                request.ToCopy(subscriber);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        /// <summary>
        /// delete subscriber
        /// </summary>
        /// <param name="subscriber"></param>
        /// <returns></returns>
        public async Task Delete(Subscriber subscriber)
        {
            _context.Subscribers.Remove(subscriber);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// get subscribers for printing
        /// </summary>
        /// <returns></returns>
        public async Task<List<Subscriber>> SubscribersToPrint()
        {
            return await _context.Subscribers.OrderByDescending(x => x.Id).ToListAsync();
        }
    }

    /// <summary>
    /// subscriber interface
    /// </summary>
    public interface ISubscriberService
    {
        /// <summary>
        /// get subscriber by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Subscriber> GetById(long id);

        /// <summary>
        /// get subscribers by page
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<SubscribersResponse> GetSubscribers(int? skip = 0, int? count = 10);

        /// <summary>
        /// add subscriber
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Subscriber> Create(CreateSubscriberRequest request);

        /// <summary>
        /// update subscriber
        /// </summary>
        /// <param name="subscriber">subscriber to update</param>
        /// <param name="request">edit subscriber request</param>
        /// <returns></returns>
        Task Edit(Subscriber subscriber, EditSubscriberRequest request);

        /// <summary>
        /// delete subscriber
        /// </summary>
        /// <param name="subscriber"></param>
        /// <returns></returns>
        Task Delete(Subscriber subscriber);

        /// <summary>
        /// get subscribers for printing
        /// </summary>
        /// <returns></returns>
        Task<List<Subscriber>> SubscribersToPrint();
    }
}
