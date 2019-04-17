using DealMeCore.BusinessLogic.Models;
using DealMeCore.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DealMeCore.WebApi.Middlewares
{
    /// <summary>
    /// ExceptionMiddleware.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHostingEnvironment env;
        private readonly ILogger<ExceptionMiddleware> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="env">The env.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">next
        /// or
        /// loggerFactory</exception>
        public ExceptionMiddleware(
            RequestDelegate next,
            IHostingEnvironment env,
            ILogger<ExceptionMiddleware> logger
        )
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.env = env ?? throw new ArgumentNullException(nameof(env));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Invokes middleware.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateConcurrencyException)
                {
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                }
                else
                {
                    logger.Log(new LogEntry(LogLevel.Error, ex.Message, ex));

                    if (env.IsEnvironment("Debug") || env.IsDevelopment())
                    {
                        throw;
                    }

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    string result = JsonConvert.SerializeObject(
                        new ErrorResponseDto()
                        {
                            Message = "Internal server error."
                        }
                    );

                    await context.Response.WriteAsync(result);
                }
            }
        }
    }
}
