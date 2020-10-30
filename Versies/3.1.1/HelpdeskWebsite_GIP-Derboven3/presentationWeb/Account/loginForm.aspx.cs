﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;

namespace presentationWeb
{
    public partial class loginForm : System.Web.UI.Page
    {
        Business _business = new Business();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtemail_gebruikersnaam.Text == "" || txtpassword.Text == "")
            {
                error.Visible = true;
                lblError.Text = "Gelieve alles in te vullen.";
            }
            else
            {
                int id = _business.login(txtemail_gebruikersnaam.Text, txtpassword.Text);
                if (id == 0)
                {
                    error.Visible = true;
                    lblError.Text = "Foute gebruikersnaam of passwoord.";
                }
                else
                {
                    Session["loggedin"] = true;
                    Session["role"] = _business.SoortGebruiker(id);
                    Session["naam"] = txtemail_gebruikersnaam.Text;
                    Session["id"] = id;
                    Response.Redirect("/dashboard.aspx");
                }
            }
        }
    }
}