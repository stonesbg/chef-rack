using McMaster.Extensions.CommandLineUtils;
using System;

namespace chef_rack.cli
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "chef-rack",
                Description = "Chef rack is used for the management of multiple chef server and switching its context",
            };

            app.HelpOption();

            app.Command("init", initCmd =>
            {
                initCmd.OnExecute(() =>
                {
                    Console.WriteLine($"Initialize Chef-Reck configuraiton");

                    return 0;
                });
            });

            app.Command("toggle", toggleCmd =>
            {
                var optionServer = toggleCmd.Option("-s|--server <SERVER>", "Chef Server to connect to", CommandOptionType.SingleValue);

                toggleCmd.OnExecute(() =>
                {

                    var server = optionServer.HasValue() ? optionServer.Value() : string.Empty;

                    var chefServer = Environment.GetEnvironmentVariable("CHEFRACK_SERVER", EnvironmentVariableTarget.User);

                    Environment.SetEnvironmentVariable("CHEFRACK_SERVER", server,EnvironmentVariableTarget.User);
                    Console.WriteLine($"{server} is now the default Chef Server");

                    return 0;
                });
            });

            app.Command("config", configCmd =>
            {
                configCmd.Command("add", configAddCmd =>
                {
                    var serverConfigAdd = configAddCmd.Option("-s|--server <SERVER>", "Chef Server to connect to", CommandOptionType.SingleValue);
                    var userConfigAdd = configAddCmd.Option("-s|--server <SERVER>", "Chef Server to connect to", CommandOptionType.SingleValue);

                    configAddCmd.OnExecute(() =>
                    {
                        var server = serverConfigAdd.HasValue() ? serverConfigAdd.Value() : string.Empty;

                        Console.WriteLine($"Initialize server: {server}");

                        return 0;
                    });
                });

                configCmd.Command("remove", configRemoveCmd =>
                {
                    var serverConfigAdd = configRemoveCmd.Option("-s|--server <SERVER>", "Chef Server to connect to", CommandOptionType.SingleValue);

                    configRemoveCmd.OnExecute(() =>
                    {
                        var server = serverConfigAdd.HasValue() ? serverConfigAdd.Value() : string.Empty;

                        Console.WriteLine($"Remove server from configuraiton: {server}");

                        return 0;
                    });
                });

                configCmd.Command("list", configListCmd =>
                {
                    configListCmd.OnExecute(() =>
                    {
                        Console.WriteLine($"List of servers are: Many");

                        return 0;
                    });
                });

                configCmd.OnExecute(() =>
                {

                    Console.WriteLine("Specify a sub command");
                    app.ShowHelp();

                    return 1;
                });
            });

            app.OnExecute(() =>
            {
                Console.WriteLine("Specify a sub command");
                app.ShowHelp();

                return 1;
            });

            return app.Execute(args);
        }
    }
}
