using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmailController : ApiController
    {






        // GET: api/Email
        [AllowAnonymous]
        public string Get(string id , string numRooms, string  accomodations ,string price, string checkin ,string checkout, string fname , string lname,string email )
        {
           

           
           // string lname = " Seabi ";

            MailMessage mailMessage = new MailMessage(email,email);
            mailMessage.Subject = "Booking Information";

            mailMessage.Body = "Dear (Mr/Mrs/Ms) "+lname+", \n"+


"\n"+
"Thank you for choosing us at the Hotel1010."+
"\n" +
"We are pleased to confirm your reservation as follows: " +
"\n" +
"\n" +

"\nConfirmation Number:                         "+ id+
"\nGuest Name         :                              "+fname+" "+lname+
"\nAccommodations     :			      " + accomodations +
"\nNumber of Rooms    :			      " +numRooms+
"\nRate per Night :				   " + price +
"\nCheck -in Date:		                          "+checkin+
"\nCheck -out Date:			         "+checkout+
"\n" +
"\nShould you require any clarification, please make your request as soon as possible." +

"\n" +
"\n" +
"\nWhatever we can do to make your visit extra special, call us at(015) 555 7667." +
"\nOr by clicking Booking.com, you will be taken to our  where we'll assist you ." +
"\n" +
"\n" +


"\nSincerely," +
"\nReservations Department";



            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            /*smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "@gmail.com",
                Password = "*"
            };
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
            */



            return "Check Email";


           
        }

       
    }
}
