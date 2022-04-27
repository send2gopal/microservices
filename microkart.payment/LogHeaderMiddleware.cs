namespace microkart.payment
{
    public class LogHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public LogHeaderMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var header = context.Request.Headers["x-correlation-id"];
            if (header.Count > 0)
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<LogHeaderMiddleware>>();
                using (logger.BeginScope("{@CorrelationId}", header[0]))
                {
                    await this._next(context);
                }
            }
            else
            {
                await this._next(context);
            }
        }
    }
}
