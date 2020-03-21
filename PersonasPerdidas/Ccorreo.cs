using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace PersonasPerdidas
{
    public class Ccorreo
    {


        Boolean estado = true;
        string merror;
        public Ccorreo(string destinatario, string asunto, string mensaje)
        {
            MailMessage correo = new MailMessage();
            SmtpClient protocolo = new SmtpClient();

            correo.To.Add(destinatario);
            //correo de origen
            correo.From = new MailAddress("belvasquez06@gmail.com", "Vecindario Seguro", System.Text.Encoding.UTF8);
            correo.Subject = asunto;
            correo.SubjectEncoding = System.Text.Encoding.UTF8;
            correo.Body = mensaje;
            correo.BodyEncoding = System.Text.Encoding.UTF8;
            correo.IsBodyHtml = false;
            MailAddress Bk = new MailAddress("belkisvasquez@outlook.es", "Belkis Y. Vasquez peña", System.Text.Encoding.UTF8);
            correo.CC.Add(Bk);
            MailAddress Gl = new MailAddress("gerlenzorrilla0307@gmail.com", "Gerlen E. Aquino Zorrilla", System.Text.Encoding.UTF8);
            correo.CC.Add(Gl);



            protocolo.Credentials = new System.Net.NetworkCredential("belvasquez06@gmail.com", "abcd123@A");
            protocolo.Port = 587;
            protocolo.Host = "smtp.gmail.com";
            protocolo.EnableSsl = true;

            try
            {
                protocolo.Send(correo);
            }
            catch (SmtpException error)
            {
                estado = false;
                merror = error.Message.ToString();
            }

        }
        public Boolean Estado
        {
            get { return estado; }
        }
        public String mensaje_error
        {
            get { return merror; }
        }
    }
}