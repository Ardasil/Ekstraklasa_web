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
    public partial class CARDS : System.Web.UI.Page
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
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM KARTKI", connectionString);
                sqlda.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                ekKartki.DataSource = dtbl;
                ekKartki.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                ekKartki.DataSource = dtbl;
                ekKartki.DataBind();
                ekKartki.Rows[0].Cells.Clear();
                ekKartki.Rows[0].Cells.Add(new TableCell());
                ekKartki.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                ekKartki.Rows[0].Cells[0].Text = "Nie znaleziono żadnych rekordów!";
                ekKartki.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void ekKartki_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO KARTKI (Id_pilkarze,Id_sed,Id_gospo,Id_gosc,Rodzaj_kartki,Nazwa_sed,Nazwa_pilk) VALUES (@Id_pilkarze,@Id_sed,@Id_gospo,@Id_gosc,@Rodzaj_kartki,@Nazwa_sed,@Nazwa_pilk)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@Id_pilkarze", System.Convert.ToInt32((ekKartki.FooterRow.FindControl("txtIDPilkFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Id_sed", System.Convert.ToInt32((ekKartki.FooterRow.FindControl("txtIDSedFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Id_gospo", System.Convert.ToInt32((ekKartki.FooterRow.FindControl("txtIDGospFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Id_gosc", System.Convert.ToInt32((ekKartki.FooterRow.FindControl("txtIDGoscFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Rodzaj_kartki", System.Convert.ToInt32((ekKartki.FooterRow.FindControl("txtKartkaFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Nazwa_sed", (ekKartki.FooterRow.FindControl("txtSedFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Nazwa_pilk", (ekKartki.FooterRow.FindControl("txtPilkFooter") as TextBox).Text.Trim());
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

        protected void ekKartki_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ekKartki.EditIndex = e.NewEditIndex;
            PopulateGridView();
        }

        protected void ekKartki_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ekKartki.EditIndex = -1;
            PopulateGridView();
        }

        protected void ekKartki_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "UPDATE KARTKI SET Id_pilkarze=@Id_pilkarze,Id_sed=@Id_sed,Id_gospo=@Id_gospo,Id_gosc=@Id_gosc,Rodzaj_kartki=@Rodzaj_kartki,Nazwa_sed=@Nazwa_sed,Nazwa_pilk=@Nazwa_pilk WHERE Id_kartki=@id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Id_pilkarze", System.Convert.ToInt32((ekKartki.Rows[e.RowIndex].FindControl("txtIDPilk") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Id_sed", System.Convert.ToInt32((ekKartki.Rows[e.RowIndex].FindControl("txtIDSed") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Id_gospo", System.Convert.ToInt32((ekKartki.Rows[e.RowIndex].FindControl("txtIDGosp") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Id_gosc", System.Convert.ToInt32((ekKartki.Rows[e.RowIndex].FindControl("txtIDGosc") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Rodzaj_kartki", System.Convert.ToInt32((ekKartki.Rows[e.RowIndex].FindControl("txtKartka") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Nazwa_sed", (ekKartki.Rows[e.RowIndex].FindControl("txtSed") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Nazwa_pilk", (ekKartki.Rows[e.RowIndex].FindControl("txtPilk") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(ekKartki.DataKeys[e.RowIndex]));
                    sqlCmd.ExecuteNonQuery();
                    ekKartki.EditIndex = -1;
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

        protected void ekKartki_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM KARTKI WHERE Id_kartki=@id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(ekKartki.DataKeys[e.RowIndex].Value.ToString()));
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
