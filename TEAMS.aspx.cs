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
    public partial class WebForm1 : System.Web.UI.Page
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
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM DRUZYNY", connectionString);
                sqlda.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                ekDruzyny.DataSource = dtbl;
                ekDruzyny.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                ekDruzyny.DataSource = dtbl;
                ekDruzyny.DataBind();
                ekDruzyny.Rows[0].Cells.Clear();
                ekDruzyny.Rows[0].Cells.Add(new TableCell());
                ekDruzyny.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                ekDruzyny.Rows[0].Cells[0].Text = "Nie znaleziono żadnych rekordów!";
                ekDruzyny.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void ekDruzyny_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO DRUZYNY (Nazwa_druz,Wlasciciel,Siedziba,Ilosc_rozegranych_meczy,Punkty) VALUES (@Nazwa_druz,@Wlasciciel,@Siedziba,@Ilosc_rozegranych_meczy,@Punkty)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@Nazwa_druz", (ekDruzyny.FooterRow.FindControl("txtDruzFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Wlasciciel", (ekDruzyny.FooterRow.FindControl("txtOwnerFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Siedziba", (ekDruzyny.FooterRow.FindControl("txtSiedzibaFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Ilosc_rozegranych_meczy", System.Convert.ToInt32((ekDruzyny.FooterRow.FindControl("txtMeczeFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Punkty", System.Convert.ToInt32((ekDruzyny.FooterRow.FindControl("txtPktFooter") as TextBox).Text.Trim()));
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

        protected void ekDruzyny_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ekDruzyny.EditIndex = e.NewEditIndex;
            PopulateGridView();
        }

        protected void ekDruzyny_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ekDruzyny.EditIndex = -1;
            PopulateGridView();
        }

        protected void ekDruzyny_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "UPDATE DRUZYNY SET Nazwa_druz=@Nazwa_druz,Wlasciciel=@Wlasciciel,Siedziba=@Siedziba,Ilosc_rozegranych_meczy=@Ilosc_rozegranych_meczy,Punkty=@Punkty WHERE Id_druz=@iksde";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Nazwa_druz", (ekDruzyny.Rows[e.RowIndex].FindControl("txtDruz") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Wlasciciel", (ekDruzyny.Rows[e.RowIndex].FindControl("txtOwner") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Siedziba", System.Convert.ToInt32((ekDruzyny.Rows[e.RowIndex].FindControl("txtSiedziba") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Ilosc_rozegranych_meczy", System.Convert.ToInt32((ekDruzyny.Rows[e.RowIndex].FindControl("txtMecze") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Punkty", System.Convert.ToInt32((ekDruzyny.Rows[e.RowIndex].FindControl("txtPkt") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@iksde", Convert.ToInt32(ekDruzyny.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    ekDruzyny.EditIndex = -1;
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

        protected void ekDruzyny_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM DRUZYNY WHERE Id_druz=@id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(ekDruzyny.DataKeys[e.RowIndex].Value.ToString()));
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