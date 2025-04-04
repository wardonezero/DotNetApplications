using System.Diagnostics;

int result = Fibonacci(6);
Console.WriteLine(result);

Console.WriteLine("This message is readable by the end user.");
Trace.WriteLine("This is a trace message when tracing the app.");
Debug.WriteLine("This is a debug message just for developers.");

bool errorFlag = false;
Trace.WriteIf(errorFlag, "Error in AppendData procedure. ");
Debug.WriteLineIf(errorFlag, "Transaction abandoned.");
Trace.Write("Invalid value for data request");

static int Fibonacci(int n)
{
    Debug.WriteLine($"Entering {nameof(Fibonacci)} method");
    Debug.WriteLine($"We are looking for the {n}th number");
    int n1 = 0;
    int n2 = 1;
    int sum;

    //for (int i = 2; i < n; i++)
    for (int i = 2; i <= n; i++)
    {
        sum = n1 + n2;
        n1 = n2;
        n2 = sum;
        Debug.WriteLineIf(sum > 0, $"sum is {sum}, n1 is {n1}, n2 is {n2}");
    }
    Debug.Assert(n2 == 5, "The return value is not 5 and it should be.");
    return n == 0 ? n1 : n2;
}