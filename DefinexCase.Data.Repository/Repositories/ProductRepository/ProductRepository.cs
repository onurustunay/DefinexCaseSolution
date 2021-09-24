using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Npgsql;
using DefinexCase.Common.Helper;
using Microsoft.Extensions.Options;
using DefinexCase.Data.Entity.Entity.Product;

namespace DefinexCase.Data.Repository.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(IOptions<SqlConnectionConfig> config)
        {
            _postgreConnectionString = config.Value.DatabaseConnectionString;
        }

        private readonly string _postgreConnectionString;
        private IEnumerable<ProductEntity> productEntities;
        private IDbConnection _postgreConnection { get { return new NpgsqlConnection(_postgreConnectionString); } }

        
        public IEnumerable<ProductEntity> GetAllProducts()
        {
            using (IDbConnection dbConnection = _postgreConnection)
            {
                try
                {
                    if (dbConnection.State == ConnectionState.Closed)
                    {
                        dbConnection.Open();
                    }
                    
                    string query = "select * from definexcasedb.products";
                    productEntities = dbConnection.Query<ProductEntity>(query);

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (dbConnection.State != ConnectionState.Closed)
                    {
                        dbConnection.Close();

                        dbConnection.Dispose();
                    }
                }
            }
            return productEntities;
        }

        public IEnumerable<ProductEntity> GetProduct(int productId)
        {
            

            using (IDbConnection dbConnection = _postgreConnection)
            {
                try
                {
                    if (dbConnection.State == ConnectionState.Closed)
                    {
                        dbConnection.Open();
                    }
                    var param = new DynamicParameters();
                    param.Add(name: "productId", value: productId, direction: ParameterDirection.Input);
                    string query = "select * from definexcasedb.products where product_id = :productId";
                    productEntities = dbConnection.Query<ProductEntity>(query,param);

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (dbConnection.State != ConnectionState.Closed)
                    {
                        dbConnection.Close();

                        dbConnection.Dispose();
                    }
                }
            }
            return productEntities;
        }

        public bool CreateProduct(ProductEntity data)
        {
            bool Checker = true;
            using (IDbConnection dbConnection = _postgreConnection)
            {
                dbConnection.Open();
                var param = new DynamicParameters();
                //param.Add(name: "time_sheet_line_id", value: data.time_sheet_line_id, direction: ParameterDirection.Input);
                //param.Add(name: "time_sheet_id", value: data.time_sheet_id, direction: ParameterDirection.Input);
                string query = @"INSERT INTO definexcasedb.products( product_id, product_name, product_price, discount) VALUES (nextval('product_id_seq'), :product_name, :product_price, :discount)";
                try
                {
                    dbConnection.Execute(query, data);

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (dbConnection.State != ConnectionState.Closed)
                    {
                        dbConnection.Close();

                        dbConnection.Dispose();
                    }
                }
            }
            return Checker;
        }

        public bool UpdateProduct(ProductEntity data)
        {
            bool Checker = true;
            using (IDbConnection dbConnection = _postgreConnection)
            {
                dbConnection.Open();
                var param = new DynamicParameters();
                //param.Add(name: "time_sheet_line_id", value: data.time_sheet_line_id, direction: ParameterDirection.Input);
                //param.Add(name: "time_sheet_id", value: data.time_sheet_id, direction: ParameterDirection.Input);
                string query = @"UPDATE definexcasedb.products SET product_id=:product_id, product_name=:product_name, product_price=:product_price, discount=:discount WHERE product_id=:product_id";
                try
                {
                    dbConnection.Execute(query, data);

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (dbConnection.State != ConnectionState.Closed)
                    {
                        dbConnection.Close();

                        dbConnection.Dispose();
                    }
                }
            }
            return Checker;
        }

        public bool DeleteProduct(int productId)
        {
            bool Checker = true;
            using (IDbConnection dbConnection = _postgreConnection)
            {
                dbConnection.Open();
                var param = new DynamicParameters();
                param.Add(name: "productId", value: productId, direction: ParameterDirection.Input);
                
                string query = @"DELETE FROM definexcasedb.products WHERE product_id = :productId";
                try
                {
                    dbConnection.Execute(query,param);

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (dbConnection.State != ConnectionState.Closed)
                    {
                        dbConnection.Close();

                        dbConnection.Dispose();
                    }
                }
            }
            return Checker;
        }
    }
}