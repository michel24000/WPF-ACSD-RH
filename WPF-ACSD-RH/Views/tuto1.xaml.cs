using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WPF_ACSD_RH.ViewModels;

namespace WPF_ACSD_RH.Views
{
    /// <summary>
    /// Logique d'interaction pour tuto1.xaml
    /// </summary>
    public partial class tuto1 : Window
    {
        public tuto1()
        {
            InitializeComponent();
            //DataContext = new LectureVideoTutoViewModel();
            webBrowser.NavigateToString(@"<!DOCTYPE html>
                <html>
                <head>
                    <meta http-equiv='Content-Type' content='text/html; charset=unicode' />
                    <meta http-equiv='X-UA-Compatible' content='IE=10' /> 
                    <title>coucou</title>                 
                </head>
                <body style='display: flex; justify-content: center;'>
                    <video autoplay='autoplay' controls preload='metadata' heigth='780' width='880'>
                        <source src='https://www.centres-sociaux-douai.fr/Application/tuto/video1.mp4' type='video/mp4'/>
                    </video>                                  
                </body>
                </html>");
        }
    }
}
