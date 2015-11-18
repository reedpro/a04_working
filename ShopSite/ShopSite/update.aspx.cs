//File:Insert.aspx.cs
//Programers: Frank (Thom) Taylor, Jordan Poirier, Matthew Thiessen, Tylor McLaughlin
//Date:11-16-2015
//Purpose: This file contains the methods to update data in the ShopBase database. These methods will also do some client side field validation.

//Using statments
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Text;

namespace ShopSite
{
    /// <summary>
    /// This calss contains the methods for updating the ShopDatabase
    /// </summary>
    public partial class update : System.Web.UI.Page
    {
        private static readonly Regex phoneNumber = new Regex(@"\d{3}-\d{3}-\d{4}");
        public string port = "54510";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button15_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://www.youtube.com/watch?v=HbW-Bnm6Ipg");
        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }
        /// <summary>
        /// Creates a file if one does not exist. Then writes
        /// an error log to it, including the time, the error that occured and the inccorect data.
        /// </summary>
        /// <param name="log">The error,time, and incorrect data sent to log</param>
        private void WriteLog(string log)
        {
            //Opens the log
            string fileName = HttpContext.Current.Request.MapPath("MyLogs.txt");
            //Writes the error to the log
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine("[" + DateTime.Now.ToString() + "]" + " " + log);
            }

        }

        /// <summary>
        /// This Method will check to make sure the data input
        /// by the user is correct and then send that data to update the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void excecuteBtn_Click(object sender, EventArgs e)
        {
            //Decleration of variables
            bool custSearch = false;
            bool prodSearch = false;
            bool ordSearch = false;
            bool cartSearch = false;
            string errMsg = "";
            string getString = "";

            List<Panel> panels = new List<Panel>();
            panels.Add(custPnl);
            panels.Add(prodPnl);
            panels.Add(orderPnl);
            panels.Add(cartPnl);

            foreach (Panel p in panels)
            {

                foreach (Control t in p.Controls)
                {
                    if (t is TextBox)
                    {
                        TextBox tx = (TextBox)t;
                        if (tx.Text != "")
                        {
                            if (p.ID == "custPnl")
                            {
                                custSearch = true;
                            }
                            else if (p.ID == "prodPnl")
                            {
                                prodSearch = true;
                            }
                            else if (p.ID == "orderPnl")
                            {
                                ordSearch = true;
                            }
                            else if (p.ID == "cartPnl")
                            {
                                cartSearch = true;
                            }
                        }
                    }
                }
            }
            //Checks to make sure only one table is being updated at a time
            if (custSearch)
            {
                if (prodSearch || ordSearch || cartSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table";
                    WriteLog(errLbl.Text);

                }
                //If only one table contains new data, check to see what data needs to be updated
                else
                {
                    //Error checking, and writing that error to a log
                    if (custIDTxt.Text != "" && Regex.IsMatch(custIDTxt.Text, @"^[1-9]?$"))
                    {
                        getString += "custID " + custIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(custIDTxt.Text, @"^[1-9]?$") || custIDTxt.Text == "")
                    {
                        errMsg += "customer ID must be numeric, and not blank ";
                        WriteLog("Error: " + "customer ID must be numeric, and not blank " + "Input: " + custIDTxt.Text);
                    }
                    //Error checking, and writing that error to a log
                    if (firstNameTxt.Text != "" && !Regex.IsMatch(firstNameTxt.Text, @"\d"))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "firstName " + firstNameTxt.Text;
                    }
                    else if (Regex.IsMatch(firstNameTxt.Text, @"\d"))
                    {
                        errMsg += "first name must not be numeric. ";
                        WriteLog("Error: " + "first name must not be numeric. " + "Input: " + firstNameTxt.Text);
                    }
                    //Error checking, and writing that error to a log
                    if (lastNameTxt.Text != "" && !Regex.IsMatch(lastNameTxt.Text, @"\d"))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "lastName " + lastNameTxt.Text;
                    }
                    else if (Regex.IsMatch(lastNameTxt.Text, @"\d"))
                    {
                        errMsg += "last name must not be numeric. ";
                        WriteLog("Error: " + "last name must not be numeric. " + "Input: " + lastNameTxt.Text);
                    }
                    //Error checking, and writing that error to a log
                    if (phoneTxt.Text != "" && phoneNumber.IsMatch(phoneTxt.Text))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "phoneNumber " + phoneTxt.Text;
                    }
                    else if (!phoneNumber.IsMatch(phoneTxt.Text) && phoneTxt.Text != "")
                    {
                        errMsg += "Phone number must be XXX-XXX-XXXX. ";
                        WriteLog("Error: " + "Phone number must be XXX-XXX-XXXX. " + "Input: " + phoneTxt.Text);
                    }
                    if (errMsg != "")
                    {
                        errLbl.Text = errMsg;
                    }
                    ///Send data string to the web Service
                    else
                    {
                        try
                        {
                            string content;
                            string Method = "post";
                            string uri = "http://localhost:" + port + "/Customer/" + firstNameTxt.Text + " " + lastNameTxt.Text + " " + phoneTxt.Text; ;

                            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                            req.KeepAlive = false;
                            req.Method = Method.ToUpper();

                            content = firstNameTxt.Text + " " + lastNameTxt.Text + " " + phoneTxt.Text;

                            byte[] buffer = Encoding.ASCII.GetBytes(content);
                            req.ContentLength = buffer.Length;
                            req.ContentType = "text/xml";
                            Stream PostData = req.GetRequestStream();
                            PostData.Write(buffer, 0, buffer.Length);
                            PostData.Close();


                            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

                            Encoding enc = System.Text.Encoding.GetEncoding(1252);
                            StreamReader loResponseStream =
                            new StreamReader(resp.GetResponseStream(), enc);

                            string Response = loResponseStream.ReadToEnd();


                            loResponseStream.Close();
                            resp.Close();
                            errLbl.Text = Response.ToString(); //show response

                        }
                        catch (Exception ex)
                        {
                            errLbl.Text = ex.Message.ToString();
                        }
                    }
                    //getString;
                }
            }
            //Checks to make sure only one table is being updated at a time
            else if (prodSearch)
            {
                if (custSearch || ordSearch || cartSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table";
                    WriteLog(errLbl.Text);
                }
                else
                {
                    //Error checking, and writing that error to a log
                    if (prodIDTxt.Text != "" && Regex.IsMatch(custIDTxt.Text, @"^[1-9]?$"))
                    {
                        getString += "prodID:" + prodIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(prodIDTxt.Text, @"^[1-9]?$") || prodIDTxt.Text == "")
                    {
                        errMsg += "product ID must be numeric, and not blank. ";
                        WriteLog("Error: " + "product ID must be numeric, and not blank. " + "Input: " + prodIDTxt.Text);
                    }
                    //Error checking, and writing that error to a log
                    if (prodNameTxt.Text != "" && !Regex.IsMatch(prodNameTxt.Text, @"\d"))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "prodName:" + prodNameTxt.Text;
                    }
                    else if (Regex.IsMatch(prodNameTxt.Text, @"\d"))
                    {
                        errMsg += "product name must not be numeric. ";
                        WriteLog("Error: " + "product name must not be numeric. " + "Input: " + prodNameTxt.Text);
                    }
                    //Error checking, and writing that error to a log
                    if (priceTxt.Text != "" && !Regex.IsMatch(priceTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "price:" + priceTxt.Text;
                    }
                    else if (Regex.IsMatch(priceTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        errMsg += "price be numeric. ";
                        WriteLog("Error: " + "price be numeric. " + "Input: " + priceTxt.Text);
                    }
                    //Error checking, and writing that error to a log
                    if (prodWeightTxt.Text != "" && Regex.IsMatch(prodWeightTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "prodWeight:" + prodWeightTxt.Text;
                    }
                    else if (!Regex.IsMatch(prodWeightTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        errMsg += "Product weight must be a number. ";
                        WriteLog("Error: " + "Product weight must be a number. " + "Input: " + prodWeightTxt.Text);
                    }
                    errLbl.Text = errMsg + "\n" + getString;
                }
            }
            else if (ordSearch)
            {
                ///Check to make sure only one table is being updated
                if (prodSearch || custSearch || cartSearch)
                {
                http://localhost:49462/update.aspx.cs
                    errLbl.Text = "Error: You cannot search more than one table";
                    WriteLog(errLbl.Text);
                }
                else
                {
                    //Error checking, and writing that error to a log
                    if (orderIDTxt.Text != "" && Regex.IsMatch(orderIDTxt.Text, @"^[1-9]?$"))
                    {
                        getString += "prodID:" + prodIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(orderIDTxt.Text, @"^[1-9]?$") || orderIDTxt.Text == "")
                    {
                        errMsg += "order ID must be numeric, and not blank.";
                        WriteLog("Error: " + "order ID must be numeric, and not blank." + "Input: " + orderIDTxt.Text);
                    }
                    //Error checking, and writing that error to a log
                    if (ordCustIDTxt.Text != "" && Regex.IsMatch(ordCustIDTxt.Text, @"^[1-9]?$"))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "custID:" + ordCustIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(ordCustIDTxt.Text, @"^[1-9]?$"))
                    {
                        errMsg += "customer ID must be numeric. ";
                        WriteLog("Error: " + "customer ID must be numeric. " + "Input: " + ordCustIDTxt.Text);
                    }
                    //Error checking, and writing that error to a log
                    if (poNumberTxt.Text != "")
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "poNumber:" + poNumberTxt.Text;
                    }
                    //Error checking, and writing that error to a log
                    DateTime temp;
                    string[] format = new string[] { "MM-dd-yy" };
                    //valivate order date
                    if (orderDateTxt.Text != "" && DateTime.TryParseExact(orderDateTxt.Text, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out temp))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "orderDate:" + orderDateTxt.Text;
                    }
                    else if (!DateTime.TryParseExact(orderDateTxt.Text, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out temp))
                    {
                        errMsg += "date must be in MM-DD-YY format. ";
                        WriteLog("Error: " + "date must be in MM-DD-YY format. " + "Input: " + orderDateTxt.Text);
                    }
                }
            }
            else if (cartSearch)
            {
                if (prodSearch || ordSearch || custSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table";
                    WriteLog(errLbl.Text);
                }
                //Error checking, and writing that error to a log
                else
                {
                    if (cartOrderIDTxt.Text != "" && Regex.IsMatch(cartOrderIDTxt.Text, @"^[1-9]?$"))
                    {
                        getString += "prodID:" + prodIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(cartOrderIDTxt.Text, @"^[1-9]?$") || cartOrderIDTxt.Text == "")
                    {
                        errMsg += "order ID must be numeric. ";
                        WriteLog("Error: " + "order ID must be numeric. " + "Input: " + cartOrderIDTxt.Text);
                    }
                    //Error checking, and writing that error to a log
                    if (cartProdIDTxt.Text != "" && Regex.IsMatch(cartProdIDTxt.Text, @"^[1-9]?$"))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "prodID:" + cartProdIDTxt.Text;
                    }
                    //Error checking, and writing that error to a log
                    else if (!Regex.IsMatch(cartProdIDTxt.Text, @"^[1-9]?$"))
                    {
                        errMsg += "product ID must be numeric. ";
                        WriteLog("Error: " + "product ID must be numeric. " + "Input: " + cartProdIDTxt.Text);
                    }
                    //Error checking, and writing that error to a log
                    int i = 0;
                    if (quantityTxt.Text != "" && int.TryParse(quantityTxt.Text, out i))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "quantity:" + quantityTxt.Text;
                    }
                    else if (!int.TryParse(quantityTxt.Text, out i))
                    {
                        errMsg += "quantity must be numeric. ";
                        WriteLog("Error: " + "quantity must be numeric. " + "Input: " + quantityTxt.Text);
                    }
                }
            }
        }
    }
}