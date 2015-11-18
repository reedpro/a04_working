//File:Insert.aspx.cs
//Programers: Frank (Thom) Taylor, Jordan Poirier, Matthew Thiessen, Tylor McLaughlin
//Date:11-16-2015
//Purpose: This file contains the methods to insert new data into the ShopBase database. These methods will also do some client side field validation.
//Using statments
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// The insert class used to insert the data
    /// </summary>
    public partial class insert : System.Web.UI.Page
    {
        //A regex to ensure phone number is in the correct format. 
        private static readonly Regex phoneNumber = new Regex(@"\d{3}-\d{3}-\d{4}");
        public string port = "54510";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// This method will redirect the user from the page to a youtube video. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void Button15_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://www.youtube.com/watch?v=HbW-Bnm6Ipg");
        }
        /// <summary>
        /// This method will redirect the user back to the home page. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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
            string fileName = HttpContext.Current.Request.MapPath("MyLogs.txt");

            using (StreamWriter sw = File.AppendText(fileName))
            {
                log.Replace("+=", "");
                sw.WriteLine("["+ DateTime.Now.ToString() +"]"+ " " + log);
                log = "";
            }

        }
        /// <summary>
        /// This method will deal with all client side validation for the insert page. It will be triggered 
        /// when the user trys to execute their insert. Only an insert into a single table in the data base is allowed
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
            string errString = "";



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
            //For searching Customer table
            if (custSearch)
            {
                if (prodSearch || ordSearch || cartSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table";
                    WriteLog(errLbl.Text);
                }
                else
                {
                    //validate first name
                    if (firstNameTxt.Text == "")
                    {
                        errString += "First name cannot be blank. ";
                        WriteLog(errLbl.Text);
                    }
                    else if (Regex.IsMatch(firstNameTxt.Text, @"\d"))
                    {
                        errString += "First name cannot have numbers. ";
                        WriteLog("Error: " + "First name cannot have numbers. " + "Input: " + firstNameTxt.Text);
                        //   WriteLog("Intput: "+firstNameTxt.Text);
                    }

                    //valivate last name
                    if (lastNameTxt.Text == "")
                    {
                        errString += "Last name cannot be blank. ";
                        WriteLog(errString);
                    }
                    else if (Regex.IsMatch(lastNameTxt.Text, @"\d"))
                    {
                        errString += "Last name cannot have numbers. ";
                        WriteLog("Error: " + "Last name cannot have numbers. " + "Input: " + lastNameTxt.Text);
                    }

                    //validate phone number
                    if (phoneTxt.Text == "")
                    {
                        errString += "Phone number cannot be blank. ";
                        WriteLog(errString);
                    }
                    else if (!phoneNumber.IsMatch(phoneTxt.Text))
                    {
                        errString += "Phone number is not XXX-XXX-XXXX";
                        WriteLog("Error: " + "Phone number is not XXX-XXX-XXXX" + "Input: " + phoneTxt.Text);
                    }
                    //Thom makes changes
                    if (errString != "")
                    {
                        errLbl.Text = errString;
                    }
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
                    
                }
            }

          //For serching Product Table
            else if (prodSearch)
            {
                if (custSearch || ordSearch || cartSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table";
                    WriteLog(errString);
                }
                else
                {
                    //validate product name
                    if (prodNameTxt.Text == "")
                    {
                        errString += "Product name cannot be blank. ";
                        WriteLog(errString);
                    }

                    //valivate price
                    if (priceTxt.Text == "")
                    {
                        errString += "price cannot be blank. ";
                        WriteLog(errString);
                    }
                    else if (!Regex.IsMatch(priceTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        errString += "must be a valid price with numbers and decimal only X.XX. ";
                        WriteLog("Error: " + "must be a valid price with numbers and decimal only X.XX. " + "Input: " + priceTxt.Text);
                    }

                    //validate product weight
                    if (prodWeightTxt.Text == "")
                    {
                        errString += "Product weight cannot be blank. ";
                        WriteLog(errString);
                    }
                    else if (!Regex.IsMatch(prodWeightTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        errString += "Weight Must be a number. ";
                        WriteLog("Error: " + "Weight Must be a number. " + "Input: " + prodWeightTxt.Text);
                    }
                    //Thom starts changes
                    if (errString != "")
                    {
                        errLbl.Text = errString;
                    }
                    else
                    {
                        //format get  string
                        try
                        {
                            string content;
                            string Method = "post";
                            string uri = "http://localhost:" + port + "/Product/" + prodNameTxt.Text + " " + priceTxt.Text + " " + prodWeightTxt.Text;

                            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                            req.KeepAlive = false;
                            req.Method = Method.ToUpper();

                            content = prodNameTxt.Text + " " + priceTxt.Text + " " + prodWeightTxt.Text;

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
                    // errLbl.Text = errString;
                }
            }

                //For serching Order Table

            else if (ordSearch)
            {
                if (prodSearch || custSearch || cartSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table. ";
                    WriteLog(errLbl.Text);
                }
                else
                {
                    //validate customer id
                    if (ordCustIDTxt.Text == "")
                    {
                        errString += "custID cannot be blank. ";
                        WriteLog(errString);
                    }
                    else if (!Regex.IsMatch(ordCustIDTxt.Text, "^[1-9]*$"))
                    {
                        errString += "custID can only have numbers. ";
                        WriteLog("Error: " + "custID can only have numbers. " + "Input: " + ordCustIDTxt.Text);
                    }

                    DateTime temp;
                    string[] format = new string[] { "MM-dd-yy" };
                    //valivate order date
                    if (orderDateTxt.Text == "")
                    {
                        errString += "order date cannot be blank. ";
                        WriteLog(errString);
                    }
                    else if (!DateTime.TryParseExact(orderDateTxt.Text, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out temp))
                    {
                        errString += "date must be in MM-DD-YY format. ";
                        WriteLog("Error: " + "date must be in MM-DD-YY format. " + "Input: " + orderDateTxt.Text);
                    }
                    //Thom makes changes
                    if (errString != "")
                    {
                        errLbl.Text = errString;
                    }
                    else
                    {
                        //format get string

                        string getOrdString = ordCustIDTxt.Text +  " ";
                        //Check to see if
                        if (poNumberTxt.Text != "")
                        {
                            getOrdString +=  poNumberTxt.Text + " ";
                        }
                        getOrdString += orderDateTxt.Text;

                        try
                        {
                            string content;
                            string Method = "post";
                            string uri = "http://localhost:" + port + "/Product/" + getOrdString;

                            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                            req.KeepAlive = false;
                            req.Method = Method.ToUpper();

                            content = getOrdString;

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
                    // errLbl.Text = errString;
                }
            }

                //For serching Cart table
            else if (cartSearch)
            {
                if (prodSearch || ordSearch || custSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table";
                    WriteLog(errLbl.Text);
                }
                else
                {
                    //validate product id
                    if (cartProdIDTxt.Text == "")
                    {
                        errString += "prodID cannot be blank. ";
                    }
                    else if (!Regex.IsMatch(cartProdIDTxt.Text, "^[1-9]*$"))
                    {
                        errString += "prodID can only have numbers. ";
                        WriteLog("Error: " + "prodID can only have numbers. " + "Input: " + cartProdIDTxt.Text);
                    }


                    //valivate quantity
                    int i = 0;
                    if (quantityTxt.Text == "")
                    {
                        errString += "quantity cannot be blank. ";
                        WriteLog(errString);
                    }
                    else if (!int.TryParse(quantityTxt.Text, out i))
                    {
                        errString += "quantity must be an integer. ";
                        WriteLog("Error: " + "quantity must be an integer. " + "Input: " + quantityTxt.Text);
                    }
                    //Thom makes changes
                    if (errString != "")
                    {
                        errLbl.Text = errString;
                    }
                    else
                    {

                        //format get sting
                        string getCartString =  cartProdIDTxt.Text + " " + quantityTxt.Text;
                        try
                        {
                            string content;
                            string Method = "post";
                            string uri = "http://localhost:" + port + "/Product/" + getCartString;

                            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                            req.KeepAlive = false;
                            req.Method = Method.ToUpper();

                            content = getCartString;

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

                    // errLbl.Text = errString;
                }
            }
        }
    }
}