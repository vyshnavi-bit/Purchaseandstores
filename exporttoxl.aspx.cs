﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class exporttoxl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["xportdata"] != null)
        {
            DataTable dtt = (DataTable)Session["xportdata"];
            if (dtt.Columns.Contains("InvoiceNo"))
            {
                dtt.Columns.Remove("InvoiceNo");
            }
            ExportToExcel(dtt);
        }
    }

    public void ExportToExcel(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
              
                string filena = Session["filename"].ToString();
                string title = Session["Address"].ToString();
                string filename = "";
                if (filena != "" && filena != null)
                {
                    filename = filena;
                }
                else
                {
                    filename = "Report";
                }

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                //sets font
                HttpContext.Current.Response.Write("<font style='font-size:10.0pt;'>");
                HttpContext.Current.Response.Write("<BR><BR><BR>");
                //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
                HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
                  "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                  "style='font-size:10.0pt; background:white;'> <TR>");
                int columnscount = dt.Columns.Count;
                //For Header
                if (filena == "Stores MRN Report" || filena == "Stores ISSUE Report" || filena == "Stores Branch Transfer report")
                {

                }
                else
                {
                    HttpContext.Current.Response.Write("<Td colspan='" + columnscount + "' align='center' style='font-size:30.0pt;'>SRI VYSHNAVI DAIRY SPECIALITIES (P) LTD</Td><TR>");
                    HttpContext.Current.Response.Write("<Td colspan='" + columnscount + "' align='center' style='font-size:20.0pt;'>" + title + "</Td><TR>");
                }
                //am getting my grid's column headers
                for (int j = 0; j < columnscount; j++)
                {      //write in new column
                    HttpContext.Current.Response.Write("<Td style='font-size:14.0pt;'>");
                    //Get column headers  and make it as bold in excel columns
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(dt.Columns[j].ColumnName.ToString());
                    HttpContext.Current.Response.Write("</B>");
                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");
                foreach (DataRow row in dt.Rows)
                {//write in new row
                    HttpContext.Current.Response.Write("<TR>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write(row[i].ToString());
                        HttpContext.Current.Response.Write("</Td>");
                    }

                    HttpContext.Current.Response.Write("</TR>");
                }
                HttpContext.Current.Response.Write("</Table>");
                HttpContext.Current.Response.Write("</font>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();





            }
        }
        catch (Exception ex)
        {
        }
    }
}