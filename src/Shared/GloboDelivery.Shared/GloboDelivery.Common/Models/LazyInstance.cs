using Microsoft.Extensions.DependencyInjection;

namespace GloboDelivery.Common.Models
{
    public class LazyInstance<T> : Lazy<T>
    {
        public LazyInstance(IServiceProvider serviceProvider)
            : base(() => serviceProvider.GetRequiredService<T>())
        {

        }
    }
}
