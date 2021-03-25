# sshClient
The tool is ssh and scp ,may use jenkins for windows script

中文描述：这款工具主要运行在Windows上，通过ssh和sftp 向远程linux主机发送文件和运行shell命令。
          主要用途应该是自动化运维，我是在Jenkins中运行它，以便把windows slave节点的文件拷贝到Linux中，然后再执行一些指定的脚本，比如解压等。
          
          其主要依赖是.net core 3.1运行环境，其他的应该均不需要。

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

