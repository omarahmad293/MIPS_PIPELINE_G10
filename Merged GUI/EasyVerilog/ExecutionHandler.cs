using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace EasyVerilog
{
    public static class ExecutionHandler
    {
        static string FullCompilerPath = @Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\iverilog\bin\iverilog.exe";
        static string FullSimulatorPath = @Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\iverilog\bin\vvp.exe";
        static string FullWaveViewerPath = @Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\iverilog\gtkwave\bin\gtkwave.exe";
        static string FullCompilerOutputPath = @Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\Output\";
        static string FullSimulatorOutputPath = @Directory.GetCurrentDirectory()+@"\";

        static readonly string FullCompilerOutputName = "output.vvp";
        static readonly string FullSimulatorOutputName = "dump.vcd";

        /// <summary>
        /// This will compile all the files found in OpenedFiles list.
        /// You must open all the files you want to compile before using this.
        /// </summary>
        public static void CompileAll(out int exitCode, out string errorsOutput)
        {
            //This is the object used to start the process
            ProcessStartInfo startInfo = new ProcessStartInfo();

            //Constructing compiler arguments
            string Arguments = "-o " + FullCompilerOutputPath + FullCompilerOutputName;

            for (int i = 0; i < OpenedFilesHandles.OpenedFilesCount; i++)
            {
                Arguments += " " + OpenedFilesHandles.GetFullName(i);
            }

            //Put in the object arguments as a full string
            startInfo.Arguments = Arguments;

            //The excutable to run, including the comlpete path
            startInfo.FileName = FullCompilerPath;

            //No window for compilation
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;

            //Redirect errors to me
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;

            using (Process process = Process.Start(startInfo))
            {
                StreamReader errors = process.StandardError;

                process.WaitForExit();

                errorsOutput = errors.ReadToEnd();
                exitCode = process.ExitCode;
            }
        }

        /// <summary>
        /// This will simulate output.vvp that exists in the Output folder
        /// </summary>
        public static void Simulate(out int exitCode, out string errorsOutput, out string output)
        {
            //This is the object used to start the process
            ProcessStartInfo startInfo = new ProcessStartInfo();

            //Constructing simulator path
            string Arguments = FullCompilerOutputPath + FullCompilerOutputName;

            //Put in the object arguments as a full string
            startInfo.Arguments = Arguments;

            //The excutable to run, including the comlpete path
            startInfo.FileName = FullSimulatorPath;

            //No window for compilation
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;

            //Redirect errors to me
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;



            using (Process process = Process.Start(startInfo))
            {
                StreamReader errors = process.StandardError;
                StreamReader outputs = process.StandardOutput;

                if(process.WaitForExit(5000))
                {
                    //Simulation ended exited normally

                }
                else
                {
                    process.Kill();
                }

                errorsOutput = errors.ReadToEnd();
                output = outputs.ReadToEnd();
                exitCode = process.ExitCode;
            }
        }

        public static void DrawWave()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();

            //Constructing wave path
            string Arguments = FullSimulatorOutputPath + FullSimulatorOutputName;

            //Put in the object arguments as a full string
            startInfo.Arguments = "-M -F " + Arguments;

            //The excutable to run, including the comlpete path
            startInfo.FileName = FullWaveViewerPath;


            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }

        }

    }
}
