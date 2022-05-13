namespace WebAPI5.Abstract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReader
    {
        Task<IEnumerable<string>> Weathers(int days);
    }
}