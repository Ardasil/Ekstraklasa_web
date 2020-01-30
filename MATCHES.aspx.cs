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
    public partial class MATCHES : System.Web.UI.Page
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
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM SPOTKANIA ORDER BY DATA ASC", connectionString);                
                sqlda.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                ekSpotkania.DataSource = dtbl;
                ekSpotkania.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                ekSpotkania.DataSource = dtbl;
                ekSpotkania.DataBind();
                ekSpotkania.Rows[0].Cells.Clear();
                ekSpotkania.Rows[0].Cells.Add(new TableCell());
                ekSpotkania.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                ekSpotkania.Rows[0].Cells[0].Text = "Nie znaleziono żadnych rekordów!";
                ekSpotkania.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void ekSpotkania_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO SPOTKANIA (Id_gospo,Id_gosc,Id_druz,DRU_Id_druz,Data,Godzina,Miejsce,Wynik_gospo,Wynik_gosc) VALUES (FLOOR(RAND()*(20+1))+10,FLOOR(RAND()*(20+1))+10,@Id_druz,@DRU_Id_druz,@Data,@Godzina,@Miejsce,@Wynik_gospo,@Wynik_gosc)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@Id_druz", System.Convert.ToInt32((ekSpotkania.FooterRow.FindControl("txtIDGospFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@DRU_Id_druz", System.Convert.ToInt32((ekSpotkania.FooterRow.FindControl("txtIDGoscFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Data", DateTime.Parse((ekSpotkania.FooterRow.FindControl("txtDataFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Godzina", TimeSpan.Parse((ekSpotkania.FooterRow.FindControl("txtGodzinaFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Miejsce", (ekSpotkania.FooterRow.FindControl("txtMiejsceFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Wynik_gospo", System.Convert.ToInt32((ekSpotkania.FooterRow.FindControl("txtWynikGospFooter") as TextBox).Text.Trim()));
                        sqlCmd.Parameters.AddWithValue("@Wynik_gosc", System.Convert.ToInt32((ekSpotkania.FooterRow.FindControl("txtWynikGoscFooter") as TextBox).Text.Trim()));
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

        protected void ekSpotkania_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ekSpotkania.EditIndex = e.NewEditIndex;
            PopulateGridView();
        }

        protected void ekSpotkania_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ekSpotkania.EditIndex = -1;
            PopulateGridView();
        }

        protected void ekSpotkania_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                 {
                    sqlCon.Open();
                    string query = "UPDATE SPOTKANIA SET Id_druz=@Id_druz,DRU_Id_druz=@DRU_Id_druz,Data=@Data,Godzina=@Godzina,Miejsce=@Miejsce,Wynik_gospo=@Wynik_gospo,Wynik_gosc=@Wynik_gosc WHERE Id_gospo=@id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Id_druz", System.Convert.ToInt32((ekSpotkania.Rows[e.RowIndex].FindControl("txtIDGosp") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@DRU_Id_druz", System.Convert.ToInt32((ekSpotkania.Rows[e.RowIndex].FindControl("txtIDGosc") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Data", DateTime.Parse((ekSpotkania.Rows[e.RowIndex].FindControl("txtData") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Godzina", TimeSpan.Parse((ekSpotkania.Rows[e.RowIndex].FindControl("txtGodzina") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Miejsce", (ekSpotkania.Rows[e.RowIndex].FindControl("txtMiejsce") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Wynik_gospo", System.Convert.ToInt32((ekSpotkania.Rows[e.RowIndex].FindControl("txtWynikGosp") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@Wynik_gosc", System.Convert.ToInt32((ekSpotkania.Rows[e.RowIndex].FindControl("txtWynikGosc") as TextBox).Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(ekSpotkania.DataKeys[e.RowIndex]));
                    sqlCmd.ExecuteNonQuery();
                    ekSpotkania.EditIndex = -1;
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

        protected void ekSpotkania_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM SPOTKANIA WHERE Id_gospo=@id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(ekSpotkania.DataKeys[e.RowIndex].Value.ToString()));
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