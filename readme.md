## Introduction

This repository contains all the files needed to compile a working version of the **vigenere_square_cipher (command-line version)**.

- If you're looking for the GUI version, this is not it. You can find that [here](https://github.com/DelKatey/vig.sq.crypt.gui).
- If you're looking for the DLL version, this is also not it. You can find that [here](https://github.com/DelKatey/vig.sq.crypt.dll).


## Usage

If you use the command, "vigscrypt help", the below will appear. Please note that the help command only works when you're not using the program directly, and only by calling it through the command prompt or another program.

    This is the help information for the Vigenere Square Cryptor program.
    USAGE:
	    vigscrypt [mode] [outputType] [passcode] [text]
    where
	    mode				The mode to operate in
		    				(-e for encrypting, -d for decrypting)
	
    	outputType			Optional, add "-n" to return result only, or "-f",
	    					for the program to export the results to its
		    				running directory in a text file.
	
    	passcode			The keyword to encrypt or decrypt the text with

	    text				The plain text to encrypt or cipher text to decrypt
		
		Options:
		    /?				Display this help message
			/help			Display this help message
			/about			Display the about information for this program
	
## How this program came to be

Out of boredom, I decided to implement what I learnt in a module of a course I was taking, in C#, and as a quick challenge to myself.

## Installation

This was coded with Visual Studio 2010, using .NET Framework 3.5, and should thus be usable with any version of Visual Studio 2010 and up, as well as SharpDevelop. Compatibility wise, it should present no challenges in being converted to use a newer version of the .NET Framework.

## License

This project is licensed under the [MIT License](LICENSE.md).
