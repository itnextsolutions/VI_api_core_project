using FizzWare.NBuilder.Dates;
using NHibernate.SqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VastraIndiaDAL
{
    public class ProductDAL
    {
        DataTable dt = new DataTable();
        SqlHelper objsqlHelper = new SqlHelper();
        //List<SqlParameter> SqlParameters = new List<SqlParameter>();


        public DataTable GetProdutCatDropDown()
        {
            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.Text, "SP_GetProductCatDropDown");
            return dt;
        }

        public DataTable GetProduct()
        {
            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.Text, "SP_GetProduct");
            return dt;
        }
        public DataTable GetProductById(int id)
        {

            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Product_Id";
            param[0].Value = id;
            param[0].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_GetProductById", param);
            return dt;
        }


        //public DataTable InsertProduct(int CategoryId, int SubCategory_Id, string ProductName, string Description, string Image_Name, string SidePhotoName, string BackPhotoName, string ColorId, string SizeId)
        //{
        //    SqlParameter[] param = new SqlParameter[9];

        //    param[0] = new SqlParameter();
        //    param[0].ParameterName = "@Category_Id";
        //    param[0].SqlDbType = SqlDbType.Int;
        //    param[0].Value = CategoryId;
        //    param[0].Direction = ParameterDirection.Input;

        //    param[1] = new SqlParameter();
        //    param[1].ParameterName = "@SubCategory_Id";
        //    param[1].SqlDbType = SqlDbType.Int;
        //    param[1].Value = SubCategory_Id;
        //    param[1].Direction = ParameterDirection.Input;

        //    param[2] = new SqlParameter();
        //    param[2].ParameterName = "@Product_Title";
        //    param[2].SqlDbType = SqlDbType.VarChar;
        //    param[2].Value = ProductName;
        //    param[2].Direction = ParameterDirection.Input;

        //    param[3] = new SqlParameter();
        //    param[3].ParameterName = "@Product_Photo";
        //    param[3].SqlDbType = SqlDbType.VarChar;
        //    param[3].Value = Image_Name;
        //    param[3].Direction = ParameterDirection.Input;

        //    param[4] = new SqlParameter();
        //    param[4].ParameterName = "@Product_SidePhoto";
        //    param[4].SqlDbType = SqlDbType.VarChar;
        //    param[4].Value = SidePhotoName;
        //    param[4].Direction = ParameterDirection.Input;

        //    param[5] = new SqlParameter();
        //    param[5].ParameterName = "@Product_BackPhoto";
        //    param[5].SqlDbType = SqlDbType.VarChar;
        //    param[5].Value = BackPhotoName;
        //    param[5].Direction = ParameterDirection.Input;

        //    param[6] = new SqlParameter();
        //    param[6].ParameterName = "@Description";
        //    param[6].SqlDbType = SqlDbType.VarChar;
        //    param[6].Value = Description;
        //    param[6].Direction = ParameterDirection.Input;

        //    param[7] = new SqlParameter();
        //    param[7].ParameterName = "@xmlcolor";
        //    param[7].SqlDbType = SqlDbType.Xml;
        //    param[7].Value = ColorId;
        //    param[7].Direction = ParameterDirection.Input;

        //    param[8] = new SqlParameter();
        //    param[8].ParameterName = "@xmlsize";
        //    param[8].SqlDbType = SqlDbType.Xml;
        //    param[8].Value = SizeId;
        //    param[8].Direction = ParameterDirection.Input;

        //    dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_InsertProducts", param);
        //    return dt;
        //}

        public DataTable InsertProduct(int CategoryId, int SubCategory_Id, string ProductName, string Description, string MenFrontImage, string MenSidePhotoName, string MenBackPhotoName, string MenSizeChartImage, String WomenDescription, string WomenFrontImage, String WomenSideImage, String WomenBackImage, string WomenSizeChartImage, string ColorId, string SizeId)
        {
            SqlParameter[] param = new SqlParameter[15];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Category_Id";
            param[0].SqlDbType = SqlDbType.Int;
            param[0].Value = CategoryId;
            param[0].Direction = ParameterDirection.Input;

            param[1] = new SqlParameter();
            param[1].ParameterName = "@SubCategory_Id";
            param[1].SqlDbType = SqlDbType.Int;
            param[1].Value = SubCategory_Id;
            param[1].Direction = ParameterDirection.Input;

            param[2] = new SqlParameter();
            param[2].ParameterName = "@Product_Title";
            param[2].SqlDbType = SqlDbType.VarChar;
            param[2].Value = ProductName;
            param[2].Direction = ParameterDirection.Input;

            param[3] = new SqlParameter();
            param[3].ParameterName = "@Product_Photo";
            param[3].SqlDbType = SqlDbType.VarChar;
            param[3].Value = MenFrontImage;
            param[3].Direction = ParameterDirection.Input;

            param[4] = new SqlParameter();
            param[4].ParameterName = "@Product_SidePhoto";
            param[4].SqlDbType = SqlDbType.VarChar;
            param[4].Value = MenSidePhotoName;
            param[4].Direction = ParameterDirection.Input;

            param[5] = new SqlParameter();
            param[5].ParameterName = "@Product_BackPhoto";
            param[5].SqlDbType = SqlDbType.VarChar;
            param[5].Value = MenBackPhotoName;
            param[5].Direction = ParameterDirection.Input;

            param[6] = new SqlParameter();
            param[6].ParameterName = "@Description";
            param[6].SqlDbType = SqlDbType.VarChar;
            param[6].Value = Description;
            param[6].Direction = ParameterDirection.Input;

            param[7] = new SqlParameter();
            param[7].ParameterName = "@SizeChartForMen";
            param[7].SqlDbType = SqlDbType.VarChar;
            param[7].Value = MenSizeChartImage;
            param[7].Direction = ParameterDirection.Input;

            param[8] = new SqlParameter();
            param[8].ParameterName = "@WomenProduct_Description";
            param[8].SqlDbType = SqlDbType.VarChar;
            param[8].Value = WomenDescription;
            param[8].Direction = ParameterDirection.Input;

            param[9] = new SqlParameter();
            param[9].ParameterName = "@WomenProduct_Photo";
            param[9].SqlDbType = SqlDbType.VarChar;
            param[9].Value = WomenFrontImage;
            param[9].Direction = ParameterDirection.Input;


            param[10] = new SqlParameter();
            param[10].ParameterName = "@WomenProduct_SidePhoto";
            param[10].SqlDbType = SqlDbType.VarChar;
            param[10].Value = WomenSideImage;
            param[10].Direction = ParameterDirection.Input;

            param[11] = new SqlParameter();
            param[11].ParameterName = "@WomenProduct_BackPhoto";
            param[11].SqlDbType = SqlDbType.VarChar;
            param[11].Value = WomenBackImage;
            param[11].Direction = ParameterDirection.Input;

            param[12] = new SqlParameter();
            param[12].ParameterName = "@SizeChartForWomen";
            param[12].SqlDbType = SqlDbType.VarChar;
            param[12].Value = WomenSizeChartImage;
            param[12].Direction = ParameterDirection.Input;

            param[13] = new SqlParameter();
            param[13].ParameterName = "@xmlcolor";
            param[13].SqlDbType = SqlDbType.Xml;
            param[13].Value = ColorId;
            param[13].Direction = ParameterDirection.Input;

            param[14] = new SqlParameter();
            param[14].ParameterName = "@xmlsize";
            param[14].SqlDbType = SqlDbType.Xml;
            param[14].Value = SizeId;
            param[14].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_InsertProducts", param);
            return dt;
        }


        //public DataTable UpdateProduct(int Product_Id, int CategoryId, int SubCategory_Id, string ProductName, string Description, string Image_Name, string SidePhotoName, string BackPhotoName, string ColorId, string SizeId)
        //{
        //    SqlParameter[] param = new SqlParameter[10];

        //    param[0] = new SqlParameter();
        //    param[0].ParameterName = "@Product_Id";
        //    param[0].Value = Product_Id;
        //    param[0].Direction = ParameterDirection.Input;

        //    param[1] = new SqlParameter();
        //    param[1].ParameterName = "@Category_Id";
        //    param[1].Value = CategoryId;
        //    param[1].Direction = ParameterDirection.Input;

        //    param[2] = new SqlParameter();
        //    param[2].ParameterName = "@SubCategory_Id";
        //    param[2].SqlDbType = SqlDbType.Int;
        //    param[2].Value = SubCategory_Id;
        //    param[2].Direction = ParameterDirection.Input;


        //    param[3] = new SqlParameter();
        //    param[3].ParameterName = "@Product_Title";
        //    param[3].Value = ProductName;
        //    param[3].Direction = ParameterDirection.Input;

        //    param[4] = new SqlParameter();
        //    param[4].ParameterName = "@Description";
        //    param[4].SqlDbType = SqlDbType.VarChar;
        //    param[4].Value = Description;
        //    param[4].Direction = ParameterDirection.Input;

        //    param[5] = new SqlParameter();
        //    param[5].ParameterName = "@Product_Photo";
        //    param[5].SqlDbType = SqlDbType.VarChar;
        //    param[5].Value = Image_Name;
        //    param[5].Direction = ParameterDirection.Input;

        //    param[6] = new SqlParameter();
        //    param[6].ParameterName = "@Product_SidePhoto";
        //    param[6].SqlDbType = SqlDbType.VarChar;
        //    param[6].Value = SidePhotoName;
        //    param[6].Direction = ParameterDirection.Input;

        //    param[7] = new SqlParameter();
        //    param[7].ParameterName = "@Product_BackPhoto";
        //    param[7].SqlDbType = SqlDbType.VarChar;
        //    param[7].Value = BackPhotoName;
        //    param[7].Direction = ParameterDirection.Input;

        //    param[8] = new SqlParameter();
        //    param[8].ParameterName = "@xmlcolor";
        //    param[8].SqlDbType = SqlDbType.Xml;
        //    param[8].Value = ColorId;
        //    param[8].Direction = ParameterDirection.Input;

        //    param[9] = new SqlParameter();
        //    param[9].ParameterName = "@xmlsize";
        //    param[9].SqlDbType = SqlDbType.Xml;
        //    param[9].Value = SizeId;
        //    param[9].Direction = ParameterDirection.Input;


        //    dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_UpdateProducts", param);
        //    return dt;
        //}

        public DataTable UpdateProduct(int Product_Id, int CategoryId, int SubCategory_Id, string ProductName, string Description, string MenFrontImage, string MenSidePhotoName, string MenBackPhotoName, string MenSizeChartImage, String WomenDescription, string WomenFrontImage, String WomenSideImage, String WomenBackImage, string WomenSizeChartImage, string ColorId, string SizeId)
        {
            SqlParameter[] param = new SqlParameter[16];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Product_Id";
            param[0].Value = Product_Id;
            param[0].Direction = ParameterDirection.Input;

            param[1] = new SqlParameter();
            param[1].ParameterName = "@Category_Id";
            param[1].Value = CategoryId;
            param[1].Direction = ParameterDirection.Input;

            param[2] = new SqlParameter();
            param[2].ParameterName = "@SubCategory_Id";
            param[2].SqlDbType = SqlDbType.Int;
            param[2].Value = SubCategory_Id;
            param[2].Direction = ParameterDirection.Input;


            param[3] = new SqlParameter();
            param[3].ParameterName = "@Product_Title";
            param[3].Value = ProductName;
            param[3].Direction = ParameterDirection.Input;

            param[4] = new SqlParameter();
            param[4].ParameterName = "@Description";
            param[4].SqlDbType = SqlDbType.VarChar;
            param[4].Value = Description;
            param[4].Direction = ParameterDirection.Input;

            param[5] = new SqlParameter();
            param[5].ParameterName = "@Product_Photo";
            param[5].SqlDbType = SqlDbType.VarChar;
            param[5].Value = MenFrontImage;
            param[5].Direction = ParameterDirection.Input;

            param[6] = new SqlParameter();
            param[6].ParameterName = "@Product_SidePhoto";
            param[6].SqlDbType = SqlDbType.VarChar;
            param[6].Value = MenSidePhotoName;
            param[6].Direction = ParameterDirection.Input;

            param[7] = new SqlParameter();
            param[7].ParameterName = "@Product_BackPhoto";
            param[7].SqlDbType = SqlDbType.VarChar;
            param[7].Value = MenBackPhotoName;
            param[7].Direction = ParameterDirection.Input;

            param[8] = new SqlParameter();
            param[8].ParameterName = "@SizeChartForMen";
            param[8].SqlDbType = SqlDbType.VarChar;
            param[8].Value = MenSizeChartImage;
            param[8].Direction = ParameterDirection.Input;

            param[9] = new SqlParameter();
            param[9].ParameterName = "@WomenProduct_Description";
            param[9].SqlDbType = SqlDbType.VarChar;
            param[9].Value = WomenDescription;
            param[9].Direction = ParameterDirection.Input;

            param[10] = new SqlParameter();
            param[10].ParameterName = "@WomenProduct_Photo";
            param[10].SqlDbType = SqlDbType.VarChar;
            param[10].Value = WomenFrontImage;
            param[10].Direction = ParameterDirection.Input;


            param[11] = new SqlParameter();
            param[11].ParameterName = "@WomenProduct_SidePhoto";
            param[11].SqlDbType = SqlDbType.VarChar;
            param[11].Value = WomenSideImage;
            param[11].Direction = ParameterDirection.Input;

            param[12] = new SqlParameter();
            param[12].ParameterName = "@WomenProduct_BackPhoto";
            param[12].SqlDbType = SqlDbType.VarChar;
            param[12].Value = WomenBackImage;
            param[12].Direction = ParameterDirection.Input;

            param[13] = new SqlParameter();
            param[13].ParameterName = "@SizeChartForWomen";
            param[13].SqlDbType = SqlDbType.VarChar;
            param[13].Value = WomenSizeChartImage;
            param[13].Direction = ParameterDirection.Input;


            param[14] = new SqlParameter();
            param[14].ParameterName = "@xmlcolor";
            param[14].SqlDbType = SqlDbType.Xml;
            param[14].Value = ColorId;
            param[14].Direction = ParameterDirection.Input;

            param[15] = new SqlParameter();
            param[15].ParameterName = "@xmlsize";
            param[15].SqlDbType = SqlDbType.Xml;
            param[15].Value = SizeId;
            param[15].Direction = ParameterDirection.Input;


            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_UpdateProducts", param);
            return dt;
        }

        public DataTable DeleteProduct(int Product_Id)
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Product_Id";
            param[0].Value = Product_Id;
            param[0].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_DeleteProductById", param);
            return dt;
        }

        public DataTable GetProductCategory()
        {
            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.Text, "SP_GetProductCategory");
            return dt;
        }
        public DataTable GetProductCategoryById(int id)
        {

            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Category_Id";
            param[0].Value = id;
            param[0].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_GetProductCategoryById", param);
            return dt;
        }

        public DataTable InsertCategory(string Category_Name, string Category_Photo, string Category_Description)
        {
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Category_Name";
            param[0].Value = Category_Name;
            param[0].Direction = ParameterDirection.Input;

            param[1] = new SqlParameter();
            param[1].ParameterName = "@Category_Description";
            param[1].Value = Category_Description;
            param[1].Direction = ParameterDirection.Input;


            param[2] = new SqlParameter();
            param[2].ParameterName = "@Category_Photo";
            param[2].Value = Category_Photo;
            param[2].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_InsertProductCategory", param);
            return dt;
        }

        public DataTable UpdateCategory(int CategoryId, string Category_Name, string CategoryPhoto, string Category_Description)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter();
            param[0].ParameterName = "@Category_Id";
            param[0].Value = CategoryId;
            param[0].Direction = ParameterDirection.Input;

            param[1] = new SqlParameter();
            param[1].ParameterName = "@Category_Name";
            param[1].Value = Category_Name;
            param[1].Direction = ParameterDirection.Input;

            param[2] = new SqlParameter();
            param[2].ParameterName = "@Category_Description";
            param[2].Value = Category_Description;
            param[2].Direction = ParameterDirection.Input;


            param[3] = new SqlParameter();
            param[3].ParameterName = "@Category_Photo";
            param[3].Value = CategoryPhoto;
            param[3].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_UpdateProductCategory", param);
            return dt;
        }
        public DataTable DeleteCategory(int CategoryId)
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Category_Id";
            param[0].Value = CategoryId;
            param[0].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_DeleteProductCategoryById", param);
            return dt;
        }

        public DataTable GetProductColor()
        {
            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.Text, "SP_GetProductColor");
            return dt;
        }
        public DataTable GetProductColorById(int id)
        {

            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Product_Color_Id";
            param[0].Value = id;
            param[0].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_GetProductColorById", param);
            return dt;
        }


        public DataTable InsertProdctColor(int Product_Id, int Color_Code)
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Product_Id";
            param[0].Value = Product_Id;
            param[0].Direction = ParameterDirection.Input;

            param[1] = new SqlParameter();
            param[1].ParameterName = "@Color_Code";
            param[1].Value = Color_Code;
            param[1].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_InsertProdctColor", param);
            return dt;
        }


        public DataTable UpdateProdctColor(int Product_Color_Id, int Product_Id, int Color_Code)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter();
            param[0].ParameterName = "@Product_Color_Id";
            param[0].Value = Product_Color_Id;
            param[0].Direction = ParameterDirection.Input;

            param[1] = new SqlParameter();
            param[1].ParameterName = "@Product_Id";
            param[1].Value = Product_Id;
            param[1].Direction = ParameterDirection.Input;

            param[2] = new SqlParameter();
            param[2].ParameterName = "@Color_Code";
            param[2].Value = Color_Code;
            param[2].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_UpdateProductColor", param);
            return dt;
        }

        public DataTable DeleteProdctColor(int Product_Color_Id)
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Product_Color_Id";
            param[0].Value = Product_Color_Id;
            param[0].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_DeleteProductColorById", param);
            return dt;
        }

        public DataTable ProductSize()
        {
            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.Text, "SP_GetProductSize");
            return dt;
        }

        public DataTable GetProductSizeById(int id)
        {

            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Product_Size_Id";
            param[0].Value = id;
            param[0].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_GetProductSizeById", param);
            return dt;
        }

        public DataTable InsertProductSize(int Product_Id, string Size)
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Product_Id ";
            param[0].Value = Product_Id;
            param[0].Direction = ParameterDirection.Input;

            param[1] = new SqlParameter();
            param[1].ParameterName = "@Size";
            param[1].Value = Size;
            param[1].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_InsertProdctSize", param);
            return dt;
        }

        public DataTable UpdateProductSize(int ProductSizeId, int Product_Id, string Size)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter();
            param[0].ParameterName = "@Product_Size_Id";
            param[0].Value = ProductSizeId;
            param[0].Direction = ParameterDirection.Input;

            param[1] = new SqlParameter();
            param[1].ParameterName = "@Product_Id";
            param[1].Value = Product_Id;
            param[1].Direction = ParameterDirection.Input;

            param[2] = new SqlParameter();
            param[2].ParameterName = "@Size";
            param[2].Value = Size;
            param[2].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_UpdateProductSize", param);
            return dt;
        }

        public DataTable DeleteProductSize(int ProductSizeId)
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Product_Size_Id";
            param[0].Value = ProductSizeId;
            param[0].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_DeleteProductSizeById", param);
            return dt;
        }

        public DataTable GetSubProductCategory()
        {
            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.Text, "SP_GetSubProductCategory");
            return dt;
        }
        public DataTable GetSubCategory()
        {
            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.Text, "SP_GetSubCategory");
            return dt;
        }

        public DataTable GetSubProductCategoryById(int id)
        {

            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Id";
            param[0].Value = id;
            param[0].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_GetSubProductCategoryById", param);
            return dt;
        }



        public DataTable InsertSubCategory(int CategoryId, string Sub_Cat_Name /*string Sub_Cat_Photo*/)
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Category_Id";
            param[0].Value = CategoryId;
            param[0].Direction = ParameterDirection.Input;

            param[1] = new SqlParameter();
            param[1].ParameterName = "@Sub_Cat_Name";
            param[1].Value = Sub_Cat_Name;
            param[1].Direction = ParameterDirection.Input;


            //param[2] = new SqlParameter();
            //param[2].ParameterName = "@Sub_Cat_Photo";
            //param[2].Value = Sub_Cat_Photo;
            //param[2].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_InsertSubProductCategory", param);
            return dt;
        }


        public DataTable UpdateSubCategory(int Id, int CategoryId, string Sub_Cat_Name /*string Sub_Cat_Photo*/)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter();
            param[0].ParameterName = "@Id";
            param[0].Value = Id;
            param[0].Direction = ParameterDirection.Input;

            param[1] = new SqlParameter();
            param[1].ParameterName = "@Category_Id";
            param[1].Value = CategoryId;
            param[1].Direction = ParameterDirection.Input;

            param[2] = new SqlParameter();
            param[2].ParameterName = "@Sub_Cat_Name";
            param[2].Value = Sub_Cat_Name;
            param[2].Direction = ParameterDirection.Input;


            //param[3] = new SqlParameter();
            //param[3].ParameterName = "@Sub_Cat_Photo";
            //param[3].Value = Sub_Cat_Photo;
            //param[3].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_UpdateSubProductCategory", param);
            return dt;
        }

        public DataTable DeleteSubCategory(int Id)
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Id";
            param[0].Value = Id;
            param[0].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_DeleteSubProductCategoryById", param);
            return dt;
        }


        public DataTable GetSubCatByCatid(int id)
        {

            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Category_Id";
            param[0].Value = id;
            param[0].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_GetSubCatByCatId", param);
            return dt;
        }
        public DataTable GetColorCodeListByProductId(int id)
        {

            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@Product_Id";
            param[0].Value = id;
            param[0].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_GetColorCodeListByProductId", param);
            return dt;
        }
        public DataTable GetProductCategoryPagination(int pageNo, int pageSize)
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@PageNo";
            param[0].Value = pageNo;
            param[0].Direction = ParameterDirection.Input;

            param[1] = new SqlParameter();
            param[1].ParameterName = "@PageSize";
            param[1].Value = pageSize;
            param[1].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_GetProductCategoryPagination", param);
            return dt;
        }

        public DataTable GetProductCategoryCount()
        {
            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_GetProductCategoryCount");
            return dt;
        }



        public DataTable SubCategoryPagination(int pageNo, int pageSize)
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@PageNo";
            param[0].Value = pageNo;
            param[0].Direction = ParameterDirection.Input;

            param[1] = new SqlParameter();
            param[1].ParameterName = "@PageSize";
            param[1].Value = pageSize;
            param[1].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_GetSubCategoryPagination", param);
            return dt;
        }

        public DataTable SubCategoryCount()
        {
            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_GetSubCategoryCount");
            return dt;
        }




        public DataTable GetProductPagination(int pageNo, int pageSize)
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter();
            param[0].ParameterName = "@PageNo";
            param[0].Value = pageNo;
            param[0].Direction = ParameterDirection.Input;

            param[1] = new SqlParameter();
            param[1].ParameterName = "@PageSize";
            param[1].Value = pageSize;
            param[1].Direction = ParameterDirection.Input;

            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_GetProductPagination", param);
            return dt;
        }

        public DataTable GetProductCount()
        {
            dt = objsqlHelper.ExecuteDataTable(objsqlHelper.GetConnection(), CommandType.StoredProcedure, "SP_GetProductCount");
            return dt;
        }
    }
}
