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
    public partial class REFEREES : System.Web.UI.Page
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
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM SEDZIOWIE", connectionString);
                sqlda.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                ekSedziowie.DataSource = dtbl;
                ekSedziowie.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                ekSedziowie.DataSource = dtbl;
                ekSedziowie.DataBind();
                ekSedziowie.Rows[0].Cells.Clear();
                ekSedziowie.Rows[0].Cells.Add(new TableCell());
                ekSedziowie.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                ekSedziowie.Rows[0].Cells[0].Text = "Nie znaleziono żadnych rekordów!";
                ekSedziowie.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void ekSedziowie_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO SEDZIOWIE (Id_gospo,Id_gosc,Imie_sed,Nazwisko_sed,Wiek_sed,Ilosc_wydanych_kartek) VALUES (@Id_gospo,@Id_gosc,@Imie_sed,@Nazwisko_sed,@Wiek_sed,@Ilosc_wydanych_kartek)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@Id_gospo", System.Convert.ToInt32((ekSedziowie.FooterRow.FindControl("txtIDGospFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Id_gosc", System.Convert.ToInt32((ekSedziowie.FooterRow.FindControl("txtIDGoscFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Imie_sed", (ekSedziowie.FooterRow.FindControl("txtImieFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Nazwisko_sed", (ekSedziowie.FooterRow.FindControl("txtNazwiskoFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Wiek_sed", System.Convert.ToInt32((ekSedziowie.FooterRow.FindControl("txtWiekFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Ilosc_wydanych_kartek", System.Convert.ToInt32((ekSedziowie.FooterRow.FindControl("txtKartkiFooter") as TextBox).Text.Trim()));
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

        protected void ekSedziowie_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ekSedziowie.EditIndex = e.NewEditIndex;
            PopulateGridView();
        }

        protected void ekSedziowie_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ekSedziowie.EditIndex = -1;
            PopulateGridView();
        }

        protected void ekSedziowie_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "UPDATE SEDZIOWIE SET Id_gospo=@Id_gospo,Id_gosc=@Id_gosc,Imie_sed=@Imie_sed,Nazwisko_sed=@Nazwisko_sed,Wiek_sed=@Wiek_sed,Ilosc_wydanych_kartek=@Ilosc_wydanych_kartek WHERE Id_sed=@id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Id_gospo", System.Convert.ToInt32((ekSedziowie.Rows[e.RowIndex].FindControl("txtIDGosp") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Id_gosc", System.Convert.ToInt32((ekSedziowie.Rows[e.RowIndex].FindControl("txtIDGosc") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Imie_sed", (ekSedziowie.Rows[e.RowIndex].FindControl("txtImie") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Nazwisko_sed", (ekSedziowie.Rows[e.RowIndex].FindControl("txtNazwisko") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Wiek_sed", System.Convert.ToInt32((ekSedziowie.Rows[e.RowIndex].FindControl("txtWiek") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Ilosc_wydanych_kartek", System.Convert.ToInt32((ekSedziowie.Rows[e.RowIndex].FindControl("txtKartki") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(ekSedziowie.DataKeys[e.RowIndex]));
                    sqlCmd.ExecuteNonQuery();
                    ekSedziowie.EditIndex = -1;
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

        protected void ekSedziowie_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM SEDZIOWIE WHERE Id_sed=@id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(ekSedziowie.DataKeys[e.RowIndex].Value.ToString()));
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