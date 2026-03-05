public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // creating return array
        double[] returnArray = new double[length];

        // entering for loop. This loop will start with one and end when i is equal the length provided
        for (int i = 1; i <= length; i++)
        {
            // getting 0 based index for array and then adding the requested number times the current i variable
            returnArray[i - 1] = number * i;
        }

        return returnArray;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // getting point in list where cut off happens
        int cutoffPoint = data.Count - amount;

        // creating temp list to store information

        /* entering for loop that will take the current element out of the list and store it in a temporary element. 
        *  Once the element is removed, then it is appended to the end of the list. 
        */
        for (int i = 0; i < cutoffPoint; i++)
        {
            int tempNum = data[0];
            data.Remove(tempNum);
            data.Add(tempNum);
        }
    }
}
