using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace j11record
{
    public partial class Form1 : Form
    {
        private Thread thr;
        static SynchronizationContext SyncContext = null;
        private FileStream stream, stream_in, stream_out;
      
        public Form1()
        {
            InitializeComponent();
            SyncContext = SynchronizationContext.Current;
        }

        private void button_start_Click(object sender, EventArgs e)
        {
           
            // Creating and initializing threads
            thr = new Thread(new ThreadStart(StartClient));
            thr.Start();

        }
        private void Settext(object state)
        {
            this.textBox1.Text = state.ToString();
        }

        private void Stop_Click(object sender, EventArgs e)
        {

            Console.WriteLine("Thread is abort");

            // Abort thr thread
            // Using Abort() method
            thr.Abort();
            Thread.Sleep(10);
           
            stream.Close();

          //  postprocess();

            

        }

        // Non-Static method

        void StartClient()
        {
            byte[] bytes = new byte[8192];
          
            String infile = "j11dump.log";
            stream = new FileStream(infile, FileMode.Create);




            try
            {
                // Connect to a Remote server
                // Get Host IP Address that is used to establish a connection
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                // If a host has multiple addresses, you will get a list of addresses
                IPHostEntry host = Dns.GetHostEntry("127.0.0.1");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 19021);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    // Connect to Remote EndPoint
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());
                    SyncContext.Post(Settext, "Socket connected, Dumping to j11dump.log");
                    if (false)
                    {
                        // Encode the data string into a byte array.
                        byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                        // Send the data through the socket.
                        int bytesSent = sender.Send(msg);
                    }
                    int total_bytes = 0;
                    int has_error = 0;
                    int start_segment_offset = -1;
                    int display_freq = 0;
                    while (total_bytes <10*1000*1000)
                    {

                        // Receive the response from the remote device.
                        int bytesRec = sender.Receive(bytes);
                      
                        stream.Write(bytes, 0, bytesRec);
                        total_bytes += bytesRec;
                        display_freq++;
                        if (display_freq >100)
                        {
                            SyncContext.Post(Settext, "RX Bytes:" + total_bytes);
                            display_freq = 0;
                        }
                        

                        Thread.Sleep(1);
                       
                    }

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                 

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    SyncContext.Post(Settext, ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                    SyncContext.Post(Settext, "SocketException: " + se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    SyncContext.Post(Settext, e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                SyncContext.Post(Settext, e.ToString());
            }
            SyncContext.Post(Settext, "j11dump.log file saved");
            stream.Close();
        }

        void button_sendcmd_Click(object sender, EventArgs e)
        {
           
             
                // Connect to a Remote server
                // Get Host IP Address that is used to establish a connection
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                // If a host has multiple addresses, you will get a list of addresses
                IPHostEntry host = Dns.GetHostEntry("127.0.0.1");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 19021);

                // Create a TCP/IP  socket.
                Socket sender2 = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    // Connect to Remote EndPoint
                    sender2.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender2.RemoteEndPoint.ToString());
                    SyncContext.Post(Settext, "Socket connected, Sending");
                    if (true)
                    {
                        // Encode the data string into a byte array.
                        byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                        // Send the data through the socket.
                        int bytesSent = sender2.Send(msg);
                    }
                   

                    // Release the socket.
                    sender2.Shutdown(SocketShutdown.Both);
                    sender2.Close();



                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    SyncContext.Post(Settext, ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                    SyncContext.Post(Settext, "SocketException: " + se.ToString());
                }
                catch (Exception e2)
                {
                    Console.WriteLine("Unexpected exception : {0}", e2.ToString());
                    SyncContext.Post(Settext, e2.ToString());
                }

           
           

        }

        private void postprocess()
        {
            String str1 = DateTime.Now.Hour.ToString().PadLeft(2, '0')
            + DateTime.Now.Minute.ToString().PadLeft(2, '0') +
            DateTime.Now.Second.ToString().PadLeft(2, '0');

            String infile = "j11dump_" + str1 + ".raw";
            stream = new FileStream("j11dump.log", FileMode.Open, FileAccess.Read);
            stream_in = new FileStream(infile, FileMode.Create);
           

            int start_fp = 0;
            byte[] bytes = new byte[10 * 1000 * 1000];

            //简化起见，一次性读完，规定文件大小<10M
            int total_reads = stream.Read(bytes, start_fp, 10 * 1000 * 1000);
            if (total_reads == 0)
            {
                return;
            }
            //寻找第一个01
            int index = 0;
 
            int sample_len = 384;
            byte[] arr_segment = new byte[384];
            byte[] arr_in = new byte[192];
            
            for (index = 0; index < sample_len; index++)
                arr_segment[index] = 0;

            index = 0;
            int start_segment_offset = -1;
            int end_segment_offset = -1;
            for (index =0;index <total_reads;index++)
            {
                if (bytes[index]==0x01)
                {
                    start_segment_offset = index;
                    break;
                }
            }
            if (start_segment_offset == -1) return;
            int segment_idx = 0;


            {
                //这几句是测试，发现a1<<4和a1*16结果不同
                byte a1 = 9; byte a2 = 9;
                byte a3 = Convert.ToByte(a1 * 16 + a2);

                Console.WriteLine(" a1={0},a2={1},a3={2}", a1, a2, a3);
            }

            for (index = start_segment_offset+1; index < total_reads; index++)
            {
                
                if (bytes[index] != 0x01)
                {
                    segment_idx = index - start_segment_offset - 1;
                    if (segment_idx <sample_len)
                         arr_segment[segment_idx] = bytes[index];
                     else
                    {
                        String str = String.Format("wrong data:%d,offset=%d", bytes[index], index);

                        Settext(str);
                       
                    }
                }
                else
                {
                    //再次找到了0x01
                    end_segment_offset = index;
                    if ((end_segment_offset - start_segment_offset - 1) != sample_len)
                    {
                        String str = String.Format(  "数据不匹配,0x%0x-0x%x !=%d,退出", end_segment_offset, start_segment_offset,sample_len);

                        Settext(str);
                        break;
                    }
                    else
                    { 

                        for (int j = 0; j < sample_len / 2; j++)
                        {
                            byte a1 = Convert.ToByte(arr_segment[j * 2] - 0xa0);
                            byte a2 = Convert.ToByte(arr_segment[j * 2 + 1] - 0x80);
                            byte a3 = Convert.ToByte((a1 * 16 + a2) & 0xff);
                            arr_in[j] = a3;

                        }
                        stream_in.Write(arr_in, 0, sample_len / 2);

                        start_segment_offset = end_segment_offset; 

                    } 

                }







            }
            stream_in.Close();
          
            stream.Close();

            Settext("保存为raw 文件:" + infile);



        }

        private void button_processfile_Click(object sender, EventArgs e)
        {
         

        }
    }
     

}
