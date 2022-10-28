using System;

namespace HomeWork
{
    public class myHomeWork
    {       
        public static void Main(string[] args)
        {   try
            {
                Calculate();
            }
            catch (Exception ex)
            {
                if (ex.Data.Count > 0)
                    foreach (var item in ex.Data.Values)                    
                        Console.WriteLine($"В калькуляторе произошла ошибка: {item}");                                       
                else                
                    Console.WriteLine("В калькуляторе произошла ошибка");                
            }                        
        }

        public static void Calculate()
        {
            while (true) {
                try
                {
                    string? example = Console.ReadLine();
                    string[] exampleInArray;

                    if (!string.IsNullOrWhiteSpace(example))
                        exampleInArray = example.Split(" ");
                    else
                    {
                        var ex = new Exception();
                        ex.Data.Add(0, "Нет значения");
                        throw ex;
                    }
                        

                    string[] operations = { "+", "-", "*", "/" };
                    int lengthArray = exampleInArray.Length;

                    if (lengthArray == 1)
                    {
                        if (exampleInArray[0] == "стоп")
                            break;
                        else
                        {
                            var iex = new IncorrectFormatException();
                            iex.Data.Add(0, example);
                            throw iex;
                        }                            
                    }
                    else if (lengthArray == 2)
                    {
                        if (isInt(exampleInArray[0]) && isInt(exampleInArray[1]))
                        {        
                            var loex = new LostOperationException();
                            loex.dopInfo = "Какая-то важная информация!";
                            throw loex;
                        }
                        else
                        {
                            var iex = new IncorrectFormatException();
                            iex.Data.Add(0, example);
                            throw iex;
                        }
                    }
                    else if (lengthArray == 3)
                    {
                        int a;
                        int b;
                        bool isValidOperation = false;

                        if (!isInt(exampleInArray[0]))
                            throw new ParseException(exampleInArray[0]);
                        else
                            a = int.Parse(exampleInArray[0]);

                        if (!isInt(exampleInArray[2]))
                            throw new ParseException(exampleInArray[2]);
                        else
                            b = int.Parse(exampleInArray[2]);
                        
                        if (isInt(exampleInArray[1]))
                            throw new IncorrectFormatException();

                        foreach (var item in operations)
                        {
                            if (exampleInArray[1] == item)
                            {
                                isValidOperation = true;
                            }
                        }

                        if (!isValidOperation)
                            throw new BadOperationException(exampleInArray[1]);
                        else
                        {
                            switch (exampleInArray[1])
                            {
                                case "+":
                                    Sum(a, b);
                                    break;
                                case "-":
                                    Sub(a, b);
                                    break;
                                case "*":
                                    Mul(a, b);
                                    break;
                                case "/":
                                    Div(a, b);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        var iex = new IncorrectFormatException();
                        iex.Data.Add(0, example);
                        throw iex;
                    }
                }
                // CASE 1
                catch (LostOperationException loex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Укажите в выражении оператор: +, -, *, /");
                    // Использования поля dopInfo класса LostOperationException
                    Console.WriteLine(loex.dopInfo);
                }
                // CASE 2
                catch (BadOperationException bex)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    // Использования поля message класса BadOperationException
                    Console.WriteLine("Я пока не умею работать с оператором " + bex.message);
                }
                // CASE 3
                catch (IncorrectFormatException iex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    // Использование свойства Data класса BadOperationException для одной пары ключ-значение
                    if (iex.Data.Count > 0)
                        Console.WriteLine($"Выражение {iex.Data[0]} некорректное, попробуйте написать в формате \n" +
        " a + b \n a * b \n a - b \n a / b");
                    else
                        Console.WriteLine("Выражение некорректное, попробуйте написать в формате \n" +
        " a + b \n a * b \n a - b \n a / b");
                }
                // CASE 4
                catch (ParseException pex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    // Использования поля message класса ParseException
                    Console.WriteLine("Операнд " + pex.message + " не является числом");
                }
                // CASE 5
                catch (DivideByZeroException dbzex)
                {
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.ForegroundColor = ConsoleColor.White;
                    // Использование свойства Data класса DivideByZeroException (если инфы много)
                    if (dbzex.Data.Count > 0)
                    {
                        foreach (var item in dbzex.Data.Values)
                            Console.WriteLine(item);
                    }
                    else
                        Console.WriteLine("Деление на ноль");
                }
                // CASE 6
                catch (Evil13Exception eex)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    // Использование свойства Data класса Evil13Exception для одной пары ключ-значение
                    if (eex.Data.Count > 0)
                        Console.WriteLine("Вы получили ответ 13! \n" + eex.Data[0]);
                    else
                        Console.WriteLine("Вы получили ответ 13!");
                }
                // Доп. задание в критерии оценки
                catch (OverflowException ovex) when (ovex.Data.Count > 0)
                {
                    Console.BackgroundColor = ConsoleColor.Green;                    
                    Console.WriteLine($"Введите значение от {int.MinValue} до {int.MaxValue}");
                    Console.ResetColor();
                }
                // Задание с *
                catch (OverflowException) 
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Результат выражения вышел за границы int");                    
                    Console.ResetColor();
                }
                // CASE 7
                catch (Exception)
                {    
                    // Доп. задание 2
                    throw;
                }
                Console.ResetColor();
            }            
        }

        public static void Sum(int a, int b)
        {
            int result = checked(a + b);
            Console.WriteLine($"Ответ: {result}");
            CheckResult(result);
        }

        public static void Sub(int a, int b)
        {
            int result = a - b;
            Console.WriteLine($"Ответ: {result}");
            CheckResult(result);
        }

        public static void Mul(int a, int b)
        {
            int result = checked(a * b);
            Console.WriteLine($"Ответ: {result}");
            CheckResult(result);
        }

        public static void Div(int a, int b)
        {
           if (b == 0)
            {
                var dbzex = new DivideByZeroException();
                dbzex.Data.Add(0, "DataInfo: Произошло деление на ноль!");
                throw dbzex;
            }              
            else
            {                   
                int result = a / b;
                Console.WriteLine($"Ответ: {result}");
                CheckResult(result);              
            }                
        }
        
        public static bool isInt (string str)
        {
            try
            {
                int a = int.Parse(str);
                return true;
            }
            catch (FormatException)
            {
                // Вывод числа с плавающей точкой
                if (double.TryParse(str, out double result))
                {
                    var exd = new Exception();
                    exd.Data.Add(4, "Я не смог обработать ошибку");
                    throw exd;
                }                                  
                               
                return false;              
            }
            catch (OverflowException)
            {
                var ex = new OverflowException();
                ex.Data.Add(2, "2");
                throw ex;
            }
        }
        
        public static void CheckResult(int result)
        {
            if (result == 13)
            {
                var ex = new Evil13Exception();
                ex.Data.Add(0, "Дополнительная информация из Data");
                throw ex;
            }
        }
    }
}