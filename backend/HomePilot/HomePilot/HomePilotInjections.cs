using HomePilot.Db;
using HomePilot.Managers;
using Microsoft.EntityFrameworkCore;

namespace HomePilot;

public static class HomePilotInjections
{
    public static void ConfigureServices(
        this IServiceCollection services
    )
    {
        services.AddDbContext<HomePilotDbContext>(
            dbContextOptions => dbContextOptions
            .UseMySql(
                HomePilotDbContext.HomePilotDbConnectionString,
                HomePilotDbContext.HomerPilotMariaDbVersion,
                options =>
                {
                    /*
                     * PRED-2032 otherwise we have a lot of 
                     * "
                     * An exception has been raised that is likely due to a transient failure.
                     * Consider enabling transient error resiliency by adding 'EnableRetryOnFailure()' to the 'UseMySql' call.
                     * Connect Timeout expired.
                     * Unable to connect to any of the specified MySQL hosts.
                     * "
                     * errors in prod
                     */
                    options.EnableRetryOnFailure();
                    options.CommandTimeout(5) /* PRED-2328 */;
                }
            ));
        services.AddScoped<AmendmentsManager>();
        services.AddScoped<LeaseManager>();

    }
}

