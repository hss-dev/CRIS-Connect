using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRISConnectDemo
{
    public partial class DemoForm : Form
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(DemoForm));
        private WebSocketServer server;
        private RootConfig rootConfig;
        private ServerConfig serverConfig;
        private int commandLinePort;
        private bool useHTTP;

        public DemoForm()
        {
            InitializeComponent();

            ActivateLogging();

            ProcessCommandLine();

            StartWebSocketServer();
        }

        private void StartWebSocketServer()
        {
            server = new WebSocketServer();
            rootConfig = new RootConfig();

            string defaultServerIP = "Any";
            int defaultServerPort = 9998;
            int defaultBufferSize = 65535;

            if (commandLinePort > 1024 && commandLinePort <= 49151)
            {
                defaultServerPort = commandLinePort;
            }

            logger.Debug("IP : " + defaultServerIP + " Port : " + defaultServerPort + " : Buffer Size : " + defaultBufferSize);

            if (useHTTP)
            {
                serverConfig = new ServerConfig
                {
                    Name = "SecureSuperWebSocket",
                    Ip = defaultServerIP,
                    Port = defaultServerPort,
                    MaxRequestLength = defaultBufferSize,
                    ReceiveBufferSize = defaultBufferSize,
                    SendBufferSize = defaultBufferSize
                };
            }
            else
            {
                logger.Debug("Using Secure Web Socket");

                string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

                appDataFolder += "/HSS/crisconnect.pfx";

                logger.Debug("Certificate path = " + appDataFolder);

                CertificateConfig Certificate;

                Certificate = new CertificateConfig
                {
                    FilePath = appDataFolder,
                    Password = "1234"
                };

                serverConfig = new ServerConfig
                {
                    Name = "SecureSuperWebSocket",
                    Ip = defaultServerIP,
                    Port = defaultServerPort,
                    MaxRequestLength = defaultBufferSize,
                    ReceiveBufferSize = defaultBufferSize,
                    SendBufferSize = defaultBufferSize,
                    Security = "tls",
                    Certificate = Certificate
                };
            }

            if (server.Setup(rootConfig, serverConfig))
            {
                server.NewMessageReceived += new SessionHandler<WebSocketSession, string>(server_NewMessageReceived);
                server.NewSessionConnected += new SessionHandler<WebSocketSession>(server_NewSessionConnected);
                server.SessionClosed += new SessionHandler<WebSocketSession, SuperSocket.SocketBase.CloseReason>(server_SessionClosed);

                if (server.Start())
                {
                    logger.Debug("Successfully started web socket server");
                }
                else
                {
                    logger.Error("Failed to start web socket server");

                    Environment.Exit(1000);
                }
            }
            else
            {
                logger.Error("Failed to set up web socket server");

                Environment.Exit(1001);
            }
        }

        private void StopWebSocketServer()
        {
            if (server != null)
            {
                logger.Debug("Sessions remaining " + server.SessionCount);

                foreach (WebSocketSession session in server.GetAllSessions())
                {
                    session.Close();
                }

                logger.Debug("Sessions still remaining " + server.SessionCount);

                server.Stop();

                logger.Debug("Web socket server closed");
            }
        }

        private void server_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            logger.Debug("Connection closed : " + session.SessionID);
        }

        private void server_NewSessionConnected(WebSocketSession session)
        {
            logger.Debug("Received connection : " + session.SessionID);

            foreach (var item in session.Items)
            {
                logger.Debug(item);
            }
        }

        private void server_NewMessageReceived(WebSocketSession session, string value)
        {
            logger.Debug("Message received : " + value);

            this.Invoke((MethodInvoker)delegate
            {
                incomingTextBox.Text = value;

                try
                {
                    Command command = JsonConvert.DeserializeObject<Command>(value);

                    if (command.command.Equals("EVENT"))
                    {
                        implementationTextBox.Text = "Maps to \"SHOW_REPORT\"";
                    }
                    else if (command.command.Equals("RELEASE"))
                    {
                        implementationTextBox.Text = "Maps to \"CLEAR_DISPLAY\"";
                    }
                }
                catch (JsonSerializationException jse)
                {
                    logger.Error("Failed to deserialize JSON", jse);
                }
            });
        }

        private void SendData(string data)
        {
            if (data != null && server.SessionCount > 0)
            {
                foreach (WebSocketSession session in server.GetAllSessions())
                {
                    if (session != null)
                    {
                        session.Send(data);
                    }
                    else
                    {
                        logger.Debug("Null session");
                    }
                }
            }
            else
            {
                logger.Debug("Attempt to send null data");
            }
        }

        private void DemoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
            {
                StopWebSocketServer();

                logger.Debug("WebsocketServer stopped, forcing garbage collection");

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void ActivateLogging()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string logPath = appDataFolder + "/HSS/PACSRISIntegerationDemo/demo.log";
            var layout = new PatternLayout("%-4timestamp %date{ISO8601} [%thread] %-5level %logger - %message%newline");
            var appender = new FileAppender { File = logPath, Layout = layout };
            var consoleAppender = new ConsoleAppender { Layout = layout, Threshold = Level.Debug };

            appender.ActivateOptions();

            consoleAppender.ActivateOptions();

            layout.ActivateOptions();

            BasicConfigurator.Configure(appender);

            Hierarchy logHierarchy = (Hierarchy)log4net.LogManager.GetRepository();
            Logger rootLogger = logHierarchy.Root;

            rootLogger.AddAppender(consoleAppender);
        }

        private void showRequestButton_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();

            list.Add("<Add accession number>");

            Command command = new Command() { command = "DISPLAY", accession = list};

            mapToJsonPayloadTextBox.Text = JsonConvert.SerializeObject(command, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore});
        }

        private void clearDisplayButton_Click(object sender, EventArgs e)
        {
            Command command = new Command() { command = "CLEAR" };

            mapToJsonPayloadTextBox.Text = JsonConvert.SerializeObject(command, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }

        private void ProcessCommandLine()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Contains("-port"))
            {
                int portIndex = Array.FindIndex(args, arg => arg.Equals("-port"));

                portIndex++;

                if (portIndex < args.Length)
                {
                    try
                    {
                        commandLinePort = int.Parse(args[portIndex]);
                    }
                    catch (FormatException fe)
                    {
                        logger.Error("Invalid port number (using default)", fe);
                    }
                }
            }

            if (args.Contains("-http"))
            {
                useHTTP = true;
            }
        }

        private void sendToCrisReportingButton_Click(object sender, EventArgs e)
        {
            if (mapToJsonPayloadTextBox.TextLength > 0)
            {
                SendData(mapToJsonPayloadTextBox.Text);
            }
        }

    }
}
