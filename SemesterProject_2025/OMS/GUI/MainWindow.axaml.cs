using Avalonia.Controls;
using OMS;

namespace OMS.GUI
{

    public partial class MainWindow : Window
    {
        private Process.Process process_instance;

        public MainWindow()
        {
            InitializeComponent();

            // . 
            process_instance = Process.Process.GetInstance();


        }



    }

}