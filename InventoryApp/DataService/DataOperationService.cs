using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using InventoryApp.Models;

namespace InventoryApp.DataService
{
    public class DataOperationService
    {
        public async Task<List<Product>> GetProductsAsync(int PageNo, int RowCountPerPage, int IsPaging)
        {
            DBConnector dBConnector = new DBConnector();
            SqlConnection sqlConnection = dBConnector.GetConnection;
            sqlConnection.Open();

            try
            {
                List<Product> productList = new List<Product>();
                if (sqlConnection.State != System.Data.ConnectionState.Open)
                    sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("SP_READ_PRODUCT", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@PageNo", PageNo);
                sqlCommand.Parameters.AddWithValue("@RowCountPerPage", RowCountPerPage);
                sqlCommand.Parameters.AddWithValue("@IsPaging", IsPaging);
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync().ConfigureAwait(false);

                while (sqlDataReader.Read())
                {
                    Product product = new Product();
                    product.PID = Convert.ToInt32(sqlDataReader["PID"]);
                    product.PName = sqlDataReader["PName"].ToString();
                    product.PDescription = sqlDataReader["PDescription"].ToString();
                    product.PQuantity = Convert.ToInt32(sqlDataReader["PQty"]);
                    product.PPrice = Convert.ToDecimal(sqlDataReader["PPrice"]);
                    product.DateAdded = Convert.ToDateTime(sqlDataReader["DateAdded"]);
                    product.DateUpdated = Convert.ToDateTime(sqlDataReader["DateUpdated"]);
                    productList.Add(product);
                }
                return productList;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Open)
                    {
                        sqlConnection.Close();
                        sqlConnection.Dispose();
                    }
                }
            }
        }

        public async Task<Product> GetProductAsync(int Id)
        {
            DBConnector dBConnector = new DBConnector();
            SqlConnection sqlConnection = dBConnector.GetConnection;
            sqlConnection.Open();

            try
            {
                Product product = new Product();

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                    sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("SP_VIEW_PRODUCT", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@PID", Id);
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync().ConfigureAwait(false);

                while (sqlDataReader.Read())
                {
                    product.PID = Convert.ToInt32(sqlDataReader["PID"]);
                    product.PName = sqlDataReader["PName"].ToString();
                    product.PDescription = sqlDataReader["PDescription"].ToString();
                    product.PQuantity = Convert.ToInt32(sqlDataReader["PQty"]);
                    product.PPrice = Convert.ToDecimal(sqlDataReader["PPrice"]);
                    product.DateAdded = Convert.ToDateTime(sqlDataReader["DateAdded"]);
                    product.DateUpdated = Convert.ToDateTime(sqlDataReader["DateUpdated"]);
                }
                return product;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }
        }

        public async Task<int> InsertProductAsync(Product product)
        {
            DBConnector dBConnector = new DBConnector();
            SqlConnection sqlConnection = dBConnector.GetConnection;
            sqlConnection.Open();

            int result = 0;
            try
            {
                if (sqlConnection.State != System.Data.ConnectionState.Open)
                    sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("SP_CREATE_PRODUCT", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@PName", product.PName);
                sqlCommand.Parameters.AddWithValue("@PDescription", product.PDescription);
                sqlCommand.Parameters.AddWithValue("@PQty", product.PQuantity);
                sqlCommand.Parameters.AddWithValue("@PPrice", product.PPrice);
                object resultObject = await sqlCommand.ExecuteScalarAsync().ConfigureAwait(false);
                result = Convert.ToInt32(resultObject);
                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Open)
                    {
                        sqlConnection.Close();
                        sqlConnection.Dispose();
                    }
                }
            }
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            DBConnector dBConnector = new DBConnector();
            SqlConnection sqlConnection = dBConnector.GetConnection;
            sqlConnection.Open();

            int result = 0;
            try
            {
                if (sqlConnection.State != System.Data.ConnectionState.Open)
                    sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("SP_UPDATE_PRODUCT", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@PID", product.PID);
                sqlCommand.Parameters.AddWithValue("@PName", product.PName);
                sqlCommand.Parameters.AddWithValue("@PDescription", product.PDescription);
                sqlCommand.Parameters.AddWithValue("@PQty", product.PQuantity);
                sqlCommand.Parameters.AddWithValue("@PPrice", product.PPrice);
                object resultObject = await sqlCommand.ExecuteScalarAsync().ConfigureAwait(false);
                result = Convert.ToInt32(resultObject);
                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Open)
                    {
                        sqlConnection.Close();
                        sqlConnection.Dispose();
                    }
                }
            }
        }

        public async Task<int> DeleteProductAsync(int Id)
        {
            DBConnector dBConnector = new DBConnector();
            SqlConnection sqlConnection = dBConnector.GetConnection;
            sqlConnection.Open();

            int result = 0;
            try
            {
                if (sqlConnection.State != System.Data.ConnectionState.Open)
                    sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("SP_DELETE_PRODUCT", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@PID", Id);
                object resultObject = await sqlCommand.ExecuteScalarAsync().ConfigureAwait(false);
                result = Convert.ToInt32(resultObject);
                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Open)
                    {
                        sqlConnection.Close();
                        sqlConnection.Dispose();
                    }
                }
            }
        }
    }
}