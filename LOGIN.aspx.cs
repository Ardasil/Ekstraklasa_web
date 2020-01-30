using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Ekstra_web
{
    public partial class LOGIN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-L3NRFTC;Initial Catalog=Ekstraklasa;Integrated Security=True;"))
            {
                con.Open();
                string query = "SELECT Count(1) FROM Login WHERE LOGIN=@login AND PASSWORD=@pass";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@login", txtLogin.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@pass", txtPass.Text.Trim());
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    Session["login"] = txtLogin.Text.Trim();
                    Response.Redirect("DASHBOARD.aspx");
                }
                else { lblError.Visible = true; }
            }
        }
    }
}