using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

//to connect with the database
using System.Data.SqlClient;
using System.Data;

//to write in to a text file
using System.IO;    
using System.Text;


namespace ASPRssReader
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateRssFeed();
            }
        }

        private void PopulateRssFeed()
        {
            //string RssFeedUrl = "http://timesofindia.feedsportal.com/c/33039/f/533965/index.rss";
            
            //string RssFeedUrl = "http://newsfirst.lk/sinhala/feed/";

            //string RssFeedUrl = "http://www.lankadeepa.lk/index.php/maincontroller/breakingnews_rss";

            string RssFeedUrl = "http://www.hirunews.lk/rss/sinhala.xml"; 

            //string RssFeedUrl = "http://sinhala.adaderana.lk/rsshotnews.php";

            List<Feeds> feeds = new List<Feeds>();
            try
            {
                int k = 0;
                XDocument xDoc = new XDocument();
                xDoc = XDocument.Load(RssFeedUrl);
                var items = (from x in xDoc.Descendants("item")
                             select new
                             {
                                 title = x.Element("title").Value,
                                 link = x.Element("link").Value,
                                 pubDate = x.Element("pubDate").Value,
                                 description = x.Element("description").Value
                             });
                if (items != null)
                {
                    foreach (var i in items)
                    {
                        Feeds f = new Feeds
                        {
                            Title = i.title,
                            Link = i.link,
                            PublishedDate = i.pubDate,
                            Description = i.description
                        };

                        feeds.Add(f);

                        //------------other stuff-----------
                        
                        if (k < 1)
                        {
                            //------------writing in to the text file-----------C:\Users\Madhuka Dilshan\Desktop
                            StreamWriter File = new StreamWriter(@"C:\Users\Madhuka Dilshan\Desktop\News_text.txt", true);
                            File.WriteLine(f.Description + " /n" + f.Title);
                            //File.Write("Hello world");
                            File.Close();

                            
                            //------------Add to database-----------
                            SqlConnection cs = new SqlConnection("Data Source=DIL;Initial Catalog=finalyear;Integrated Security=True");
                            //SqlConnection cs = new SqlConnection("Data Source = HP_TDK; Initial Catalog = NewsDB; Integrated Security=TRUE");
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.InsertCommand = new SqlCommand("INSERT INTO kk VALUES(@title, @link, @Description, @Pub_date)", cs);

                            da.InsertCommand.Parameters.Add("@title", SqlDbType.NVarChar, 100).Value = f.Title.Trim();
                            da.InsertCommand.Parameters.Add("@link", SqlDbType.NVarChar, 100).Value = f.Link.Trim();
                            da.InsertCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 100).Value = f.Description.Trim();
                            da.InsertCommand.Parameters.Add("@Pub_date", SqlDbType.NVarChar, 100).Value = f.PublishedDate.Trim();

                            cs.Open();
                            da.InsertCommand.ExecuteNonQuery();
                            cs.Close();
                           
                             
                            //k = 2;
                        }
                        //-----------------------

                    }
                }
                gvRss.DataSource = feeds;
                gvRss.DataBind();
            }
            catch(Exception ex)
            {
                throw;
            }
        } 
    }
}