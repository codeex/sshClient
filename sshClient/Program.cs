using Renci.SshNet;
using System;
using System.IO;
using CommandLine;

namespace sshClient
{
    class Program
    {

        static int exeResult = 0;
        //static string sourceDir = @"C:\Users\admin\Downloads\";
       static int Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    try
                    {
                        //解析host
                        var userName = "root";
                        var host = o.Host;
                        var port = 22;
                        if (o.Host?.Contains("@") ?? false)
                        {
                            var arrHost = o.Host.Split("@");
                            userName = arrHost[0];
                            host = arrHost[1];
                            if (host.Contains(":"))
                            {
                                var arrPort = host.Split(":");
                                host = arrPort[0];
                                int.TryParse(arrPort[1], out port);
                            }
                        }

                        //执行scp
                        Console.WriteLine("start sshclient ...");
                        var connectionInfo = new ConnectionInfo(host,port,
                                                    userName,
                                                    new PasswordAuthenticationMethod(userName, o.Password));
                        var firstZip = "";
                        using (var client = new SftpClient(connectionInfo))
                        {
                            Console.WriteLine($"connecting {host} ...");
                            client.Connect();
                            if (client.IsConnected)
                            {
                                Console.WriteLine($"connect {host} success");
                                var file = new FileInfo(o.Source);

                                if (!file.Exists)
                                {
                                    Console.WriteLine($"file [{o.Source}]  dont exist.");
                                }
                                else
                                {
                                    Console.WriteLine($"local file [{o.Source}]  exist.");
                                }
                                
                                firstZip = file.Name;
                                using (var stream = File.Open(file.FullName, FileMode.Open))
                                {
                                    var sep = o.Dest.EndsWith("/") ? "" : "/";
                                    var path = $"{o.Dest}{sep}{firstZip}";
                                    client.UploadFile(stream, path, true);
                                    Console.WriteLine($"file [{o.Source}]  upload success,remote path is {path}.");
                                }

                                Console.WriteLine($"upload end.");                                
                                
                            }
                            else
                            {
                                Console.WriteLine($"connect {host} failed");
                            }
                            client.Disconnect();

                        }

                        //执行cmd
                        if (string.IsNullOrEmpty(o.Cmd))
                        {
                            Console.WriteLine("Not shell execute.");
                        }
                        else
                        {
                            Console.WriteLine($"Start shell: {o.Cmd}");
                            var cmd = o.Cmd.Replace(";", "\r\n");                            
                            using (SshClient client = new SshClient(connectionInfo))
                            {
                                client.Connect();
                                client.ErrorOccurred += Client_ErrorOccurred;
                                var result = client.RunCommand(cmd);

                                var sr = result.Result?.Replace("\r", "\r\n").Replace("\n", "\r\n").Replace("\b", "");
                                var err = result.Error?.Replace("\r", "\r\n").Replace("\n", "\r\n").Replace("\b", "");

                                if (result.ExitStatus == 0)
                                {
                                    Console.WriteLine($"shell execute failed, result:{sr},status:{result.ExitStatus},error:");
                                    Console.WriteLine(err);
                                    exeResult = result.ExitStatus;
                                }
                                else
                                {
                                    
                                    Console.WriteLine($"shell execute failed, result:{sr},status:{result.ExitStatus},error:");                                   
                                    Console.WriteLine(err);
                                    exeResult = result.ExitStatus;
                                }
                                client.ErrorOccurred -= Client_ErrorOccurred;
                                client.Disconnect();
                            }
                            Console.WriteLine($"end shell {exeResult}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"exec error: {ex.Message}");
                        exeResult = -1;
                    }
                });
            

            return exeResult;
        }

        private static void Client_ErrorOccurred(object sender, Renci.SshNet.Common.ExceptionEventArgs e)
        {
            Console.WriteLine($"exec sehll error: {e.Exception}");
            exeResult = -1;
        }
    }
}
