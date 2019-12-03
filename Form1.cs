using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SICSim
{
    public partial class SicSimForm : Form
    {
        string a, x, l, pc; // variables for register values
        string startAddr;   // starting address of program
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
            pc = 0.ToString("X");   // prog counter in hex
            this.regText.Text = $"Registers:\r\n\r\nA (Accum): {a}\r\n\r\nX (Index): {x}\r\n\r\nL (Link): {l}\r\n\r\nPC (Program Counter): {pc}";
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

        // Not using this function anymore but saving some code for reuse
        /// <summary>
        /// Handles updating the memory table when saveDataBtn is clicked
        /// </summary>
        /// <param name="sender"> Auto Generated </param>
        /// <param name="e"> Auto Generated </param>
        //private void saveDataBtn_Click(object sender, EventArgs e)
        //{
        //    string label, dir;  // String variables for data memory labels and compiler directives
        //    int value, length;  // value for BYTE and WORD, length for RESB and RESW
            
        //    // Loops through each line in data text box, splits each line into multiple strings, and updates memory table from each line
        //    foreach (string line in dataBox.Lines)
        //    {
        //        line.Trim();
        //        if (!string.IsNullOrEmpty(line))
        //        {
        //            string[] data = line.Split();
        //            label = data[0];    // data memory label
        //            dir = data[1];      // compiler directive
        //            length = Int32.Parse(data[2]);  // amount of bytes or words to reserve in memory
        //            value = Int32.Parse(data[2]);   // value to store in memory

        //            // Int32.Parse converts string to int to allow addition, must be converted back to string to be written to memory table
        //            if (dir == "WORD")
        //            {
        //                this.memView.Rows.Add(addr, label, value);      // update memory table
        //                this.addr = (Int32.Parse(addr, System.Globalization.NumberStyles.HexNumber) + 3).ToString("X");     // increment next memory address by 3 bytes 
        //            }
        //            else if (dir == "BYTE")
        //            {
        //                this.memView.Rows.Add(addr, label, value);      // update memory table
        //                this.addr = (Int32.Parse(addr, System.Globalization.NumberStyles.HexNumber) + 1).ToString("X");     // increment next memory address by 1 byte
        //            }
        //            else if (dir == "RESB")
        //            {
        //                this.memView.Rows.Add(addr, label, "NULL");     // update memory table
        //                this.addr = (Int32.Parse(addr, System.Globalization.NumberStyles.HexNumber) + length).ToString("X");    // increment next memory address by given amount of bytes
        //            }
        //            else if (dir == "RESW")
        //            {
        //                this.memView.Rows.Add(addr, label, "NULL");     // update memory table
        //                this.addr = (Int32.Parse(addr, System.Globalization.NumberStyles.HexNumber) + (3 * length)).ToString("X");    // incremement next memory address by givn amount of words
        //            }
        //            else
        //            {
        //                MessageBox.Show("Invalid Directve");    // SIC only supports BYTE, WORD, RESB, RESW, anything else throws and error, unless we want to implement and START and END directive
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Handles updating registers when instrBtn is clicked
        /// </summary>
        /// <param name="sender"> Auto Generated </param>
        /// <param name="e"> Auto Generated </param>
        private void instrBtn_Click(object sender, EventArgs e)
        {
            string label, instr, mem;  // variables for instructions and data labels
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
                        instr = instructions[0];    // Instruction
                        mem = instructions[1];      // Data memory label
                        // Need to check what format to determine next PC
                        if (instr[instr.Length-1] == 'R')   // Format 2
                        {

                        }
                    }
                    else if (instructions.Length == 3)  // Instruction with label
                    {
                        label = instructions[0];    // Instruction location label
                        instr = instructions[1];    // Instruction
                        mem = instructions[2];      // Data memory label
                        // Set starting address
                        if (instr == "START")   
                        {
                            startAddr = mem;
                            pc = mem;
                            continue;   // skip to next iteration of loop
                        }
                        // Need to check what format to determine next PC
                        if (instr[instr.Length - 1] == 'R')   // Format 2
                        {

                        }
                        this.memView.Rows.Add(pc, label, mem);  // update memory table
                    }
                    else
                    {
                        MessageBox.Show("Invalid Instruction Format");
                    }
                }
                Console.WriteLine(locctr);  // trying to test contents of location counter
                ++lineNum;  // increment line number
            }
        }

        /// <summary>
        /// Handle instructions opcodes
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
