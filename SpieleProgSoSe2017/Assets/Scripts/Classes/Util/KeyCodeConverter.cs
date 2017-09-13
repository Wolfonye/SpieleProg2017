/*
 * Author: Katya Engelmann, Philipp Bous
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Sorgt dafür, dassdie bei Tastendruck erkannten Keycodes in benutzbare Strings umgewandelt werden, weil ToString das leider nicht tut.
//!!Vorsicht unterstützt nicht alle Tasten--> Verhalten entsprechend steuern
public static class KeyCodeConverter : object {

	public static string keyCodeStringToKeyString(string keyCodeString){
		switch(keyCodeString){
			case "Backspace":
				return "backspace";
			case "Tab":
				return "tab";	
			case "Return":
				return "return";
			case "Escape":
				return "escape";
			case "Space":
				return "space";
			case "UpArrow":
				return "up";
			case "DownArrow":
				return "down";
			case "RightArrow":
				return "right";
			case "LeftArrow":
				return "left";
			case "Alpha0":
				return "0";
			case "Alpha1":
				return "1";
			case "Alpha2":
				return "2";
			case "Alpha3":
				return "3";
			case "Alpha4":
				return "4";
			case "Alpha5":
				return "5";
			case "Alpha6":
				return "6";
			case "Alpha7":
				return "7";
			case "Alpha8":
				return "8";
			case "Alpha9":
				return "9";
			case "Hash":
				return "#";
			case "Plus":
				return "+";
			case "Comma":
				return ",";
			case "Minus":
				return "-";
			case "Period":
				return ".";
			case "Less":
				return "<";
			case "A":
				return "a";
			case "B":
				return "b";
			case "C":
				return "c";
			case "D":
				return "d";
			case "E":
				return "e";
			case "F":
				return "f";
			case "G":
				return "g";
			case "H":
				return "h";
			case "I":
				return "i";
			case "J":
				return "j";
			case "K":
				return "k";
			case "L":
				return "l";
			case "M":
				return "m";
			case "N":
				return "n";
			case "O":
				return "o";
			case "P":
				return "p";
			case "Q":
				return "q";
			case "R":
				return "r";
			case "S":
				return "s";
			case "T":
				return "t";
			case "U":
				return "u";
			case "V":
				return "v";
			case "W":
				return "w";
			case "X":
				return "x";
			case "Y":
				return "y";
			case "Z":
				return "z";
			case "RightShift":
				return "right shift";
			case "LeftShift":
				return "left shift";
			case "RightControl":
				return "right ctrl";
			case "LeftControl":
				return "left ctrl";
			case "RightAlt":
				return "right alt";
			case "LeftAlt":
				return "left alt";
		}
		return "none";
	}
}

