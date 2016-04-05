using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;
using Passbook.Generator;
using Passbook.Generator.Fields;

namespace PassbookService.Controllers
{
    public class PassController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var generator = new PassGenerator();

            var request = new PassGeneratorRequest();
            //Mandatory data
            //You will need an Apple developer account and will need to get the next 2 values by setting up a Pass type through the developer site
            request.PassTypeIdentifier = "REPLACE WITH YOUR PASS TYPE ID";
            request.TeamIdentifier = "REPLACE WITH YOUR APPLE TEAM IDENTIFIER";

            //Pass data
            request.SerialNumber = "121212";
            request.Description = "POC Pass";
            request.OrganizationName = "POC Inc.";
            request.LogoText = "POC!";

            //Pass colors
            request.BackgroundColor = "#FFFFFF";
            request.LabelColor = "#000000";
            request.ForegroundColor = "#000000";
            request.BackgroundColor = "rgb(255,255,255)";
            request.LabelColor = "rgb(0,0,0)";
            request.ForegroundColor = "rgb(0,0,0)";

            //Load the cert using the Windows Certificate Store thumbprint. The Apple WWDRC cert is handled automatically (and location is hardcoded in the library)
            request.CertThumbprint = "‎REPLACE WITH CERT THUMBPRINT FOR YOUR CERT. SEE README LINKS FOR INSTRUCTIONS";
            request.CertLocation = System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser;


            //Add images (always include both standard and 2x image
            // override icon and icon retina
            request.Images.Add(PassbookImage.Icon, File.ReadAllBytes(HostingEnvironment.MapPath("~/images/icon.png")));
            request.Images.Add(PassbookImage.IconRetina, File.ReadAllBytes(HostingEnvironment.MapPath("~/images/icon@2x.png")));
            request.Images.Add(PassbookImage.Logo, File.ReadAllBytes(HostingEnvironment.MapPath("~/images/logo.png")));
            request.Images.Add(PassbookImage.LogoRetina, File.ReadAllBytes(HostingEnvironment.MapPath("~/images/logo@2x.png")));

            request.AddBarCode("01927847623423234234", BarcodeType.PKBarcodeFormatPDF417, "UTF-8", "01927847623423234234");

            var generatedPass = generator.Generate(request);

            //return new FileContentResult(generatedPass, "application/vnd.apple.pkpass");

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(generatedPass);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.apple.pkpass");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "LashPOC.pkpass"
            };
            return result;

        }
    }
}
