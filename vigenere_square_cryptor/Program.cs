/*
 * Created by SharpDevelop.
 * User: delkatey
 * Date: 19/11/2015
 * Time: 1:13 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace vigenere_square_cryptor
{
	class Program
	{
		internal static readonly string[] AlphabetList = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
													"L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", 
													"W", "X", "Y", "Z" };
		internal static readonly string AlphabetString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		
		internal static int iMode = 0;
		internal static string sText = "", sPass = "";
		
		internal static void InitialWrongSelection()
		{
			//Console.WriteLine("");
			//Console.WriteLine("");
			//Console.WriteLine("Invalid selection! Please reselect!");
			//Console.ReadKey(true);
			Console.Clear();
			Main(new string[0]);
		}
		
		internal static int GetMode()
		{
			Console.Clear();
			Console.WriteLine("Please choose the mode you wish to use:");
			Console.WriteLine("1. Encrypt");
			Console.WriteLine("2. Decrypt");
			Console.WriteLine("3. About");
			Console.Write("Your selection: ");
			string tempRead = Console.ReadKey().KeyChar.ToString();
			//http://www.dotnetperls.com/console-readkey
			
			int intTemp = 0;
			
			if (!int.TryParse(tempRead, out intTemp))
			{
				InitialWrongSelection();
			}
			
			if (intTemp != 1 && intTemp != 2 && intTemp != 3)
			{
				InitialWrongSelection();
			}
			
			Console.Clear();
			
			return intTemp;
		}
		
		internal static string GetPlainOrCipherText(int TheMode)
		{
			Console.Clear();
			Console.Write("Please key in the ");
			if (TheMode == 1)
				Console.WriteLine("plain text to encrypt.");
			else if (TheMode == 2)
				Console.WriteLine("cipher text to decrypt.");
			Console.WriteLine("(Remember to press \"Enter\" when finished)");
			Console.WriteLine("");
			Console.Write("Enter your ");
			if (TheMode == 1)
				Console.Write("plain text");
			else if (TheMode == 2)
				Console.Write("cipher text");
			Console.WriteLine(" now:");
			
			string tempRead = Console.ReadLine();
			Console.Clear();
			
			return tempRead;
		}
		
		internal static string GetPasscode(int TheMode)
		{
			Console.Clear();
			Console.Write("Please enter the keyword that will be used to ");
			if (TheMode == 1)
				Console.WriteLine("encrypt the plain text.");
			else if (TheMode == 2)
				Console.WriteLine("decrypt the cipher text.");
			Console.WriteLine("(Remember to press \"Enter\" when finished)");
			Console.WriteLine("");
			Console.WriteLine("Enter your keyword now:");
			
			string tempRead = ReadPassword();
			
			Console.Clear();
			
			return tempRead;
		}
		
		//http://stackoverflow.com/questions/29201697/hide-replace-when-typing-a-password-c
		public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }
		
		internal static string ExpandKey(string cipherOrPlainText, string keyword)
		{
			if (keyword.Length < cipherOrPlainText.Length)
			{
				string tempKey = keyword;
				
				while (cipherOrPlainText.Length > tempKey.Length)
				{
					tempKey += keyword;
				}
				
				if (tempKey.Length > cipherOrPlainText.Length)
				{
					tempKey = tempKey.Substring(0, cipherOrPlainText.Length);
				}
				
				return tempKey;
			}
			else if (keyword.Length == cipherOrPlainText.Length)
				return keyword;
			else
				return "Keyword too long!";
		}

        internal static string RecaseAlpha(string convertedText, string originalText)
        {
            string tempStore = "";

            for (int ii = 0; ii < originalText.Length; ii++)
            {
                if (char.IsUpper(originalText[ii]))
                    tempStore += convertedText[ii].ToString().ToUpper();
                else
                    tempStore += convertedText[ii].ToString().ToLower();
            }

            return tempStore;
        }

        internal static string DecryptedText(string ciphertext, string keyword)
		{
			//Broken, non deciphering
			string tempStore = "";
			string KeyToUse = ExpandKey(RemoveAllNonAlpha(ciphertext), keyword);
			string[] tempList;
			
			for (int ii = 0; ii < RemoveAllNonAlpha(ciphertext).Length; ii++)
			{
                tempList = GetNewAlphaList(KeyToUse[ii].ToString());

                for (int iii = 0; iii < tempList.Length; iii++)
                {
                    //string FromList = tempList[iii].ToString().ToLower();
                    //string FromCipher = RemoveAllNonAlpha(ciphertext)[ii].ToString().ToLower();
                    if (tempList[iii].ToString().ToLower() == RemoveAllNonAlpha(ciphertext)[ii].ToString().ToLower())
                    {
                        tempStore += GetAlphaFromNumber(iii).ToLower();//GetAlphaFromNumber(iii).ToLower();
                        break;
                    }
                }
			}

            return RecaseAlpha(ReplaceAllNonAlpha(tempStore, ciphertext), ciphertext);
		}
		
		internal static string ReplaceSpaces(string convertedText, string originalText)
		{
			string sTemp = convertedText;
			string sTemp2 = convertedText;
			
			for (int ii = 0; ii < originalText.Length; ii++)
			{
				if (originalText[ii].ToString() == " ")
				{
					sTemp = sTemp.Substring(0, ii) + " " + sTemp2.Substring(ii);
				}
				
				sTemp2 = sTemp;
			}
			
			return sTemp;
		}
		
		internal static string ReplaceAllNonAlpha(string convertedText, string originalText)
		{
			string sTemp = convertedText;
			string sTemp2 = convertedText;
			string sTemp3 = convertedText;
			
			for (int ii = 0; ii < originalText.Length; ii++)
			{
				if (!OnlyLetters(originalText[ii].ToString()))
				{
					sTemp2 = sTemp.Substring(0, ii);
					sTemp3 = sTemp.Substring(ii);
					
					sTemp = sTemp2 + originalText[ii].ToString() + sTemp3;
				}
			}
			
			return sTemp;
		}
		
		internal static bool OnlyLetters(string textToCheck)
		{
			return Regex.IsMatch(textToCheck, @"^[a-zA-Z]+$");
		}
		
		internal static string RemoveNonAlpha(string sInput)
		{
			return Regex.Replace(sInput, "[^a-zA-Z0-9 -]", "");
		}
		
		internal static string RemoveSpaces(string sInput)
		{
			return Regex.Replace(sInput, " ", "");
		}
		
		internal static string RemoveAllNonAlpha(string sInput)
		{
			return RemoveNonAlpha(RemoveSpaces(sInput));
		}
		
		internal static string EncryptedText(string plaintext, string keyword)
		{
			string tempStore = "";
			string KeyToUse = ExpandKey(RemoveAllNonAlpha(plaintext), keyword);
			string[] tempList;
			int iSelector = 0;
			
			for (int ii = 0; ii < RemoveAllNonAlpha(plaintext).Length; ii++)
			{
				tempList = GetNewAlphaList(KeyToUse[ii].ToString());
				if (RemoveAllNonAlpha(plaintext)[ii].ToString() != " ")
				{
					iSelector = NeverOver26(GetNumericFromLetter(RemoveAllNonAlpha(plaintext)[ii].ToString())) - 1;
					
					tempStore += tempList[iSelector].ToLower();
				}
				else
				{
					tempStore += " ";
				}
			}
			
			return RecaseAlpha(ReplaceAllNonAlpha(tempStore, plaintext), plaintext);
		}
		
		public static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				iMode = GetMode();
				
				if (iMode != 3)
				{
					sText = GetPlainOrCipherText(iMode);
				
					sPass = GetPasscode(iMode);
				}
				
				if (iMode == 1)
				{
					Console.WriteLine("Your encrypted text is:");
					Console.WriteLine(EncryptedText(sText, sPass));
					Console.WriteLine("");
					Console.ReadKey(true);
				}
				else if (iMode == 2)
				{
					Console.WriteLine("Your decrypted text is:");
					Console.WriteLine(DecryptedText(sText, sPass));
					Console.WriteLine("");
					Console.ReadKey(true);
				}
				else if (iMode == 3)
				{
					Console.Clear();
					Console.WriteLine("About This Program...");
					Console.WriteLine("-------------------------------");
					Console.WriteLine("Coded by: Delaney, Katie");
					Console.WriteLine("Version: 1.0");
					Console.WriteLine("Time taken: 7 hours approx max");
					Console.WriteLine("");
					Console.Write("Press any key to continue . . . ");
					Console.ReadKey(true);
				}
				
				// TODO: Implement Functionality Here
	
				RestartProgram();
				
				TerminateProgram();
			}
			else if (args.Length == 1 && (args[0].ToLower() == "/help" || args[0].ToLower() == "/?" || args[0].ToLower() == "?" || args[0].ToLower() == "help"))
			{
				Console.WriteLine("This is the help information for the Vigenere Square Cryptor program.");
				PrintUsageCommands();
			}
			else if (args.Length == 1 && (args[0].ToLower() == "/about" || args[0].ToLower() == "about"))
			{
				Console.WriteLine("Coded by: Delaney, Katie");
				Console.WriteLine("Version: 1.0");
				Console.WriteLine("Time taken: 7 hours approx at most");
			}
			else if (args.Length == 3 && (args[0].ToLower() == "-e" || args[0].ToLower() == "-d"))
			{
				if (args[0].ToLower() == "-e")
				{
					Console.WriteLine("");
					Console.WriteLine("Your encrypted text is:");
					Console.WriteLine(EncryptedText(args[2], args[1]));
				}
				else if (args[0].ToLower() == "-d")
				{
					Console.WriteLine("");
					Console.WriteLine("Your decrypted text is:");
					Console.WriteLine(DecryptedText(args[2], args[1]));
				}
			}
			else if (args.Length == 3 && (args[0].ToLower() != "-e" && args[0].ToLower() != "-d"))
			{
				Console.WriteLine("");
				Console.WriteLine("Error: unrecognized or incomplete command line");
				Console.WriteLine("");
				PrintUsageCommands();
			}
			else if (args.Length == 4 && (args[0].ToLower() == "-e" || args[0].ToLower() == "-d") && args[1].ToLower() == "-f")
			{
				if (args[0].ToLower() == "-e")
				{
					using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + @"\encrypted_result.txt"))
					{
						sw.WriteLine(EncryptedText(args[3], args[2]));
					}
					
					Console.WriteLine("Exported results to encrypted_results.txt");
				}
				else if (args[0].ToLower() == "-d")
				{
					using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + @"\decrypted_result.txt"))
					{
						sw.WriteLine(DecryptedText(args[3], args[2]));
					}
					
					Console.WriteLine("Exported results to encrypted_results.txt");
				}
			}
			else if (args.Length == 4 && (args[0].ToLower() == "-e" || args[0].ToLower() == "-d") && args[1].ToLower() == "-n")
			{
				if (args[0].ToLower() == "-e")
				{
					Console.WriteLine(EncryptedText(args[3], args[2]));
				}
				else if (args[0].ToLower() == "-d")
				{
					Console.WriteLine(DecryptedText(args[3], args[2]));
				}
			}
			else
			{
				Console.WriteLine("");
				Console.WriteLine("Error: unrecognized or incomplete command line");
				Console.WriteLine("");
				PrintUsageCommands();
			}
		}
		
		internal static void PrintUsageCommands()
		{
			Console.WriteLine("USAGE:");
			Console.WriteLine("\tvigscrypt [mode] [outputType] [passcode] [text]");
			Console.WriteLine("where");
			Console.WriteLine("\tmode\t\tThe mode to operate in");
			Console.WriteLine("\t\t\t(-e for encrypting, -d for decrypting)");
			Console.WriteLine("");
			Console.WriteLine("\toutputType\tOptional, add \"-n\" to return result only, or \"-f\",");
			Console.WriteLine("\t\t\tfor the program to export the results to its");
			Console.WriteLine("\t\t\trunning directory in a text file.");
			Console.WriteLine("");
			Console.WriteLine("\tpasscode\tThe keyword to encrypt or decrypt the text with");
			Console.WriteLine("");
			Console.WriteLine("\ttext\t\tThe plain text to encrypt or cipher text to decrypt");
			Console.WriteLine("");
			Console.WriteLine("\tOptions:");
			Console.WriteLine("\t   /?\t\tDisplay this help message");
			Console.WriteLine("\t   /help\tDisplay this help message");
			Console.WriteLine("\t   /about\tDisplay the about information for this program");
		}
		
		internal static void TerminateProgram()
		{
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
			
			//http://stackoverflow.com/questions/5682408/command-to-close-an-application-of-console
			Environment.Exit(0);
		}
		
		internal static void RestartProgram()
		{
			Console.Clear();
			Console.Write("Would you like to encrypt/decrypt another text? (Y/N) ");
			
			string sAnswer = Console.ReadKey().KeyChar.ToString().ToLower();
			
			Console.WriteLine("");
			Console.WriteLine("");
			
			if (sAnswer != "y" && sAnswer != "n")
			{
				RestartProgram();
			}
			else if (sAnswer == "n")
			{
				Console.WriteLine("Bye bye!");
			}
			else if (sAnswer == "y")
			{
				Console.Clear();
				Main(new String[0]);
			}
		}
		
		internal static string GetNewAlphaString(string iChar)
		{
			if (iChar.Length == 1)
				return AlphabetString.Substring((NeverOver26(GetNumericFromLetter(iChar)) - 1)) + AlphabetString.Substring(0, (NeverOver26(GetNumericFromLetter(iChar)) - 1));
			else
				return "Invalid character!";
		}
		
		internal static string[] GetNewAlphaList(string iChar)
		{	
			char[] AlphaCharArray = GetNewAlphaString(iChar).ToCharArray();
			
			List<string> tempListAppend = new List<string>();
			
			for (int ii = 0; ii < AlphaCharArray.Length; ii++)
			{
				tempListAppend.Add(AlphaCharArray[ii].ToString());
			}
			
			return tempListAppend.ToArray();
		}

        internal static string GetAlphaFromNumber(int number)
        {//http://stackoverflow.com/questions/1951517/convert-a-to-1-b-to-2-z-to-26-and-then-aa-to-27-ab-to-28-column-indexes-to
            string sString = "";
            decimal iNumber = number + 1;
            while (iNumber > 0)
            {
                decimal currentLetterNumber = (iNumber - 1) % 26;
                char currentLetter = (char)(currentLetterNumber + 65);
                sString = currentLetter + sString;
                iNumber = (iNumber - (currentLetterNumber + 1)) / 26;
            }
            return sString;
        }

		internal static int GetNumericFromLetter(string letter)
		{
			//http://stackoverflow.com/questions/1951517/convert-a-to-1-b-to-2-z-to-26-and-then-aa-to-27-ab-to-28-column-indexes-to
			if (letter.Length == 1)
			{
				int retVal = 0;
			    string col = letter.ToUpper();
			    for (int iChar = col.Length - 1; iChar >= 0; iChar--)
			    {
			    	char colPiece = col[iChar];
			    	int colNum = colPiece - 64;
			    	retVal = retVal + colNum * (int)Math.Pow(26, col.Length - (iChar + 1));
			    }
			    return retVal;
			}
			else
				return 0;
		}
		
		internal static int NeverOver26(int iValue)
		{
			if (iValue > 26)
				return iValue - 26;
			else
				return iValue;
		}
	}
}
