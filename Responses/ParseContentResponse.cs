namespace kamsoft_zadanie_kwalifikacyjne.Responses
{
	public class ParseContentResponse
	{
		public int HttpCode { get; set; }
		public int ObjectCount { get; set; }
		public object[] Objects { get; set; } = [];
	}
}
