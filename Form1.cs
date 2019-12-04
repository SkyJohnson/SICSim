﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SICSim
{
    public partial class SicSimForm : Form
    {
        string a, x, l, b, s, t, f, pc; // variables for register values

        string startAddr = 0.ToString("X");   // starting address of program (0 by default)
        Dictionary<string, string> locctr = new Dictionary<string, string>();   // dictionary for keeping track of addresses

        /// <summary>
        /// Initializing the form
        /// Initializes register values to 0 and sets the text in the registers text box
        /// </summary>
        public SicSimForm()
        {
            InitializeComponent();
            a = "0";
            x = "0";
            l = "0";
            b = "0";
            s = "0";
            t = "0";
            f = "0";
            pc = 0.ToString("X");   // prog counter in hex
            this.regText.Text = $"Registers:\r\n\r\n\r\n\r\n\r\nA (Accum): {a}\r\n\r\n\r\nX (Index): {x}\r\n\r\n\r\nL (Link): {l}\r\n\r\n\r\nB (Base): {b}\r\n\r\n\r\nS (General): {s}\r\n\r\n\r\nT (General): {t}\r\n\r\n\r\nF (Float): {f}\r\n\r\n\r\nPC (Program Counter): {pc}";
        }

        /// <summary>
        /// Button to import user selected text file
        /// </summary>
        /// <param name="sender"> Auto Generated </param>
        /// <param name="e"> Auto Generated </param>
        private void fileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string path = openFile.FileName;    // File path
                codeInput.Text = System.IO.File.ReadAllText(path);  // write contents of file to coneInput text box
            }
        }

        /// <summary>
        /// Runs pass one of the assembler
        /// Assigns addresses to each line of code using locctr dictionary and
        /// populates memory table with labels and their corresponding addresses and values
        /// </summary>
        /// <param name="sender"> Auto Generated </param>
        /// <param name="e"> Auto Generated </param>
        private void instrBtn_Click(object sender, EventArgs e)
        {
            string label, op, ta, r1, r2;  // variables for instructions and data labels
            int lineNum = 0;    // Keep track of line number for user debugging
            // loops through each line of codeInput text box and splits into seperate strings for instructions and data labels
            foreach (string line in codeInput.Lines)
            {
                line.Trim();    // ignore extra whitespace
                if (!string.IsNullOrEmpty(line))    // make sure line is not empty
                {
                    locctr.Add(pc, line);   // add line to location counter
                    string[] instructions = line.Split();
                    if (instructions.Length == 2)   // Instruction with no label
                    {
                        op = instructions[0];    // Instruction
                        ta = instructions[1];      // Target address
                        // Need to check what format to determine next PC
                        if (op[op.Length-1] == 'R' || op == "RMO")   // Format 2
                        {
                            pc = (Int32.Parse(pc, System.Globalization.NumberStyles.HexNumber) + 2).ToString("X");  // PC increments 2 bytes
                        }
                        else
                        {
                            // assuming everything else is format 3
                            pc = (Int32.Parse(pc, System.Globalization.NumberStyles.HexNumber) + 3).ToString("X");  // PC increments 3 bytes
                        }
                    }
                    else if (instructions.Length == 3)  // Format 3 with labels or format 2
                    {
                        // Set starting address
                        if (instructions[1] == "START")
                        {
                            startAddr = instructions[2];
                            pc = instructions[2];
                            continue;   // skip to next iteration of loop
                        }
                        string opOrLabel = instructions[0]; // intermediate var to determine if first part of string is address label or op for format 2 instruction
                        if (opOrLabel == "RMO" || opOrLabel[opOrLabel.Length - 1] == 'R') // format 2
                        {
                            op = opOrLabel;
                            r1 = instructions[1];
                            r2 = instructions[2];
                            pc = (Int32.Parse(pc, System.Globalization.NumberStyles.HexNumber) + 2).ToString("X");  // PC increments by 2 bytes
                        }
                        else
                        {
                            label = instructions[0];    // Instruction location label
                            op = instructions[1];    // Instruction
                            ta = instructions[2];      // Data memory label

                            // Need to check what format to determine next PC
                            if (op == "RESW")  // reserve word (3 bytes) in memory
                            {
                                this.memView.Rows.Add(pc, label, "NULL");  // update memory table
                                pc = (Int32.Parse(pc, System.Globalization.NumberStyles.HexNumber) + 3 * Int32.Parse(ta, System.Globalization.NumberStyles.HexNumber)).ToString("X");  // PC increments by given bytes * 3
                            }
                            else if (op == "RESB")  // reserve byte in memory
                            {
                                this.memView.Rows.Add(pc, label, "NULL");  // update memory table
                                pc = (Int32.Parse(pc, System.Globalization.NumberStyles.HexNumber) + Int32.Parse(ta, System.Globalization.NumberStyles.HexNumber)).ToString("X");  // PC increments by given bytes
                            }
                            else if (op == "BYTE")
                            {
                                this.memView.Rows.Add(pc, label, ta);  // update memory table
                                pc = (Int32.Parse(pc, System.Globalization.NumberStyles.HexNumber) + 1).ToString("X");  // PC increments 1 bytes
                            }
                            else if (op == "WORD")
                            {
                                this.memView.Rows.Add(pc, label, ta);  // update memory table
                                pc = (Int32.Parse(pc, System.Globalization.NumberStyles.HexNumber) + 3).ToString("X");  // PC increments 3 bytes
                            }
                            else
                            {
                                // assuming everything else is format 3
                                this.memView.Rows.Add(pc, label, $"{op} {ta}");  // update memory table
                                pc = (Int32.Parse(pc, System.Globalization.NumberStyles.HexNumber) + 3).ToString("X");  // PC increments 3 bytes
                            }
                        }
                    }
                    else if (instructions.Length == 4)
                    {
                        // format 2 with labels
                        label = instructions[0];    // Instruction location label
                        op = instructions[1];   // Instruction
                        r1 = instructions[2];   // register 1
                        r2 = instructions[3];   // register 2
                        this.memView.Rows.Add(pc, label, $"{op} {r1},{r2}");  // update memory table
                        pc = (Int32.Parse(pc, System.Globalization.NumberStyles.HexNumber) + 2).ToString("X");  // PC increments 2 bytes
                    }
                    else
                    {
                        MessageBox.Show("Invalid Instruction Format");
                    }
                }
                /* This sections strictly for debugging purposes to verify correct values for locctr*/
                foreach (KeyValuePair<string, string> pair in locctr)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", pair.Key, pair.Value);
                }
                ++lineNum;  // increment line number
            }
        }

        /// <summary>
        /// Pass two
        /// Looks up instruction in locctr at given value of PC
        /// TOOD:
        /// Split instruction into its opcode and target address or registers
        /// Call runInstr to handle given operation (Need seperate functions for format 2 and format 3/4?)
        /// How to determine end of instructions?
        /// </summary>
        /// <param name="addr"> program counter used to look up instruction in locctr </param>
        private void lookupInstr(string addr)
        {
            locctr.TryGetValue(addr, out string instruction);   // same as string instruction = locctr[addr]
        }

        /// <summary>
        /// Handles operation of given instruction based on given opcode and target address for format 3/4 instructions
        /// TODO:
        /// Implement rest of Jump and XE instructions
        /// Update PC
        /// Call lookupInstr at new PC
        /// </summary>
        /// <param name="op"> Instruction Opcode </param>
        /// <param name="ta"> Target Address </param>
        private void runInstr(string op, string ta)
        {
            switch (op)
            {
                case "RSUB":    // Return from subroutine
                    pc = l;     // Sets program counter equal to link register
                    break;
                case "ADD":     // Add to register A
                    for (int i = 0; i < memView.Rows.Count; i++)
                    {
                        // Searches for label in memory table
                        if (memView.Rows[i].Cells[1].Value.ToString().Contains(ta))
                        {
                            // Have to convert from string to int to perform operation, then convert back to string to write back to register
                            a = (Int32.Parse(a) + Int32.Parse(this.memView.Rows[i].Cells[2].Value.ToString())).ToString();
                            break;
                        }
                    }
                    break;
                case "SUB":     // Subtract from register A
                    for (int i = 0; i < memView.Rows.Count; i++)
                    {
                        if (memView.Rows[i].Cells[1].Value.ToString().Contains(ta))
                        {
                            a = (Int32.Parse(a) - Int32.Parse(this.memView.Rows[i].Cells[2].Value.ToString())).ToString();
                            break;
                        }
                    }
                    break;
                case "MUL":     // Multiply by register A
                    for (int i = 0; i < memView.Rows.Count; i++)
                    {
                        if (memView.Rows[i].Cells[1].Value.ToString().Contains(ta))
                        {
                            a = (Int32.Parse(a) * Int32.Parse(this.memView.Rows[i].Cells[2].Value.ToString())).ToString();
                            break;
                        }
                    }
                    break;
                case "DIV":     // Divide by register A
                    for (int i = 0; i < memView.Rows.Count; i++)
                    {
                        if (memView.Rows[i].Cells[1].Value.ToString().Contains(ta))
                        {
                            a = (Int32.Parse(a) / Int32.Parse(this.memView.Rows[i].Cells[2].Value.ToString())).ToString();
                            break;
                        }
                    }
                    break;
                case "AND":     // Bitwise AND of register A
                    for (int i = 0; i < memView.Rows.Count; i++)
                    {
                        if (memView.Rows[i].Cells[1].Value.ToString().Contains(ta))
                        {
                            a = (Int32.Parse(a) & Int32.Parse(this.memView.Rows[i].Cells[2].Value.ToString())).ToString();
                            break;
                        }
                    }
                    break;
                case "OR":     // Bitwise OR of register A
                    for (int i = 0; i < memView.Rows.Count; i++)
                    {
                        if (memView.Rows[i].Cells[1].Value.ToString().Contains(ta))
                        {
                            a = (Int32.Parse(a) | Int32.Parse(this.memView.Rows[i].Cells[2].Value.ToString())).ToString();
                            break;
                        }
                    }
                    break;
                case "LDA":     // Load from memory to register A
                    for (int i = 0; i < memView.Rows.Count; i++)
                    {
                        if (memView.Rows[i].Cells[1].Value.ToString().Contains(ta))
                        {
                            a = this.memView.Rows[i].Cells[2].Value.ToString();
                            break;
                        }
                    }
                    break;
                case "LDL":     // Load from memory to link register
                    for (int i = 0; i < memView.Rows.Count; i++)
                    {
                        if (memView.Rows[i].Cells[1].Value.ToString().Contains(ta))
                        {
                            l = this.memView.Rows[i].Cells[2].Value.ToString();
                            break;
                        }
                    }
                    break;
                case "LDX":     // Load from memory to index register
                    for (int i = 0; i < memView.Rows.Count; i++)
                    {
                        if (memView.Rows[i].Cells[1].Value.ToString().Contains(ta))
                        {
                            x = this.memView.Rows[i].Cells[2].Value.ToString();
                            break;
                        }
                    }
                    break;
                case "STA":     // store register A in memory
                    for (int i = 0; i < memView.Rows.Count; i++)
                    {
                        if (memView.Rows[i].Cells[1].Value.ToString().Contains(ta))
                        {
                            this.memView.Rows[i].Cells[2].Value = a;
                            break;
                        }
                    }
                    break;
                case "STL":     // store link register in memory
                    for (int i = 0; i < memView.Rows.Count; i++)
                    {
                        if (memView.Rows[i].Cells[1].Value.ToString().Contains(ta))
                        {
                            this.memView.Rows[i].Cells[2].Value = l;
                            break;
                        }
                    }
                    break;
                case "STX":     // store index register in memory
                    for (int i = 0; i < memView.Rows.Count; i++)
                    {
                        if (memView.Rows[i].Cells[1].Value.ToString().Contains(ta))
                        {
                            this.memView.Rows[i].Cells[2].Value = x;
                            break;
                        }
                    }
                    break;
                case "J":   // Jump to target address
                    for (int i = 0; i < memView.Rows.Count; i++)
                    {
                        if (memView.Rows[i].Cells[1].Value.ToString().Contains(ta))
                        {
                            pc = this.memView.Rows[i].Cells[0].Value.ToString();    // set program counter to target address
                            break;
                        }
                    }
                    break;
                default:
                    MessageBox.Show("Unsupported Instruction");
                    break;
            }
        }
    }
}
