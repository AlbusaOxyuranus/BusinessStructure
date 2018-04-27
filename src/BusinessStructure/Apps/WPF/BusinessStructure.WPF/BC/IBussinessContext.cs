using System;
using BlackBee.Toolkit.Rest.EasyData;

namespace BusinessStructure.Bc
{
    public interface IBussinessContext:IDisposable
    {
        /// <summary>
        /// Доуступ к контекту данных
        /// </summary>
        IDataContext DataContext { get; }
    }
}
