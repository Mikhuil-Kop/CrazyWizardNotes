namespace UNet.Core.Abstractions
{
	public interface IConfiguration
	{
		IConfiguration GetSection(string path);
		string Get(string path);
	}
}