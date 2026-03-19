namespace AutomationPracticeDemo.Tests.Utils;

public static class WaitConfig
{
    // Global explicit-wait timeout used across page objects.
    // CI runners are slower and the demo site can be rate-limited.
    public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(60);

    // Polling interval for WebDriverWait.
    public static readonly TimeSpan PollingInterval = TimeSpan.FromMilliseconds(250);
}
