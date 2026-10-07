using School.Application.Interfaces;
using School.Core.Models;

namespace School.API.GraphQL.DataLoaders;

public class GroupByIdDataLoader : BatchDataLoader<string, Group>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public GroupByIdDataLoader(IServiceScopeFactory scopeFactory, IBatchScheduler batchScheduler, DataLoaderOptions options) : base(batchScheduler, options)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task<IReadOnlyDictionary<string, Group>> LoadBatchAsync( IReadOnlyList<string> keys, CancellationToken cancellationToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();

        var groupService = scope.ServiceProvider
            .GetRequiredService<IGroupService>();

        var result = await groupService.GetByIdsAsync(keys);

        if (result.IsFailure)
        {
            throw new GraphQLException(
                ErrorBuilder.New()
                    .SetMessage(result.Error!.Message)
                    .SetCode(result.Error.Code)
                    .SetExtension("statusCode", result.Error.StatusCode)
                    .Build());
        }

        return result.Data!.ToDictionary(group => group.Id);
    }
}