using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class index : System.Web.UI.Page
{

    SqlConnection sc = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);


    protected void Page_Load(object sender, EventArgs e)
    {
        Person.mealList.Clear();
        Person.calorieCounter = 0;
    }

    protected void btnCreateAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("createAccount.aspx");
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        // connect to database to retrieve stored password string
        try
        {
            sc.Open();
            System.Data.SqlClient.SqlCommand findPass = new System.Data.SqlClient.SqlCommand();
            findPass.Connection = sc;
            // SELECT PASSWORD STRING WHERE THE ENTERED USERNAME MATCHES
            findPass.CommandText = "select password from Customer where Email = @Email";
            findPass.Parameters.Add(new SqlParameter("@Email", txtEmail.Text));

            SqlDataReader reader = findPass.ExecuteReader(); // create a reader

            if (reader.HasRows) // if the username exists, it will continue
            {
                while (reader.Read()) // this will read the single record that matches the entered username
                {
                    string storedHash = reader["password"].ToString(); // store the database password into this variable

                    if (PasswordHash.ValidatePassword(txtPassword.Text, storedHash)) // if the entered password matches what is stored, it will show success
                    {
                        Session["Email"] = txtEmail.Text;
                        Response.Redirect("indexSignedIn.aspx");

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Password Error", "alert('Password is incorrect')", true);
                    }
                }
            }
            else // if the username doesn't exist, it will show failure
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoDatabaseAlertMessage", "alert('Email not found')", true);
            }

        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoDatabaseAlertMessage", "alert('error')", true);
        }
        finally
        {
            sc.Close();
        }
    }
}