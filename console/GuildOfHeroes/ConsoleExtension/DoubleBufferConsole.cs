using ConsoleExtension.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExtension
{
    public class DoubleBufferConsole : IConsole
    {
        public const
        ConsoleColor DefaultBackground = ConsoleColor.Black;
        public const
        ConsoleColor DefaultForeground = ConsoleColor.Gray;

        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

        public int BufferWidth { get; private set; }
        public int BufferHeight { get; private set; }

        public DoubleBufferConsole(int width, int height)
        {
            BufferWidth = width;
            BufferHeight = height;
            SetupBuffers();
            Console.SetBufferSize(width, height + 1);
            Console.SetWindowSize(width, height);
        }

        public void Draw()
        {
            DrawBuffersDifference();
            SwapBuffers();
        }

        public void ResetColor()
        {
            BackgroundColor = DefaultBackground;
            ForegroundColor = DefaultForeground;
        }

        public void SetCursorPosition(int left, int top)
        {
            currentLine = top;
            currentColumn = left;
            if (!Inside())
                throw new IndexOutOfRangeException(
                    "Position of coursor is out of bounds"
                );
        }

        private bool Inside()
        {
            return currentLine >= 0 &&
                   currentColumn >= 0 &&
                   currentLine < BufferHeight &&
                   currentColumn < BufferWidth;
        }

        public void Write(char symbol)
        {
            PutSymbolAndColors(symbol);
            UpdateCoursorPosiotion();
        }

        private char[,] currentCharBuffer;
        private char[,] previousCharBuffer;
        private ConsoleColor[,] currentForegroundBuffer;
        private ConsoleColor[,] previousForegroundBuffer;
        private ConsoleColor[,] currentBackgroundBuffer;
        private ConsoleColor[,] previousBackgroundBuffer;

        private int currentLine;
        private int currentColumn;


        private void SetupBuffers()
        {
            currentCharBuffer = CreateEmptyCharBuffer();
            previousCharBuffer = CreateEmptyCharBuffer();
            currentForegroundBuffer = CreateColorBuffer(DefaultForeground);
            previousForegroundBuffer = CreateColorBuffer(DefaultForeground);
            currentBackgroundBuffer = CreateColorBuffer(BackgroundColor);
            previousBackgroundBuffer = CreateColorBuffer(BackgroundColor);
        }
        private ConsoleColor[,] CreateColorBuffer(ConsoleColor color)
        {
            return CreateBuffer(color);
        }
        private char[,] CreateEmptyCharBuffer()
        {
            return CreateBuffer(' ');
        }
        private T[,] CreateBuffer<T>(T itemValue)
        {
            var buffer = new T[BufferHeight, BufferWidth];
            FillBuffer(itemValue, buffer);
            return buffer;
        }

        private void FillBuffer<T>(T itemValue, T[,] buffer)
        {
            for (int i = 0; i < BufferHeight; ++i)
                for (int j = 0; j < BufferWidth; ++j)
                    buffer[i, j] = itemValue;
        }

        private void PutSymbolAndColors(char symbol)
        {
            currentCharBuffer[currentLine, currentColumn] = symbol;
            currentForegroundBuffer[currentLine, currentColumn] = ForegroundColor;
            currentBackgroundBuffer[currentLine, currentColumn] = BackgroundColor;
        }
        private void UpdateCoursorPosiotion()
        {
            currentColumn++;
            if (currentColumn == BufferWidth)
            {
                currentColumn = 0;
                currentLine++;
                if (currentLine == BufferHeight)
                    currentLine = 0;
            }
        }
        private void DrawBuffersDifference()
        {
            for(int line = 0; line < BufferHeight; ++line)
                for (int column = 0; column < BufferWidth; ++column)
                    if (SymbolsIsDifferent(line, column))
                        DrawSymbol(line, column);
            Console.SetCursorPosition(0, 0);
        }
        private bool SymbolsIsDifferent(int line, int column)
        {
            return
                previousCharBuffer[line, column] !=
                currentCharBuffer[line, column] ||
                previousBackgroundBuffer[line, column] !=
                currentBackgroundBuffer[line, column] ||
                previousForegroundBuffer[line, column] !=
                currentForegroundBuffer[line, column];

        }
        private void DrawSymbol(int line, int column)
        {
            Console.SetCursorPosition(column, line);
            Console.ForegroundColor = currentForegroundBuffer[line, column];
            Console.BackgroundColor = currentBackgroundBuffer[line, column];
            Console.Write(currentCharBuffer[line, column]);
            previousCharBuffer[line, column] = currentCharBuffer[line, column];
            previousForegroundBuffer[line, column] = currentForegroundBuffer[line, column];
            previousBackgroundBuffer[line, column] = currentBackgroundBuffer[line, column];
        }

        private void SwapBuffers()
        {
            SwapBuffers(ref currentCharBuffer, ref previousCharBuffer);
            SwapBuffers(ref currentBackgroundBuffer, ref previousBackgroundBuffer);
            SwapBuffers(ref currentForegroundBuffer, ref previousForegroundBuffer);

            FillBuffer(' ', currentCharBuffer);
            FillBuffer(DefaultForeground, currentForegroundBuffer);
            FillBuffer(DefaultBackground, currentBackgroundBuffer);
        }

        private void SwapBuffers<T>(ref T[,] current, ref T[,] previous)
        {
            var tmp = current;
            current = previous;
            previous = tmp;
        }
    }
}
