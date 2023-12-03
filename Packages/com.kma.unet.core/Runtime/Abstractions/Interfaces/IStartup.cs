namespace UNet.Core.Abstractions
{
    public interface IStartup
	{
		public void ConfigureStartup(IStartupContext context);
	}
}