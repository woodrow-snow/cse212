using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Create a Queue with values of [a,1], [b,2], [c,3]
    // Expected Result: a, b, c
    // Defect(s) Found: The dequeue function was not removing the position it was getting. Added code to remove the highest priority item once it has been gotten.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("a", 3);
        priorityQueue.Enqueue("b", 2);
        priorityQueue.Enqueue("c", 1);

        string[] expectedResults = ["a", "b", "c"];

        int i = 0;
        while (priorityQueue.Length > 0)
        {
            if (i >= expectedResults.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var item = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResults[i], item);
            i++;
        }
    }

    [TestMethod]
    // Scenario: Creating a queue with multiple items of the same priority value
    // Expected Result: a, b, c, d
    // Defect(s) Found: none
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("a", 3);
        priorityQueue.Enqueue("b", 2);
        priorityQueue.Enqueue("c", 1);
        priorityQueue.Enqueue("d", 2);

        string[] expectedResults = ["a", "b", "c", "d"];

        int i = 0;
        while (priorityQueue.Length > 0)
        {
            if (i >= expectedResults.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var item = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResults[i], item);
            i++;
        }
    }

    // Add more test cases as needed below.
    [TestMethod]
    // Scenario: Getting items from an empty queue
    // Expected Result: InvalidOperationException
    // Defect(s) Found: none
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exepction should have been thrown.");
        }
        // need to edit
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                 e.GetType(), e.Message)
            );
        }
    }
}