using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class createAccount : System.Web.UI.Page
{
    SqlConnection sc = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCreateAccount_Click(object sender, EventArgs e)
    {
        SqlCommand customerInsert = new SqlCommand("INSERT INTO [dbo].[CUSTOMER] (EMAIL, PASSWORD, FIRSTNAME, LASTNAME) VALUES(@email, @password, @firstName, @lastName)", sc);
        customerInsert.Parameters.AddWithValue("@email", txtEmail.Text);
        customerInsert.Parameters.AddWithValue("@password", PasswordHash.HashPassword(txtPassword.Text));
        customerInsert.Parameters.AddWithValue("@firstName", txtFirstName.Text);
        customerInsert.Parameters.AddWithValue("@lastName", txtLastName.Text);

        try
        {
            sc.Open();
            customerInsert.ExecuteNonQuery();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoDatabaseAlertMessage", "alert('New Customer inserted')", true);

            Response.Redirect("index.aspx");
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoDatabaseAlertMessage", "alert('customer NOT Inserted')", true);
        }
        finally
        {
            sc.Close();
        }
    }
}