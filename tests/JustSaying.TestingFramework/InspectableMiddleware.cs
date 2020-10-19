using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using JustSaying.Messaging.Middleware;
using JustSaying.Messaging.Middleware.Handle;
using JustSaying.Models;

namespace JustSaying.TestingFramework
{
    public class InspectableMiddleware<TMessage> : MiddlewareBase<HandleMessageContext, bool> where TMessage : Message
    {
        public InspectableMiddleware()
        {
            Handler = new InspectableHandler<TMessage>();
        }

        public InspectableHandler<TMessage> Handler { get; }
        protected override async Task<bool> RunInnerAsync(HandleMessageContext context, Func<CancellationToken, Task<bool>> func, CancellationToken stoppingToken)
        {
            await Handler.Handle(context.MessageAs<TMessage>());
            return true;
        }
    }
}
