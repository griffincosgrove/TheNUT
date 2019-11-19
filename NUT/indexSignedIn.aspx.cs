using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;

public partial class indexSignedIn : System.Web.UI.Page
{
    SqlConnection sc = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);


    protected void Page_Load(object sender, EventArgs e)
    {
        lblEmail.Text = Session["Email"].ToString();
    }

    protected void btnAddFoodItem_Click(object sender, EventArgs e)
    {

        string foodItemSelected = dropDownFood.SelectedValue;

        Person.mealList.Add(dropDownFood.SelectedValue);

        SqlCommand searchFood = new SqlCommand("Select menuItem, calories from fooditem where menuitem = @menuitem", sc);
        searchFood.Parameters.AddWithValue("@menuitem", foodItemSelected);

        SqlCommand insertFoodConsumed = new SqlCommand("INSERT INTO FOODCONSUMED VALUES (@email, @food, @calories)", sc);
        insertFoodConsumed.Parameters.AddWithValue("@email", Session["Email"].ToString());
        insertFoodConsumed.Parameters.AddWithValue("@food", dropDownFood.SelectedValue);
        //insertFoodConsumed.Parameters.AddWithValue("@calories", Person.calorieCounter);

        sc.Open();

        SqlDataReader reader = searchFood.ExecuteReader();

        if(reader.HasRows)
        {
            while (reader.Read())
            {
                Person.calorieCounter += Convert.ToInt32(reader["calories"]);
                insertFoodConsumed.Parameters.AddWithValue("@calories", Convert.ToInt32(reader["calories"]));
            }
            sc.Close();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "alert('Error')", true);
        }
        sc.Open();
        insertFoodConsumed.ExecuteNonQuery();
        sc.Close();
    }

    protected void btnViewConsumed_Click(object sender, EventArgs e)
    {
        GridViewFood.DataBind();
        GridViewTotalCalories.DataBind();
        GridViewFood.Visible = true;
        GridViewTotalCalories.Visible = true;
    }

}