using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_ACSD_RH.Models
{
    class MailSmtp
    {
        #region VARIABLES et CONSTANTES
        MailAddress fromAddress;
        MailAddress toAddress;
        private string mailCongesConnuBdd;
        private bool valide;
        private string body;
        private string reponse;
        private string dateDebut;
        private string dateFin;
        const string fromPassword = "1rhAPPLIadmin";
        const string subject = "Réponse à votre demande de congés";

        #endregion

        #region CTOR
        public MailSmtp()
        {            
        }
        #endregion

        #region public void SendtMessage(string _mailCongesConnuBdd, bool _valide, string _dateDebut, string _dateFin)
        public void SendtMessage(string _mailCongesConnuBdd, bool _valide, string _dateDebut, string _dateFin)
        {                                            
            fromAddress = new MailAddress("support-acsd@centres-sociaux-douai.fr");
            toAddress = new MailAddress(_mailCongesConnuBdd);

            if (_valide == true)
                reponse = "<div style='color: green;'>Votre demande de congés dont voici le détail : <br><br>" +
                          "- Date de début de congès : " + _dateDebut + " <br>" +
                          "- Date de fin de congès : " + _dateFin + ",<br><br>" +
                          " est validée !</div><br><br>";
            else
                reponse = "<div style='color: red;'>Votre demande de congés dont voici le détail : <br/><br/>" +
                          "- Date de début de congès : " + _dateDebut + " <br>" +
                          "- Date de fin de congès : " + _dateFin + ",<br><br>" +
                          " est refusée, veuillez contacter votre responsable de centre !</ div >";

            //génére le body !

            body = @"<html>
                        <body style='text-align: center'>
                            <a href='https://www.centres-sociaux-douai.fr' alt='site de l'ACSD' target='_blank'>
                                <img src='https://www.centres-sociaux-douai.fr/Application/Images/Authentification_logo.png' >
                            </a><br/><br/>
                            <h1>Réponse à votre demande de congés :</h1><br/><br/> " +
                            "<div style='margin: 0'>" + reponse + "</div>" +                            
                            "<p>Cet email est envoyé automatiquement, merci de ne pas y répondre.<br/>" +
                            "Pour nous contacter :<br/><br/>" +
                            "<strong>A</strong>ssociation des <strong>C</strong>entres <strong>S</strong>ociaux de <strong>D</strong>ouai<br/>" +
                            "68, rue Charles Monsarrat<br/>" +                                 
                            "59500 DOUAI CEDEX<br/>" +
                            "Tél: 03 27 71 69 44<br/>" +
                            "Mail: acsddouai@wanadoo.fr</p>" +
                            "<img src='https://www.centres-sociaux-douai.fr/Application/Images/logosSignature.jpg'>" +
                        "</body>" +
                    "</html>";          
            
            var smtp = new SmtpClient
            {
                Host = "ssl0.ovh.net",    
                Port = 587,              
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                //on ajoute la posibilitée de mettre du html dans le body 
                message.IsBodyHtml = true; 
                //on envoi le mail
                smtp.Send(message);
            }
        }
        #endregion

        #region public void SendtMessageN1(string _maildun1, string _prenom, string _nom, string _dateDebut, string _dateFin)
        public void SendtMessageN1(string _maildun1, string _prenom, string _nom, string _dateDebut, string _dateFin)
        {
            fromAddress = new MailAddress("support-acsd@centres-sociaux-douai.fr");
            toAddress = new MailAddress(_maildun1);

            reponse = "<div style='font-weight: bold;'>Un salarié de votre centre vient d'émettre une demande de congés dont voici le détail : <br><br>" +
                          "- Mr ou Mme : " + _prenom + " " + _nom +  "<br><br>" +
                          "- Date de début de congés : " + _dateDebut + " <br>" +
                          "- Date de fin de congés : " + _dateFin + ",<br><br>" +
                          "Merci de bien vouloir y porter attention ! ";

            //génére le body !
            body = @"<html>
                        <body style='text-align: center'>
                            <a href='https://www.centres-sociaux-douai.fr' alt='site de l'ACSD' target='_blank'>
                                <img src='https://www.centres-sociaux-douai.fr/Application/Images/Authentification_logo.png' >
                            </a><br/><br/>
                            <h1>Notification d'une demande de congés :</h1><br/><br/> " +
                            "<div style='margin: 0'>" + reponse + "</div><br>" +
                            "<p>Cet email est envoyé automatiquement, merci de ne pas y répondre.<br/>" +
                            "Pour nous contacter :<br/><br/>" +
                            "<strong>A</strong>ssociation des <strong>C</strong>entres <strong>S</strong>ociaux de <strong>D</strong>ouai<br/>" +
                            "68, rue Charles Monsarrat<br/>" +
                            "59500 DOUAI CEDEX<br/>" +
                            "Tél: 03 27 71 69 44<br/>" +
                            "Mail: acsddouai@wanadoo.fr</p>" +
                            "<img src='https://www.centres-sociaux-douai.fr/Application/Images/logosSignature.jpg'>" +
                        "</body>" +
                    "</html>";

            var smtp = new SmtpClient
            {
                Host = "ssl0.ovh.net",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                //on ajoute la posibilitée de mettre du html dans le body 
                message.IsBodyHtml = true;
                //on envoi le mail
                smtp.Send(message);
            }
        }
        #endregion

        #region public void SendtMessageN1Annulation(string _maildun1, string _prenom, string _nom, string _dateDebut, string _dateReprise)
        public void SendtMessageN1Annulation(string _maildun1, string _prenom, string _nom, string _dateDebut, string _dateReprise)
        {
            fromAddress = new MailAddress("support-acsd@centres-sociaux-douai.fr");
            toAddress = new MailAddress(_maildun1);

            reponse = "<div style='font-weight: bold; color: red;'>Un salarié de votre centre vient d'émettre une demande d'annulation d'un congés dont voici le détail : <br><br>" +
                          "- Mr ou Mme : " + _prenom + " " + _nom + "<br><br>" +
                          "- Date de début de congés : " + _dateDebut + " <br>" +
                          "- Date de fin de congés : " + _dateReprise + ",<br><br>" +
                          "Merci de bien vouloir y porter attention ! ";

            //génére le body !
            body = @"<html>
                        <body style='text-align: center'>
                            <a href='https://www.centres-sociaux-douai.fr' alt='site de l'ACSD' target='_blank'>
                                <img src='https://www.centres-sociaux-douai.fr/Application/Images/Authentification_logo.png' >
                            </a><br/><br/>
                            <h1>Notification d'une demande de congés :</h1><br/><br/> " +
                            "<div style='margin: 0'>" + reponse + "</div><br>" +
                            "<p>Cet email est envoyé automatiquement, merci de ne pas y répondre.<br/>" +
                            "Pour nous contacter :<br/><br/>" +
                            "<strong>A</strong>ssociation des <strong>C</strong>entres <strong>S</strong>ociaux de <strong>D</strong>ouai<br/>" +
                            "68, rue Charles Monsarrat<br/>" +
                            "59500 DOUAI CEDEX<br/>" +
                            "Tél: 03 27 71 69 44<br/>" +
                            "Mail: acsddouai@wanadoo.fr</p>" +
                            "<img src='https://www.centres-sociaux-douai.fr/Application/Images/logosSignature.jpg'>" +
                        "</body>" +
                    "</html>";

            var smtp = new SmtpClient
            {
                Host = "ssl0.ovh.net",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                //on ajoute la posibilitée de mettre du html dans le body 
                message.IsBodyHtml = true;
                //on envoi le mail
                smtp.Send(message);
            }
        }
        #endregion

        #region public void SendtTokenMessage(string _mailCongesConnuBdd)
        public void SendtTokenMessage(string _mailCongesConnuBdd)
        {
            fromAddress = new MailAddress("support-acsd@centres-sociaux-douai.fr");
            toAddress = new MailAddress(_mailCongesConnuBdd);

            reponse = "<div style='font-weight: bold; color: red;'>Vous avez cliqué sur mot de passe oublié,<br>" +
                      "<?php $token = base_convert(hash('sha256', time() . mt_rand()), 16, 36)?>" +
                      "Voici le lien pour accéder à la page de réinitialisation de votre mot de passe : <br><br>" +
                          "<a href='https://www.centres-sociaux-douai.fr/Application/telechargement_application/reinitialisation-Mdp?param=<?php echo $token?>'" +
                          "alt='demande de réinitialisation de mot de passe' target='_blank'>Je réinitialise mon mot de passe</a>" + "<br><br>";

            //génére le body !
            body = @"<html>
                        <body style='text-align: center'>
                            <a href='https://www.centres-sociaux-douai.fr' alt='site de l'ACSD' target='_blank'>
                                <img src='https://www.centres-sociaux-douai.fr/Application/Images/Authentification_logo.png' >
                            </a><br/><br/>
                            <h1>Demande de réinitialisation du mot de passe !</h1><br/><br/> " +
                            "<div style='margin: 0'>" + reponse + "</div><br>" +
                            "<p>Cet email est envoyé automatiquement, merci de ne pas y répondre.<br/>" +
                            "Pour nous contacter :<br/><br/>" +
                            "<strong>A</strong>ssociation des <strong>C</strong>entres <strong>S</strong>ociaux de <strong>D</strong>ouai<br/>" +
                            "68, rue Charles Monsarrat<br/>" +
                            "59500 DOUAI CEDEX<br/>" +
                            "Tél: 03 27 71 69 44<br/>" +
                            "Mail: acsddouai@wanadoo.fr</p>" +
                            "<img src='https://www.centres-sociaux-douai.fr/Application/Images/logosSignature.jpg'>" +
                        "</body>" +
                    "</html>";

            var smtp = new SmtpClient
            {
                Host = "ssl0.ovh.net",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                //on ajoute la posibilitée de mettre du html dans le body 
                message.IsBodyHtml = true;
                //on envoi le mail
                smtp.Send(message);
            }
        }
        #endregion
    }
}
