using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Bakery.Models
{
    public class Product
    {
        public Product()
        {
            IsActive = true;
        }

        #region properties, fields, enumerations
        public long ProductId { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsActive { get; set; }
        public int Quantity { get; set; }
        #endregion

        private void Set(DataRow dr)
        {
            if (dr == null)
                return;

            ProductId = Convert.ToInt64(dr["ProductId"]);
            if (dr["CategoryId"] != DBNull.Value) CategoryId = Convert.ToInt64(dr["CategoryId"]);
            if (dr["Name"] != DBNull.Value) Name = Convert.ToString(dr["Name"]);
            if (dr["Description"] != DBNull.Value) Description = Convert.ToString(dr["Description"]);
            if (dr["Image"] != DBNull.Value) Image = Convert.ToString(dr["Image"]);
            if (dr["Price"] != DBNull.Value) Price = Convert.ToDecimal(dr["Price"]);
            if (dr["IsGlutenFree"] != DBNull.Value) IsGlutenFree = Convert.ToBoolean(dr["IsGlutenFree"]);
            if (dr["IsActive"] != DBNull.Value) IsActive = Convert.ToBoolean(dr["IsActive"]);
        }

        public static IList<Product> SelectList(string cnString, long CategoryId, bool IsActive = true, bool IsHome = false)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection cn = new SqlConnection(cnString))
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@CategoryId", SqlDbType.BigInt);
                sqlParams[0].Value = CategoryId;
                sqlParams[1] = new SqlParameter("@IsActive", SqlDbType.Bit);
                sqlParams[1].Value = IsActive;
                sqlParams[2] = new SqlParameter("@IsHome", SqlDbType.Bit);
                sqlParams[2].Value = IsHome;

                DataSet ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "ProductList_Select", sqlParams);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {                    
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Product p = new Product();
                        p.Set(dr);
                        products.Add(p);
                    }                    
                }
            }
            return products;
        }

        public static Product Select(string cnString, long ProductId)
        {
            Product product = new Product();
            using (SqlConnection cn = new SqlConnection(cnString))
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@ProductId", SqlDbType.BigInt);
                sqlParams[0].Value = ProductId;

                
                DataSet ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "Product_Select", sqlParams);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {                    
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        product.Set(dr);
                    }
                }
            }
            return product;
        }
    }
}
