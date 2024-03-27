namespace RockTest.Rand.Contracts.Interfaces;

public interface IRandomizer
{
	Task<int> GetValueAsync(CancellationToken cancellationToken);
}
