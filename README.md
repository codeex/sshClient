# sshClient
The tool is ssh and scp ,may use jenkins for windows script

# useage
```shell
sshClient 1.0.0
Copyright (C) 2021 sshClient

ERROR(S):
  Required option 'p, password' is missing.
  Required option 's, source' is missing.
  Required option 'd, dest' is missing.

  -p, --password    Required. remote host password

  -s, --source      Required. local windows dir path,include filename

  -d, --dest        Required. remote host destination dir path

  -c, --cmd         optional , remote host shell script

  --help            Display this help screen.

  --version         Display version information.

  value pos. 0      remote host, eg: root@192.168.0.1:2200
```

# example
```sehll
./sshClient root@192.168.0.150 -p xa123@150.com -s "D:\work2021\sshClient\sshClient\bin\Debug\netcoreapp3.1\Microsoft.Extensions.Configuration.dll" -d /usr/dotnet -c "cd /usr/dotnet;mv Microsoft.Extensions.Configuration.dll sshclient-test.dll"

```

# Thanks
1. SSH.NET
2. CommandLineParser

# site
access [webmote site](https://webmote.blog.csdn.net)
weixin: webmote31
qq:121941663

