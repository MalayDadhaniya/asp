using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Crud_Operation
{
    
    public partial class index : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@" /* connection */");
        SqlCommand cmd = new SqlCommand();

        protected void Page_Load(object sender, EventArgs e)
        {
            Print();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Insert code

            if(Button1.Text == "save")
            {
                cmd = new SqlCommand("/* Insert SQL code */", con);

                //  part of add parameters = cmd.Parameters.AddWithValue();

                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                if(res == 1)
                {
                    //Clear selection control
                    TextBox1.Text = "";
                    RadioButtonList1.ClearSelection();
                }
                Print();
            }
            if(Button1.Text == "Update")
            {
                cmd = new SqlCommand("/* Update SQL code */", con);

                //  part of add parameters = cmd.Parameters.AddWithValue();

                // add only in this

                cmd.Parameters.AddWithValue("id", ViewState["id"]);

                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                if (res == 1)
                {
                    //Clear selection control
                    TextBox1.Text = "";
                    RadioButtonList1.ClearSelection();
                    Button1.Text = "save";
                }
                Print();
            }
        }

        public void Print()
        {
            SqlDataAdapter adpt = new SqlDataAdapter("/* Select Query */", con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            //GridView1.DataSource(dt);
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            cmd = new SqlCommand("/* DElete Qurey  +btn.CommendArgument  */",con);
            // cmd.Parameters.AddWithValue();
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Print();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            SqlDataAdapter adpt = new SqlDataAdapter("/*Select Query with where +btn.CommendArgument*/ ", con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            TextBox1.Text = dt.Rows[0][1].ToString();
            RadioButtonList1.Text = dt.Rows[0][2].ToString();
            ViewState["id"] = btn.CommandArgument;
            Button1.Text = " Update";
        }
    }
}