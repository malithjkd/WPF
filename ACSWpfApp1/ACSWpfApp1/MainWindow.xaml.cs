using System;
using ACS.SPiiPlusNET;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Reflection;
using System.Windows.Threading;


namespace ACSWpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }



        // Globle ref
        // create communication object
        //Api channel = new Api();

        Api Ch = new Api();
        int connection_button_status = 0;

        private void btn_connect_Click(object sender, RoutedEventArgs e)
        {
            //Connection_status_textbox.Text = " ";
            // page 39
            // Open Ethernet with TCP protocol communication with the controller 
            // IP address: 10.0.0.100
            if (connection_button_status == 0)
            {
                try
                {
                    int port = (int)EthernetCommOption.ACSC_SOCKET_STREAM_PORT;
                    Ch.OpenCommEthernetTCP("10.0.0.100", port);

                    textBoxToUpdate.Text = "Connection establisthed";
                    btn_connect.Content = "Disconnect";
                    connection_button_status = 2;

                }
                catch (Exception ex)
                {
                    textBoxToUpdate.Text = ex.Message;
                }
            }
            else
            {
                try
                {
                    Ch.CloseComm();

                    textBoxToUpdate.Text = "Disconnected";
                    btn_connect.Content = "Connect";
                    connection_button_status = 0;

                }
                catch (Exception ex)
                {
                    textBoxToUpdate.Text = ex.Message;
                }
            }


        }
        // motor enable
        // page 127

        //axis 0
        int motor_0_enable_state = 0;
        int timeout_enable = 5000;
        private void btn_enable_motor_axis_0_Click(object sender, RoutedEventArgs e)
        {
            if (motor_0_enable_state == 0)
            {

                // Enable axis 0
                Ch.Enable(Axis.ACSC_AXIS_0);
                //Wait till axis 0 is enabled during 5 sec
                Ch.WaitMotorEnabled(Axis.ACSC_AXIS_0, 1, timeout_enable);
                motor_0_enable_state = 1;
                // update button lable 
                btn_enable_motor_0.Content = "Disable";

            }
            else
            {
                // Enable axis 0
                Ch.Disable(Axis.ACSC_AXIS_0);
                //Wait for motor 0 to disable for 5 sec.
                Ch.WaitMotorEnabled(Axis.ACSC_AXIS_0, 0, timeout_enable);
                motor_0_enable_state = 0;
                // update button lable 
                btn_enable_motor_0.Content = "Enable";
            }
        }

        // axis 1
        int motor_1_enable_state = 0;
        private void btn_enable_motor_axis_1_Click(object sender, RoutedEventArgs e)
        {
            if (motor_1_enable_state == 0)
            {
                int timeout = 5000;
                // Enable axis 1
                Ch.Enable(Axis.ACSC_AXIS_1);
                //Wait till axis 1 is enabled during 5 sec
                Ch.WaitMotorEnabled(Axis.ACSC_AXIS_0, 1, timeout_enable);
                motor_1_enable_state = 1;
                // update button lable 
                btn_enable_motor_1.Content = "Disable";

            }
            else
            {
                // Enable axis 1
                Ch.Disable(Axis.ACSC_AXIS_1);
                //Wait for motor 1 to disable for 5 sec.
                Ch.WaitMotorEnabled(Axis.ACSC_AXIS_0, 0, timeout_enable);
                motor_1_enable_state = 0;
                // update button lable 
                btn_enable_motor_1.Content = "Enable";
            }

        }
        // Page 67 Run buffer

        private void btn_update_buffer_Click(object sender, RoutedEventArgs e)
        {
            String file = "C:\\Users\\mj.j\\OneDrive - PBA Systems Pte. Ltd\\GitHub\\stability\\uplode_buffer.prg";
            // Opens a file to load
            Ch.LoadBuffersFromFile(file);

            
        }
        // Page 70 Run buffer

        private void Buffer_exc_Click(object sender, RoutedEventArgs e)
        {
            // The method compiles ACSPL+ program in buffer 
            Ch.CompileBuffer(ProgramBuffer.ACSC_BUFFER_10);
            // Run the buffer 
            Ch.RunBuffer((ProgramBuffer)10, null);

        }

        private void btn_homing_0_Click(object sender, RoutedEventArgs e)
        {
            // Run the buffer 
            Ch.RunBuffer((ProgramBuffer)4, null);
        }

        private void btn_homing_1_Click(object sender, RoutedEventArgs e)
        {
            // Run the buffer 
            Ch.RunBuffer((ProgramBuffer)5, null);
        }
    }


    
}
// End of file MainWindow.xaml.cs