using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopSite
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("search.aspx");
        }

        protected void Button15_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        }

        protected void insertBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("insert.aspx");
        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("update.aspx");
        }

        protected void deleteBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("delete.aspx");
        }



        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("https://www.youtube.com/watch?v=2G2w77jrayw");
        }
    }
}