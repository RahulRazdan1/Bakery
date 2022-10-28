using Microsoft.AspNetCore.Mvc.Routing;
using System.Data;
using System.Net.Mail;
using System.Reflection.Emit;
using System;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;

namespace Bakery.Models
{
    public class Category
    {
        public Category()
        {
            IsActive = true;
        }

        #region properties, fields, enumerations
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        #endregion

        private void Set(DataRow dr)
        {
            if (dr == null)
                return;

            CategoryId = Convert.ToInt64(dr["CategoryId"]);
            if (dr["Name"] != DBNull.Value) Name = Convert.ToString(dr["Name"]);
            if (dr["Image"] != DBNull.Value) Image = Convert.ToString(dr["Image"]);
            if (dr["IsActive"] != DBNull.Value) IsActive = Convert.ToBoolean(dr["IsActive"]);
        }

        public static IList<Category> SelectList(string cnString, bool IsActive = true, bool IsHome = false)
        {
            using (SqlConnection cn = new SqlConnection(cnString))
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@IsActive", SqlDbType.Bit);
                sqlParams[0].Value = IsActive;
                sqlParams[1] = new SqlParameter("@IsHome", SqlDbType.Bit);
                sqlParams[1].Value = IsHome;

                DataSet ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "CategoryList_Select", sqlParams);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Category> categories = new List<Category>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Category c = new Category();
                        c.Set(dr);
                        categories.Add(c);
                    }
                    return categories;
                }
            }
            return null;
        }

    }
}
