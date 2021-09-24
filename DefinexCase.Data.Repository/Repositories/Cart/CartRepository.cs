using Dapper;
using DefinexCase.Common.Helper;
using DefinexCase.Data.Entity.Entity.Cart;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DefinexCase.Data.Repository.Repositories.Cart
{
    public class CartRepository : ICartRepository
    {
        public CartRepository(IOptions<SqlConnectionConfig> config)
        {
            _postgreConnectionString = config.Value.DatabaseConnectionString;
        }

        private readonly string _postgreConnectionString;
        private IEnumerable<CartEntity> cartEntities;
        private IEnumerable<CartItemEntity> cartItemEntities;
        private IDbConnection _postgreConnection { get { return new NpgsqlConnection(_postgreConnectionString); } }

        public bool AddCartItem(CartItemEntity data)
        {
            bool Checker = true;
            using (IDbConnection dbConnection = _postgreConnection)
            {
                dbConnection.Open();
                var param = new DynamicParameters();
                //param.Add(name: "time_sheet_line_id", value: data.time_sheet_line_id, direction: ParameterDirection.Input);
                //param.Add(name: "time_sheet_id", value: data.time_sheet_id, direction: ParameterDirection.Input);
                string query = @"INSERT INTO definexcasedb.cart_items( cart_id, product_id,product_name, quantity, total_price, unit_price) VALUES (1, :product_id,:product_name, :quantity, :total_price, :unit_price)";
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

        

        public IEnumerable<CartEntity> GetCartInfo()
        {
            using (IDbConnection dbConnection = _postgreConnection)
            {
                try
                {
                    if (dbConnection.State == ConnectionState.Closed)
                    {
                        dbConnection.Open();
                    }

                    string query = "select * from definexcasedb.carts";
                    cartEntities = dbConnection.Query<CartEntity>(query);

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
            return cartEntities;
        }

        public IEnumerable<CartItemEntity> GetCartItems()
        {
            using (IDbConnection dbConnection = _postgreConnection)
            {
                try
                {
                    if (dbConnection.State == ConnectionState.Closed)
                    {
                        dbConnection.Open();
                    }

                    string query = "select * from definexcasedb.cart_items";
                    cartItemEntities = dbConnection.Query<CartItemEntity>(query);

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
            return cartItemEntities;
        }

        public bool RemoveCartItem(int cart_id, int product_id)
        {
            bool Checker = true;
            using (IDbConnection dbConnection = _postgreConnection)
            {
                dbConnection.Open();
                var param = new DynamicParameters();
                param.Add(name: "product_id", value: product_id, direction: ParameterDirection.Input);
                param.Add(name: "cart_id", value: cart_id, direction: ParameterDirection.Input);

                string query = @"DELETE FROM definexcasedb.cart_items WHERE product_id = :product_id and cart_id = :cart_id";
                try
                {
                    dbConnection.Execute(query, param);

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

        public bool UpdateCartInfo(CartEntity data)
        {
            bool Checker = true;
            using (IDbConnection dbConnection = _postgreConnection)
            {
                dbConnection.Open();
               
                string query = @"UPDATE definexcasedb.carts SET total_price=:total_price, discount_type=:discount_type, discount_price=:discount_price where id=1";
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
        
        public bool UpdateCartItem(CartItemEntity data)
        {
            bool Checker = true;
            using (IDbConnection dbConnection = _postgreConnection)
            {
                dbConnection.Open();
                
                string query = @"UPDATE definexcasedb.cart_items SET cart_id =:cart_id, product_id=:product_id,product_name=:product_name, quantity=:quantity, total_price=:total_price, unit_price=:unit_price where product_id = :product_id";
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
    }
}
