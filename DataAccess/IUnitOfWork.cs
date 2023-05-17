namespace DataAccess
{
    public interface IUnitOfWork
    {
        /// <summary>
        ///     Save the unit or work (transaction)
        /// </summary>
        void Save();
    }
}
