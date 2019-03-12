# Building a window service for automatically clean up old files
An automatically window service for cleanup old files in specific folder by using .Net, Topshelf, Quartz.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine. See deployment for notes on how to install service on a live system.

### Prerequisites

- Visual Studio 2017
- .Net Framework 4.0

### Installing on local machine

A step by step series of examples that tell you how to get a development env running

create new folder that store source code

```
mkdir RemoveFileService
```

Clone source code from github

```
git clone https://github.com/dangvu1986/automatic-remove-file-window-service.git
```

Open source code in Visual Studio, F5 to run.

### Configuration

- You can change folder path, file extension and time limit to cleanup in app.config 
- You can schedule your service run daily, monthly, yearly or specific time by update ScheduleQuartzJob in Program.cs. 
Read more [here](https://www.quartz-scheduler.net/)

## Install service in live server.

- Build source code with Release mode
- Copy build release folder to live server.
- Open cmd, go to directory that contains build source (cd)
- Install service. Read more [here](http://docs.topshelf-project.com/en/latest/overview/commandline.html)

```
[Your-Service-Name].exe install -servicename:AwesomeService –autostart
```

## Built With

* [Topshelf](http://docs.topshelf-project.com/en/latest/index.html) - An easy service hosting framework for building Windows services using .NET
* [Quartz](https://www.quartz-scheduler.net/) - Job scheduling framework
* [Topshelf.Quartz](https://github.com/dtinteractive/Topshelf.Integrations/) - Topshelf Integration with Quartz

## Authors

* **Dang Vu** - *Freelancer* - [dangvu1986](https://github.com/dangvu1986)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
