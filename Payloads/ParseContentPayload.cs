using kamsoft_zadanie_kwalifikacyjne.Enums;

namespace kamsoft_zadanie_kwalifikacyjne.Payloads
{
	public class ParseContentPayload
	{
		public ContentType Type { get; set; }
		public string Content { get; set; } = string.Empty;
	}
}
