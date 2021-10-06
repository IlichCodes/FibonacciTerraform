public class fibonacci
    {    
        public int[] FindInFibonacci(int inputNumber)
        {            
            var firstNumber = 0;
            var secondNumber = 1;
            var nextFibonacciNo = firstNumber + secondNumber;
            while (nextFibonacciNo <= inputNumber)
            {
                firstNumber = secondNumber;
                secondNumber = nextFibonacciNo;
                nextFibonacciNo = firstNumber + nextFibonacciNo;
            }
            var closestNumber = nextFibonacciNo - inputNumber < inputNumber - secondNumber ? nextFibonacciNo : secondNumber;
            if (closestNumber == secondNumber)
            {
                return new int[] { closestNumber, firstNumber, nextFibonacciNo };               
            }
            return new int[] { closestNumber, secondNumber, nextFibonacciNo + secondNumber};                        
        }
    }