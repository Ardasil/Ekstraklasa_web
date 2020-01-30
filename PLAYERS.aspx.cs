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
    public partial class PLAYERS : System.Web.UI.Page
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
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM PILKARZE ORDER BY Id_druz ASC", connectionString);
                sqlda.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                ekPilkarze.DataSource = dtbl;
                ekPilkarze.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                ekPilkarze.DataSource = dtbl;
                ekPilkarze.DataBind();
                ekPilkarze.Rows[0].Cells.Clear();
                ekPilkarze.Rows[0].Cells.Add(new TableCell());
                ekPilkarze.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                ekPilkarze.Rows[0].Cells[0].Text = "Nie znaleziono żadnych rekordów!";
                ekPilkarze.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void ekPilkarze_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO PILKARZE (Id_druz,Imie_pilk,Nazwisko_pilk,Wiek_pilk,Ilosc_meczy_pilk,Ilosc_strzelonych_goli_pilk,Ilosc_wpuszczonych_goli_pilk,Specjalizacja) VALUES (@Id_druz,@Imie_pilk,@Nazwisko_pilk,@Wiek_pilk,@Ilosc_meczy_pilk,@Ilosc_strzelonych_goli_pilk,@Ilosc_wpuszczonych_goli_pilk,@Specjalizacja)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@Id_druz", System.Convert.ToInt32((ekPilkarze.FooterRow.FindControl("txtIDDruzFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Imie_pilk", (ekPilkarze.FooterRow.FindControl("txtImieFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Nazwisko_pilk", (ekPilkarze.FooterRow.FindControl("txtNazwiskoFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Wiek_pilk", System.Convert.ToInt32((ekPilkarze.FooterRow.FindControl("txtWiekFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Ilosc_meczy_pilk", System.Convert.ToInt32((ekPilkarze.FooterRow.FindControl("txtMeczeFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Ilosc_strzelonych_goli_pilk", System.Convert.ToInt32((ekPilkarze.FooterRow.FindControl("txtStrzelFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Ilosc_wpuszczonych_goli_pilk", System.Convert.ToInt32((ekPilkarze.FooterRow.FindControl("txtStracFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Specjalizacja", (ekPilkarze.FooterRow.FindControl("txtPozFooter") as TextBox).Text.Trim());
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

        protected void ekPilkarze_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ekPilkarze.EditIndex = e.NewEditIndex;
            PopulateGridView();
        }

        protected void ekPilkarze_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ekPilkarze.EditIndex = -1;
            PopulateGridView();
        }

        protected void ekPilkarze_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "UPDATE PILKARZE SET Id_druz=@Id_druz,Imie_pilk=@Imie_pilk,Nazwisko_pilk=@Nazwisko_pilk,Nazwa_druz_pilk=@Nazwa_druz_pilk,Wiek_pilk=@Wiek_pilk,Ilosc_meczy_pilk=@Ilosc_meczy_pilk,Ilosc_strzelonych_goli_pilk=@Ilosc_strzelonych_goli_pilk,Ilosc_wpuszczonych_goli_pilk=@Ilosc_wpuszczonych_goli_pilk,Specjalizacja=@Specjalizacja WHERE Id_pilkarze=@id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Id_druz", System.Convert.ToInt32((ekPilkarze.Rows[e.RowIndex].FindControl("txtIDDruz") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Imie_pilk", (ekPilkarze.Rows[e.RowIndex].FindControl("txtImie") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Nazwisko_pilk", (ekPilkarze.Rows[e.RowIndex].FindControl("txtNazwisko") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Nazwa_druz_pilk", (ekPilkarze.Rows[e.RowIndex].FindControl("txtDruz") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Wiek_pilk", System.Convert.ToInt32((ekPilkarze.Rows[e.RowIndex].FindControl("txtWiek") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Ilosc_meczy_pilk", System.Convert.ToInt32((ekPilkarze.Rows[e.RowIndex].FindControl("txtMecze") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Ilosc_strzelonych_goli_pilk", System.Convert.ToInt32((ekPilkarze.Rows[e.RowIndex].FindControl("txtStrzel") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Ilosc_wpuszczonych_goli_pilk", System.Convert.ToInt32((ekPilkarze.Rows[e.RowIndex].FindControl("txtStrac") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Specjalizacja", (ekPilkarze.Rows[e.RowIndex].FindControl("txtPoz") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(ekPilkarze.DataKeys[e.RowIndex]));
                    sqlCmd.ExecuteNonQuery();
                    ekPilkarze.EditIndex = -1;
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

        protected void ekPilkarze_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM PILKARZE WHERE Id_pilkarze=@id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(ekPilkarze.DataKeys[e.RowIndex].Value.ToString()));
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