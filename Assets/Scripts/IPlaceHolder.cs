namespace Assets.Scripts
{
	public interface IPlaceHolder <T>
	{
		T GetAvailable(Customer customer);
	}
}
