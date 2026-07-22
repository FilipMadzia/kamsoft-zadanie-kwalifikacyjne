using kamsoft_zadanie_kwalifikacyjne.Enums;

namespace kamsoft_zadanie_kwalifikacyjne.Payloads
{
	public class ParseContentPayload
	{
		public ContentType ContentType { get; set; }
		public string Content { get; set; } = string.Empty;
	}
}
