using BusinessLogic;
using CompareTextApi.Formatter;
using CompareTextApi.Requests;
using CompareTextApi.Storage;
using Microsoft.AspNetCore.Mvc;

namespace CompareTextApi.Controllers
{
    /// <summary>
    /// Controller to compare text
    /// </summary>
    [ApiController]
    [Route("v1/diff")]
    public class CompareTextController : ControllerBase
    {
        private readonly ILogger<CompareTextController> _logger;
        private readonly IStorageForTextComparison _storageForTextComparison;

        /// <summary>
        /// Compare text controller with storage and logger
        /// </summary>
        /// <param name="storageForTextComparison"></param>
        /// <param name="logger"></param>
        public CompareTextController(IStorageForTextComparison storageForTextComparison, ILogger<CompareTextController> logger)
        {
            _storageForTextComparison = storageForTextComparison;
            _logger = logger;
        }

        /// <summary>
        /// Add left side text for comparison. If there is left text with specified id in storage - the left text will be overwritten. 
        /// </summary>
        /// <param name="id">Id of the left side input</param>
        /// <param name="inputRequest">Input text.</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST diff/b37ea680-6ed0-4023-baed-13b123ab6498/left
        ///     {
        ///        "input": "some value to be compared"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Left side text with specified id is added to storage</response>
        [HttpPost, Consumes("application/json", Base64InputFormatter.MediaType)]
        [Route("{id:guid}/left")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IResult SetLeftValueToCompare(Guid id, [FromBody] CompareInputRequest inputRequest)
        {
            _storageForTextComparison.AddLeftSideText(id, inputRequest.Input);
            return Results.Ok();
        }

        /// <summary>
        /// Add right side text for comparison. If there is right text with specified id in storage - the right text will be overwritten. 
        /// </summary>
        /// <param name="id">Id of the right side input</param>
        /// <param name="inputRequest">Input text.</param>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST diff/b37ea680-6ed0-4023-baed-13b123ab6498/right
        ///     {
        ///        "input": "some value to be compared"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Right side text with specified id is added to storage</response>
        [HttpPost, Consumes("application/json", Base64InputFormatter.MediaType)]
        [Route("{id:guid}/right")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IResult SetRightValueToCompare(Guid id, [FromBody] CompareInputRequest inputRequest)
        {
            _storageForTextComparison.AddRightSideText(id, inputRequest.Input);
            return Results.Ok();
        }

        /// <summary>
        /// Compare texts from the storage. Use endpoints to add left and right text before executing this one.
        ///     If it is equal - returns IsEqual = true, IsSameSize = true.
        ///     if it is not equal size - returns IsEqual = false, IsSameSize = false without OffsetDetails.
        ///     If it is not equal and the same size - returns IsEqual = false, IsSameSize = true and offsets.
        /// </summary>
        /// <param name="id">Id of the text which was added</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /diff/b37ea680-6ed0-4023-baed-13b123ab6498
        /// Sample response:
        /// 
        ///     {
        ///         "isEqual": false,
        ///         "isSameSize": false,
        ///         "details": {
        ///           "offsets": [
        ///             0,
        ///             10
        ///           ],
        ///           "differenceLength": 2
        ///     }
        /// </remarks>
        /// <response code="200">Comparison has been successfully done.</response>
        /// <response code="422">Left or right text wasn't set.</response>
        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public IResult Compare(Guid id)
        {
            try
            {
                var leftText = _storageForTextComparison.GetLeftSideText(id);
                if (leftText == null)
                {
                    return Results.Problem("Left text to compare wasn't set", id.ToString(), 422);
                }

                var rightText = _storageForTextComparison.GetRightSideText(id);
                if (rightText == null)
                {
                    return Results.Problem("Right text to compare wasn't set", id.ToString(), 422);
                }

                var result = TextComparer.Compare(leftText, rightText);
                return Results.Ok(result);
            }
            catch (Exception ex) {
                _logger.LogError(exception: ex, "Error while comparing text");
                throw;
            }
        }
    }
}