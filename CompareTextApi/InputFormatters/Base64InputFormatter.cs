using CompareTextApi.Requests;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CompareTextApi.Formatter
{
    /// <summary>
    /// Base64 input formatter. Allows to send request body in Base64
    /// </summary>
    public class Base64InputFormatter : TextInputFormatter
    {
        /// <summary>
        /// Media type for base64 content
        /// </summary>
        public const string MediaType = "application/custom";

        /// <summary>
        /// Base 64 Input Formatter
        /// </summary>
        public Base64InputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/custom"));
            SupportedEncodings.Add(Encoding.UTF8);
        }

        /// <inheritdoc />
        protected override bool CanReadType(Type type)
            => type == typeof(CompareInputRequest);

        /// <inheritdoc />
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(
            InputFormatterContext context, Encoding effectiveEncoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;
            var logger = serviceProvider.GetRequiredService<ILogger<Base64InputFormatter>>();

            using var reader = new StreamReader(httpContext.Request.Body, effectiveEncoding);
            try
            {
                string base64Body = await reader.ReadToEndAsync();
                byte[] decodedBytes = Convert.FromBase64String(base64Body);
                string decodedTxt = Encoding.UTF8.GetString(decodedBytes);

                var result = JsonSerializer.Deserialize<CompareInputRequest>(decodedTxt, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });


                return await InputFormatterResult.SuccessAsync(new CompareInputRequest() { Input = "Test" });
            }
            catch (Exception ex)
            {
                {
                    logger.LogError("Read failed: {ex}", ex);
                    return await InputFormatterResult.FailureAsync();
                }
            }
        }
    }
}