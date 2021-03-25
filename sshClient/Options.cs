using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace sshClient
{
    public class Options
    {
        /// <summary>
        /// root@111.222.1.1:999
        /// </summary>
        [Value(0)]
        public string Host { get; set; }

        [Option('p', "password", Required = true, HelpText = "remote host password")]
        public string Password { get; set; }

        [Option('s', "source", Required = true, HelpText = "local windows dir path,include filename")]
        public string Source { get; set; }

        [Option('d', "dest", Required = true, HelpText = "remote host destination dir path")]
        public string Dest { get; set; }

        [Option('c', "cmd", Required = false, HelpText = "remote host shell script")]
        public string Cmd { get; set; }

    }
}
