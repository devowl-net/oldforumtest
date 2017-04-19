namespace SimpleForum.Controllers
{
    //http://www.hanselman.com/blog/BackToBasicsDynamicImageGenerationASPNETControllersRoutingIHttpHandlersAndRunAllManagedModulesForAllRequests.aspx
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Web;
    using System.Web.Routing;

    public class ForumImageProvider : IHttpHandler
    {
        public bool IsReusable { get { return false; } }
        protected RequestContext RequestContext { get; set; }

        public ForumImageProvider() : base() { }

        public ForumImageProvider(RequestContext requestContext)
        {
            this.RequestContext = requestContext;
        }

        public void ProcessRequest(HttpContext context)
        {
            using (var rectangleFont = new Font("Arial", 14, FontStyle.Bold))
            using (var bitmap = new Bitmap(320, 110, PixelFormat.Format24bppRgb))
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var backgroundColor = Color.Bisque;
                g.Clear(backgroundColor);
                g.DrawString("This PNG was totally generated", rectangleFont, SystemBrushes.WindowText, new PointF(10, 40));
                context.Response.ContentType = "image/png";
                bitmap.Save(context.Response.OutputStream, ImageFormat.Png);
            }
        }
    }

    /* 1.
        public static void RegisterRoutes(RouteCollection routes)
        {
            var v = new ForumImageProvider().GetType();
            routes.Add(new Route("ImageProvider/{UserId}/profile.png",
                new ForumImageRouteHandler()));
     */
    
        // 2.
        //<system.webServer>
        //<validation validateIntegratedModeConfiguration="false" />
        //<handlers>
        //<add name="pngs" verb="*" path="images/handlerproducts/*/default.png" type="SimpleForum.Controllers.ForumImageProvider, SimpleForum.Controllers" preCondition="managedHandler"/>
    /*
     * 3.  <img src="@Url.Content(string.Format("~/ImageProvider/{0}/profile.png", Model.Id))" width="300" height="264" alt="Фотография @Model.NickName" />
     * 
     */
}