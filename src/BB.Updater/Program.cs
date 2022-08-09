namespace BB.Updater;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        bool mutexWasCreated;
        Mutex m = new Mutex(true, "Mutex.UPDATE", out mutexWasCreated);
        if (mutexWasCreated)
        {
            Application.Run(new MainForm(args));
            m.ReleaseMutex();
        }
    }
}