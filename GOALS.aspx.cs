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
    public partial class GOALS : System.Web.UI.Page
    {
        string connectionString = @"Data Source=DESKTOP-L3NRFTC;Initial Catalog = Ekstraklasa; Integrated Security = True;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
                Response.Redirect("LOGIN.aspx");
            Label1.Text = "Zalogowano jako: " + Session["login"];

            if (!IsPostBack)
            {
                PopulateGridView();
            }
        }

        void PopulateGridView()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM BRAMKI", connectionString);
                sqlda.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                ekBramki.DataSource = dtbl;
                ekBramki.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                ekBramki.DataSource = dtbl;
                ekBramki.DataBind();
                ekBramki.Rows[0].Cells.Clear();
                ekBramki.Rows[0].Cells.Add(new TableCell());
                ekBramki.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                ekBramki.Rows[0].Cells[0].Text = "Nie znaleziono żadnych rekordów!";
                ekBramki.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void ekBramki_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO BRAMKI (Id_gospo,Id_gosc,Id_pilkarze,PIL_Id_pilkarze,Czas_bramki) VALUES (@Id_gospo,@Id_gosc,@Id_pilkarze,@PIL_Id_pilkarze,@Czas_bramki)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@Id_gospo", System.Convert.ToInt32((ekBramki.FooterRow.FindControl("txtIDGospFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Id_gosc", System.Convert.ToInt32((ekBramki.FooterRow.FindControl("txtIDGoscFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Id_pilkarze", System.Convert.ToInt32((ekBramki.FooterRow.FindControl("txtStrzelecFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@PIL_Id_pilkarze", System.Convert.ToInt32((ekBramki.FooterRow.FindControl("txtBramkarzFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Czas_bramki", TimeSpan.Parse((ekBramki.FooterRow.FindControl("txtCzasFooter") as TextBox).Text.Trim()));
                        sqlCmd.ExecuteNonQuery();
                        PopulateGridView();
                        lblSucces.Text = "Dodano nowy rekord.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                throw;
            }
        }

        protected void ekBramki_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ekBramki.EditIndex = e.NewEditIndex;
            PopulateGridView();
        }

        protected void ekBramki_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ekBramki.EditIndex = -1;
            PopulateGridView();
        }

        protected void ekBramki_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "UPDATE BRAMKI SET Id_gospo=@Id_gospo,Id_gosc=@Id_gosc,Id_pilkarze=@Id_pilkarze,PIL_Id_pilkarze=@PIL_Id_pilkarze,Czas_bramki=@Czas_bramki WHERE Id_Strzelec=@id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Id_gospo", System.Convert.ToInt32((ekBramki.Rows[e.RowIndex].FindControl("txtIDGospFooter") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Id_gosc", System.Convert.ToInt32((ekBramki.Rows[e.RowIndex].FindControl("txtIDGoscFooter") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Id_pilkarze", System.Convert.ToInt32((ekBramki.Rows[e.RowIndex].FindControl("txtStrzelecFooter") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@PIL_Id_pilkarze", System.Convert.ToInt32((ekBramki.Rows[e.RowIndex].FindControl("txtBramkarzFooter") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Czas_bramki", TimeSpan.Parse((ekBramki.Rows[e.RowIndex].FindControl("txtCzasFooter") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(ekBramki.DataKeys[e.RowIndex]));
                    sqlCmd.ExecuteNonQuery();
                    ekBramki.EditIndex = -1;
                    PopulateGridView();
                    lblSucces.Text = "Edytowano wybrany rekord.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                throw;
            }
        }

        protected void ekBramki_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM BRAMKI WHERE Id_Strzelec=@id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(ekBramki.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridView();
                    lblSucces.Text = "Usunięto wybrany rekord.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                throw;
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LOGIN.aspx");
        }
    }
}