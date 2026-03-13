using System.Diagnostics;
using System.Reflection;

/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Checking to see if program sets a max size
        // Expected Result: program should set max_size to 10 if less than or equal to 0
        Console.WriteLine("Test 1");

        // Defect(s) Found: none
        var cs = new CustomerService(-1);
        Console.WriteLine(cs);

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Checking if Enqueue function works
        // Expected Result: Enqueue should add a new customer
        Console.WriteLine("Test 2");

        // Defect(s) Found: none

        var queue = new CustomerService(3);
        // getting customer 1
        queue.AddNewCustomer();
        // getting customer 2
        queue.AddNewCustomer();
        // getting customer 3
        queue.AddNewCustomer();

        var result = "[size=3 max_size=3 => John (1)  : a, Jane (2)  : a, Chris (3)  : a]";

        if (result == queue.ToString())
        {
            Console.WriteLine("Expected Results Met.");
        }
        else
        {
            Console.WriteLine("Expetions were not met.");
        }

        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below

        // Test 3
        // Scenario: Checking when queue is full
        // Expected Result: program should throw an error
        Console.WriteLine("Test 3");

        // Defect(s) Found: check for add customer did not have equal sign causing 

        queue.AddNewCustomer();
        Console.WriteLine(queue);

        Console.WriteLine("=================");

        // Test 4
        // Scenario: Testing the ServeCustomer Function to ensure customer is being returned correctly
        // Expected Result: Customer information should be printed out.
        Console.WriteLine("Test 4");

        // Defect(s) Found: The person at the front of the queue was being removed before they could be saved cauing the second in line to be pulled
        Console.Write("Before Service: ");
        Console.WriteLine(queue);

        queue.ServeCustomer();

        Console.Write("After Service: ");
        Console.WriteLine(queue);

        Console.WriteLine("=================");

        // Test 5
        // Scenario: Testing When queue is empty
        // Expected Result: error message should be displayed
        Console.WriteLine("Test 5");

        // Defect(s) Found: ServeCustomer Function didn't have code to check if the queue was empty. Added if statement to resolve issue.

        // emptying queye
        Console.WriteLine("Clearing the queue...");
        queue.ServeCustomer();
        queue.ServeCustomer();

        // displaying empty queue
        Console.Write("Ensuring Queue is empty: ");
        Console.WriteLine(queue);

        // serving with empty queue
        Console.WriteLine("Serving the next customer for the empty queue...");
        queue.ServeCustomer();

        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        if (_queue.Count == 0)
        {
            Console.WriteLine("There is no one in the queue to service!");
        }
        else
        {
            var customer = _queue[0];
            _queue.RemoveAt(0);
            Console.WriteLine(customer);
        }
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}