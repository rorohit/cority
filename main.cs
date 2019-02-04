using System;
using System.Collections.Generic;

class MainClass {
 static int peopleCount = 0;
 static int invoiceCount = 0;
 static double totalInvoiceValues = 0.0;
 static double totalInvoiceValuesPerPerson = 0.0;
 static Dictionary < string, double > people = new Dictionary < string, double > ();
 public static void Main(string[] args) {
  try {
   if (args.Length != 1) {
    Console.WriteLine("There was an Error-:Incorrect File Name Argument");
   }
   string finalState = readFile(args[0]);
   if (finalState != "Success") {
    Console.WriteLine("There was an Error-: " + finalState);
   }
  } catch (Exception e) {
   Console.WriteLine("There was an Error-: Argument Issue" + e.ToString());
  }
 }
 static string readFile(string fileNameWithPath) {
  System.IO.StreamReader file;
  System.IO.StreamWriter fileOut;
  // Read the file and display it line by line.  
  try {
   file =
    new System.IO.StreamReader(fileNameWithPath);
   fileOut =
    new System.IO.StreamWriter(fileNameWithPath + ".out");
  } catch (Exception e) {
   return e.ToString();
  }
  string firstLine = null;
  while (true) {
   try {
    int countCamps = 1;
    firstLine = file.ReadLine();
    peopleCount = 0;
    if (int.TryParse(firstLine, out peopleCount)) {
     if (peopleCount == 0) break;
     totalInvoiceValues = 0.0;

     for (int i = 1; i <= peopleCount; i++) {

      string invoiceNum = file.ReadLine();
      invoiceCount = 0;
      if (int.TryParse(invoiceNum, out invoiceCount)) {
       if (invoiceCount == 0) return "Missing invoices for people";
       totalInvoiceValuesPerPerson = 0.0;
       for (int j = 1; j <= invoiceCount; j++) {
        string invoice = file.ReadLine();
        double invoiceValue = 0.0;
        if (double.TryParse(invoice, out invoiceValue)) {
         totalInvoiceValues = totalInvoiceValues + invoiceValue;
         totalInvoiceValuesPerPerson = totalInvoiceValuesPerPerson + invoiceValue;
        } else return "Error";
        Console.WriteLine("Invoice Count is " + j);

       }
      } else {
       return "Invalid Invoice";
      }
      Console.WriteLine("People Count is " + i);
      people.Add(countCamps.ToString() + "," + i.ToString(), totalInvoiceValuesPerPerson);
      countCamps++;
     }
     double amountPerPerson = 0.0;
     amountPerPerson = totalInvoiceValues / peopleCount;
     Console.WriteLine("Total invoice Value = " + totalInvoiceValues);
     Console.WriteLine("Amount Per Person = " + amountPerPerson);

     foreach(KeyValuePair < string, double > pair in people) {
      double finalValue = amountPerPerson - pair.Value;
      if (finalValue < 0) {
       String finValue;
       finalValue = -1 * finalValue;
       decimal xx = (decimal) finalValue;
       finalValue = (double) Math.Round(xx, 2, MidpointRounding.ToEven);
       finValue = "(" + finalValue.ToString() + ")";
       fileOut.WriteLine(finValue);
      } else {
       String finValue;
       decimal xx = (decimal) finalValue;
       finalValue = (double) Math.Round(xx, 2, MidpointRounding.ToEven);
       finValue = finalValue.ToString();
       fileOut.WriteLine(finValue);
      }

     }
     fileOut.WriteLine();
     people.Clear();
    } else {
     return "Invalid people";
    }
   } catch (Exception e) {
    return e.ToString();
   }
  }
  file.Close();
  fileOut.Close();
  return "Success";
 }
}