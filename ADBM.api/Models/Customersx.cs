using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;


namespace ADBM.api.Models
{
    public class Customers
    {
        public Customer() { }

        public Customer(
            int _Ident,
            string _EDIPI,
            string _Name,
            string _Location,
            string _Description,
            string _Phone
            )
        { }

        public int _Ident { get; set; }
        public string _EDIPI { get; set; }
        public string _Name { get; set; }
        public string _Location { get; set; }
        public string _Description { get; set; }
        public string _Phone { get; set; }

        public static Customer GetCustomer(SqlDataReader reader)
        {
            Customer customer = new Customer();
            if (reader.IsClosed)
                reader.Read();

            customer._Ident = (reader["ident"] != DBNull.Value ? Convert.ToInt32(reader["ident"]) : 0);
            customer._EDIPI = (reader["EDIPI"] != DBNull.Value ? reader["EDIPI"].ToString() : "");
            customer._Name = (reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "");
            customer._Location = (reader["Location"] != DBNull.Value ? reader["Location"].ToString() : "");
            customer._Description = (reader["Description"] != DBNull.Value ? reader["Description"].ToString() : "");
            customer._Phone = (reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : "");

            return customer;
        }


        OdbcConnection conn = new OdbcConnection("DSN=ADBMSamp");
        conn.Open();
			OdbcCommand cmdCustomer = new OdbcCommand("select * from CUSTOMER", conn);
        OdbcDataReader dr;
        dr = cmdCustomer.ExecuteReader();
			DataGrid1.DataSource = dr;
			DataGrid1.DataBind();
			dr.Close();
			conn.Close();
    }

    public class CustomerCollection : List<Customer>
    {
    }

    public class Customers
    {
        public static CustomersCollection GetAllCustomers()
        {
            CustomersCollection ac = new CustomersCollection();
            using (SqlConnection conn = new SqlConnection(PhoneSQL.csPhone))
            {
                SqlCommand cmd = new SqlCommand(PhoneSQL.cGetAllPhones, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                FillPhoneList(ac, reader);
                reader.Close();
                conn.Close();

                return ac;
            }
        }

        public static void FillCustomerList(CustomersCollection coll, SqlDataReader reader)
        {
            FillCustomerList(coll, reader, -1, 0);
        }

        public static void FillCustomerList(CustomersCollection coll, SqlDataReader reader, int totalRows, int firstRow)
        {
            int index = 0;
            bool readMore = true;

            while (reader.Read())
            {
                if (index >= firstRow && readMore)
                {
                    if (coll.Count >= totalRows && totalRows > 0)
                        readMore = false;
                    else
                    {
                        Customer trx = Customer.GetCustomer(reader);
                        coll.Add(trx);
                    }
                }
                index++;
            }
        }

        public static Int32 InsertCustomer(Customer p)
        {
            int NewCustomerId;
            NewCustomerId = InsertCustomer(
                p._EDIPI
                , p._Name
                , p._Location
                , p._Description
                , p._Phone
                );
            return NewCustomerId;
        }

        public static Int32 InsertCustomer(
            string _EDIPI
            , string _Name
            , string _Location
            , string _Description
            , string _Phone
            )
        {
            using (SqlConnection conn = new SqlConnection(CustomerSQL.csCustomer))
            {
                int NewCustomerId;
                SqlCommand cmd = new SqlCommand(PhoneSQL.cInsert, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@psEDIPI", _EDIPI);
                cmd.Parameters.AddWithValue("@psName", _Name);
                cmd.Parameters.AddWithValue("@psLocation", _Location);
                cmd.Parameters.AddWithValue("@psDescription", _Description);
                cmd.Parameters.AddWithValue("@psPhone", _Phone);
                conn.Open();
                NewCustomerId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return NewCustomerId;
            }
        }

        public static Customer GetCustomer(int pid)
        {
            using (SqlConnection conn = new SqlConnection(PhoneSQL.csPhone))
            {
                SqlCommand cmd = new SqlCommand(CustomerSQL.cGetCustomer, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ident", pid);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                Customer px = Customer.GetCustomer(reader);
                reader.Close();
                conn.Close();
                return px;
            }
        }

        public static void UpdateCustomer(Customer p)
        {
            UpdateCustomer(
                p._Ident
                , p._EDIPI
                , p._Name
                , p._Location
                , p._Description
                , p._Phone
                );
        }

        public static void UpdateCustomer(
            int _Ident
            , string _EDIPI
            , string _Name
            , string _Location
            , string _Description
            , string _Phone
            )
        {
            using (SqlConnection conn = new SqlConnection(PhoneSQL.csPhone))
            {
                SqlCommand cmd = new SqlCommand(CustomerSQL.cUpdate, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@piident", _Ident);
                cmd.Parameters.AddWithValue("@psEDIPI", _EDIPI);
                cmd.Parameters.AddWithValue("@psName", _Name);
                cmd.Parameters.AddWithValue("@psLocation", _Location);
                cmd.Parameters.AddWithValue("@psDescription", _Description);
                cmd.Parameters.AddWithValue("@psPhone", _Phone);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return;
            }
        }

        public static void DeleteCustomer(Customer p)
        {
            DeleteCustomer(p._Ident);
        }

        public static void DeleteCustomer(int pid)
        {
            using (SqlConnection conn = new SqlConnection(PhoneSQL.csPhone))
            {
                SqlCommand cmd = new SqlCommand(CustomerSQL.cDelete, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@piident", pid);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return;
            }
        }

    }
    public class CustomerSQL
    {
        public static string csPhone = System.Configuration.ConfigurationManager.ConnectionStrings["Customers"].ToString();
        public static string cInsert = "dbo.InsertPhone";
        public static string cGetPhone = "dbo.GetPhone";
        public static string cGetPhoneNumber = "dbo.GetPhoneNumber";
        public static string cGetMyPhones = "dbo.GetMyPhones";
        public static string cGetAllPhones = "dbo.GetAllPhones";
        public static string cGetResponderCount = "dbo.GetResponderCount";
        public static string cUpdate = "dbo.UpdatePhone";
        public static string cDelete = "dbo.DeletePhone";
    }
}