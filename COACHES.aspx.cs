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
    public partial class COACHES: System.Web.UI.Page
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
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM TRENERZY", connectionString);
                sqlda.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                ekTrenerzy.DataSource = dtbl;
                ekTrenerzy.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                ekTrenerzy.DataSource = dtbl;
                ekTrenerzy.DataBind();
                ekTrenerzy.Rows[0].Cells.Clear();
                ekTrenerzy.Rows[0].Cells.Add(new TableCell());
                ekTrenerzy.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                ekTrenerzy.Rows[0].Cells[0].Text = "Nie znaleziono żadnych rekordów!";
                ekTrenerzy.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void ekTrenerzy_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO TRENERZY (Id_druz,Imie_tren,Nazwisko_tren,Nazwa_druz_tren,Wiek_tren) VALUES (@Id_druz,@Imie_tren,@Nazwisko_tren,@Nazwa_druz_tren,@Wiek_tren)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@Id_druz", System.Convert.ToInt32((ekTrenerzy.FooterRow.FindControl("txtIDDruzFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Imie_tren", (ekTrenerzy.FooterRow.FindControl("txtImieFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Nazwisko_tren", (ekTrenerzy.FooterRow.FindControl("txtNazwiskoFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Nazwa_druz_tren", (ekTrenerzy.FooterRow.FindControl("txtDruzFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Wiek_tren", System.Convert.ToInt32((ekTrenerzy.FooterRow.FindControl("txtWiekFooter") as TextBox).Text.Trim()));
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

        protected void ekTrenerzy_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ekTrenerzy.EditIndex = e.NewEditIndex;
            PopulateGridView();
        }

        protected void ekTrenerzy_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ekTrenerzy.EditIndex = -1;
            PopulateGridView();
        }

        protected void ekTrenerzy_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "UPDATE TRENERZY SET Id_druz=@Id_druz,Imie_tren=@Imie_tren,Nazwisko_tren=@Nazwisko_tren,Nazwa_druz_tren=@Nazwa_druz_tren,Wiek_tren=@Wiek_tren WHERE Id_tren=@id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Id_druz", System.Convert.ToInt32((ekTrenerzy.Rows[e.RowIndex].FindControl("txtIDDruz") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Imie_tren", (ekTrenerzy.Rows[e.RowIndex].FindControl("txtImie") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Nazwisko_tren", (ekTrenerzy.Rows[e.RowIndex].FindControl("txtNazwisko") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Nazwa_druz_tren", (ekTrenerzy.Rows[e.RowIndex].FindControl("txtDruz") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Wiek_tren", System.Convert.ToInt32((ekTrenerzy.Rows[e.RowIndex].FindControl("txtWiek") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(ekTrenerzy.DataKeys[e.RowIndex]));
                    sqlCmd.ExecuteNonQuery();
                    ekTrenerzy.EditIndex = -1;
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

        protected void ekTrenerzy_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM TRENERZY WHERE Id_tren=@id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(ekTrenerzy.DataKeys[e.RowIndex].Value.ToString()));
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