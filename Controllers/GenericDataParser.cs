using kamsoft_zadanie_kwalifikacyjne.Enums;
using kamsoft_zadanie_kwalifikacyjne.Payloads;
using kamsoft_zadanie_kwalifikacyjne.Responses;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System.Globalization;

namespace kamsoft_zadanie_kwalifikacyjne.Controllers
{
	[Route("api/v1/parse-content")]
	[ApiController]
	public class GenericDataParser : ControllerBase
	{
		[HttpPost]
		[Consumes("application/json")]
		public ActionResult<ParseContentResponse> ParseContent([FromBody] ParseContentPayload payload)
		{
			byte[] data = Convert.FromBase64String(payload.Content);
			var decodedContent = System.Text.Encoding.UTF8.GetString(data);

			var response = new ParseContentResponse();

			switch (payload.ContentType)
			{
				case ContentType.CSV:
					return ProcessCsv(decodedContent);
				case ContentType.INTERNAL_JSON:
					return ProcessInternalJson(decodedContent);
				default:
					return BadRequest();
			}
		}

		private ParseContentResponse ProcessCsv(string decodedContent)
		{
			using var reader = new StringReader(decodedContent);
			using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

			var objects = csvReader.GetRecords<dynamic>().ToArray();

			return new ParseContentResponse
			{
				HttpCode = 200,
				ObjectCount = objects.Length,
				Objects = objects
			};
		}

		private ParseContentResponse ProcessInternalJson(string decodedContent)
		{
			var objects = System.Text.Json.JsonSerializer.Deserialize<object[]>(decodedContent);

			return new ParseContentResponse
			{
				HttpCode = 200,
				ObjectCount = objects?.Length ?? 0,
				Objects = objects ?? Array.Empty<object>()
			};
		}
	}
}
