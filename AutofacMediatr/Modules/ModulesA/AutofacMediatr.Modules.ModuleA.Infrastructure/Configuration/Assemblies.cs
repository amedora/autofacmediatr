using AutofacMediatr.Modules.ModuleA.Application.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        // アプリケーションのアセンブリ
        public static readonly Assembly Application = typeof(CommandBase<>).Assembly;
        // インフラストラクチャのアセンブリ
        public static readonly Assembly Infrastructure = typeof(ModuleAUnitOfWork).Assembly;
    }
}
