using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Member Variables

        private const int DuplicateKeyRowSqlExceptionNumber = 2601;
        private const int UniqueConstraintViolationSqlExceptionNumber = 2627;
        private const int CheckConstraintViolationNumber = 547;
        private const int InvalidColumnNumber = 207;
        private readonly DbContext _context;

        #endregion

        #region Constructor

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        #endregion

        #region IUnitOfWork Implementation

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Exception e = ex;

                while (e != null)
                {
                    if (e is SqlException exception)
                    {
                        ProcessSqlError(exception);
                        break;
                    }

                    e = e.InnerException;
                }

                throw;
            }
        }

        #endregion

        #region Private Helpers

        private static void ProcessSqlError(SqlException sqlException)
        {
            if (sqlException.Number == DuplicateKeyRowSqlExceptionNumber)
            {
                throw new Exception("unique constraint or key violated", sqlException);
            }

            if (sqlException.Number == UniqueConstraintViolationSqlExceptionNumber)
            {
                throw new Exception("unique constraint or key violated", sqlException);
            }

            if (sqlException.Number == CheckConstraintViolationNumber)
            {
                throw new Exception("Check constraint violation", sqlException);
            }

            if (sqlException.Number == InvalidColumnNumber)
            {
                throw new Exception(sqlException.Message, sqlException);
            }
        }

        #endregion
    }
}