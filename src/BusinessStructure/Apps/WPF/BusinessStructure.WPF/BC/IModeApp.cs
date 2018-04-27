namespace BusinessStructure.Bc
{
    /// <summary>
    /// Название типа подключения
    /// </summary>
    public enum ConnectionBase
    {
        MonnaAnnaSite,
        ///// <summary>
        ///// Тестовый режим локальный сервер
        ///// </summary>
        MonnaAnnaLocalSite
        ///// <summary>
        ///// Режим продукта
        ///// </summary>
        //VelcomProd,
        ///// <summary>
        ///// Режим пред продукта
        ///// </summary>
        //VelcomPredProd,
        ///// <summary>
        ///// Режим демо с формирование фэйковых данных без подключения к серверу
        ///// </summary>
        //DemoFake
        //,
        //nbrb
    }
    /// <summary>
    /// Режим подключения
    /// </summary>
    public interface IModeApp
    {
        //Адрес подключения
        string AdressConnection { get; }
        /// <summary>
        /// Название подключения
        /// </summary>
        ConnectionBase ConnectionBase { get; }
    }
}
