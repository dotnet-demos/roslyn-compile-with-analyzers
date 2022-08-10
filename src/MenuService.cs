using EasyConsole;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System;

namespace ConsoleApp
{
    internal class MenuService : BackgroundService
    {
        public CompileWithAnalyzersOption Option1 { get; init; }
        public CompileOption compileOption { get; init; }
        public MenuService(CompileOption compileOption, CompileWithAnalyzersOption opt1)
        {
            Option1 = opt1;
            this.compileOption = compileOption;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var menu = new Menu()
                .Add("Compile", async (token) => await compileOption.Execute())
                .Add("Compile with Analyzers", async (token) => await Option1.Execute())
                .AddSync("Exit", () => Environment.Exit(0));
            await menu.Display(CancellationToken.None);
            await base.StartAsync(stoppingToken);
        }
    }
}