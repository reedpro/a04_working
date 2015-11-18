//File:         search.aspx.cs
//Description:  This lets you search by validating and then sending the data to the server through the rest webmethod
//Programmers:  Jordan Poirier, Matthew Thiessen, Thom Taylor, Tylor Mclaughlin
//Date:         11/16/2015




using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;

namespace ShopSite
{
    public partial class search : System.Web.UI.Page
    {
        private static readonly Regex phoneNumber = new Regex(@"\d{3}-\d{3}-\d{4}");
        public string port = "54510";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Method:       Button15_click
        //Description:  "Get me out of here" button, leaves the website
        protected void Button15_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        }

        //Method:       backBtn_Click
        //Description:  returns to home page
        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        //Method:       excecuteBtn_Click
        //Description:  excecutes search along with error checking
        protected void excecuteBtn_Click(object sender, EventArgs e)
        {
            //variables
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

            //check each panel to see if any of them have a textbox that isnt empty
            foreach (Panel p in panels)
            {
                //check each text box
                foreach (Control t in p.Controls)
                {
                    if (t is TextBox)
                    {
                        TextBox tx = (TextBox)t;
                        //if text box isnt blank set appropriat bool true
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
            //if customer search is true and no other table is true
            if (custSearch)
            {
                if (prodSearch || ordSearch || cartSearch)
                {
                    //add to error string
                    errLbl.Text = "Error: You cannot search more than one table";
                }
                else //then we are doing a customer search. build get string
                {
                    //check custID to see if it is valid and non-blank
                    if (custIDTxt.Text != "" && Regex.IsMatch(custIDTxt.Text, @"^[1-9]?$"))
                    {
                        getString += custIDTxt.Text;
                    } 
                    else if (!Regex.IsMatch(custIDTxt.Text, @"^[1-9]?$")) //if it is non-blank and invalid add to error string
                    {
                        errMsg += "customer ID must be numeric. ";
                    }
                    //check first name for valid non blank
                    if (firstNameTxt.Text != "" && !Regex.IsMatch(firstNameTxt.Text, @"\d"))
                    {
                        if (getString != "")
                        {
                            getString += " "; //add comma to "get" string
                        }
                        getString += firstNameTxt.Text;
                    }
                    else if (Regex.IsMatch(firstNameTxt.Text, @"\d"))
                    {
                        errMsg += "first name must not be numeric. ";
                    }
                    //lastname
                    if (lastNameTxt.Text != "" && !Regex.IsMatch(lastNameTxt.Text, @"\d"))
                    {
                        if (getString != "")
                        {
                            getString += " ";
                        }
                        getString += lastNameTxt.Text;
                    }
                    else if (Regex.IsMatch(lastNameTxt.Text, @"\d"))
                    {
                        errMsg += "last name must not be numeric. ";
                    }
                    //phone number
                    if (phoneTxt.Text != "" && phoneNumber.IsMatch(phoneTxt.Text))
                    {
                        if (getString != "")
                        {
                            getString += " ";
                        }
                        getString += phoneTxt.Text;
                    }
                    else if (!phoneNumber.IsMatch(phoneTxt.Text) && phoneTxt.Text != "")
                    {
                        errMsg += "Phone number must be XXX-XXX-XXXX. ";
                    }

                    //if error message isnt blank, display error
                    if (errMsg != "")
                    {
                        errLbl.Text = errMsg;
                    }
                    else //otherwise connect to rest service
                    {
                        try
                        {
                            string content;
                            string Method = "get";
                            string uri = "http://localhost:" + port + "/Customer/" + getString; 

                            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                            req.KeepAlive = false;
                            req.Method = Method.ToUpper();

                            //if (("POST,PUT").Split(',').Contains(Method.ToUpper()))
                            //{
                            //    Console.WriteLine("Enter XML FilePath:");
                            //    string FilePath = Console.ReadLine();
                            //    content = (File.OpenText(@FilePath)).ReadToEnd();

                            //    byte[] buffer = Encoding.ASCII.GetBytes(content);
                            //    req.ContentLength = buffer.Length;
                            //    req.ContentType = "text/xml";
                            //    Stream PostData = req.GetRequestStream();
                            //    PostData.Write(buffer, 0, buffer.Length);
                            //    PostData.Close();
                            //}

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
            //search product
            else if (prodSearch)
            {
                if (custSearch || ordSearch || cartSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table";
                }
                else
                {
                    //if product text is nonblank and valid
                    if (prodIDTxt.Text != "" && Regex.IsMatch(custIDTxt.Text, @"^[1-9]?$"))
                    {
                        getString += prodIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(prodIDTxt.Text, @"^[1-9]?$"))
                    {
                        errMsg += "product ID must be numeric. ";
                    }

                    if (prodNameTxt.Text != "" && !Regex.IsMatch(prodNameTxt.Text, @"\d"))
                    {
                        if (getString != "")
                        {
                            getString += " ";
                        }
                        getString += prodNameTxt.Text;
                    }
                    else if (Regex.IsMatch(prodNameTxt.Text, @"\d"))
                    {
                        errMsg += "product name must not be numeric. ";
                    }

                    if (priceTxt.Text != "" && !Regex.IsMatch(priceTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        if (getString != "")
                        {
                            getString += " ";
                        }
                        getString += priceTxt.Text;
                    }
                    else if (Regex.IsMatch(priceTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        errMsg += "price be numeric. ";
                    }

                    if (prodWeightTxt.Text != "" && Regex.IsMatch(prodWeightTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        if (getString != "")
                        {
                            getString += " ";
                        }
                        getString += prodWeightTxt.Text;
                    }
                    else if (!Regex.IsMatch(prodWeightTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        errMsg += "Product weight must be a number. ";
                    }
                    if (errMsg != "")
                    {
                        errLbl.Text = errMsg + "\n" + getString;
                    }
                    else
                    {
                        try
                        {
                            string content;
                            string Method = "get";
                            string uri = "http://localhost:" + port + "/product/" + getString;

                            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                            req.KeepAlive = false;
                            req.Method = Method.ToUpper();

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
            else if (ordSearch)
            {
                if (prodSearch || custSearch || cartSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table";
                }
                else
                {
                    if (orderIDTxt.Text != "" && Regex.IsMatch(orderIDTxt.Text, @"^[1-9]?$"))
                    {
                        getString += prodIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(orderIDTxt.Text, @"^[1-9]?$"))
                    {
                        errMsg += "order ID must be numeric. ";
                    }

                    if (ordCustIDTxt.Text != "" && Regex.IsMatch(ordCustIDTxt.Text, @"^[1-9]?$"))
                    {
                        if (getString != "")
                        {
                            getString += " ";
                        }
                        getString += ordCustIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(ordCustIDTxt.Text, @"^[1-9]?$"))
                    {
                        errMsg += "customer ID must be numeric. ";
                    }

                    if (poNumberTxt.Text != "")
                    {
                        if (getString != "")
                        {
                            getString += " ";
                        }
                        if (errMsg != "")
                        {
                            getString += poNumberTxt.Text;
                        }
                    }

                    DateTime temp;
                    string[] format = new string[] { "MM-dd-yy" };
                    //valivate order date
                    if (orderDateTxt.Text != "" && DateTime.TryParseExact(orderDateTxt.Text, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out temp))
                    {
                        if (getString != "")
                        {
                            getString += " ";
                        }
                        getString += orderDateTxt.Text;
                    }
                    else if (!DateTime.TryParseExact(orderDateTxt.Text, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out temp))
                    {
                        errMsg += "date must be in MM-DD-YY format. ";
                    }
                }
                if(errMsg!= "")
                {
                    errLbl.Text = errMsg;
                }
                else
                {
                    try
                    {
                        string content;
                        string Method = "get";
                        string uri = "http://localhost:" + port + "/order/" + getString;

                        HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                        req.KeepAlive = false;
                        req.Method = Method.ToUpper();

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
            else if (cartSearch)
            {
                if (prodSearch || ordSearch || custSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table";
                }
                else
                {
                    if (cartOrderIDTxt.Text != "" && Regex.IsMatch(cartOrderIDTxt.Text, @"^[1-9]?$"))
                    {
                        getString += prodIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(cartOrderIDTxt.Text, @"^[1-9]?$"))
                    {
                        errMsg += "order ID must be numeric. ";
                    }

                    if (cartProdIDTxt.Text != "" && Regex.IsMatch(cartProdIDTxt.Text, @"^[1-9]?$"))
                    {
                        if (getString != "")
                        {
                            getString += " ";
                        }
                        getString += cartProdIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(cartProdIDTxt.Text, @"^[1-9]?$"))
                    {
                        errMsg += "product ID must be numeric. ";
                    }
                    int i = 0;
                    if (quantityTxt.Text != "" && int.TryParse(quantityTxt.Text, out i))
                    {
                        if (getString != "")
                        {
                            getString += " ";
                        }
                        getString += quantityTxt.Text;
                    }
                    else if (!int.TryParse(quantityTxt.Text, out i))
                    {
                        errMsg += "quantity must be numeric. ";
                    }
                }
                if(errMsg != "")
                {
                    try
                    {
                        string content;
                        string Method = "get";
                        string uri = "http://localhost:" + port + "/cart/" + getString;

                        HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                        req.KeepAlive = false;
                        req.Method = Method.ToUpper();

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
                else
                {
                    errLbl.Text = errMsg;
                }
            }
        }
    }
}